﻿using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentBusiness : ILogicalRecordSegmentBusiness
    {
        private readonly ILogicalRecordSegmentHeaderBusiness _logicalRecordSegmentHeaderBusiness;
        private readonly ILogicalRecordSegmentEncryptionPacketBusiness _logicalRecordSegmentEncryptionPacketBusiness;
        private readonly ILogicalRecordSegmentTrailerBusiness _logicalRecordSegmentTrailerBusiness;
        private readonly IComponentBusiness _componentBusiness;

        public LogicalRecordSegmentBusiness(ILogicalRecordSegmentHeaderBusiness logicalRecordSegmentHeaderBusiness, ILogicalRecordSegmentEncryptionPacketBusiness logicalRecordSegmentEncryptionPacketBusiness, ILogicalRecordSegmentTrailerBusiness logicalRecordSegmentTrailerBusiness, IComponentBusiness componentBusiness)
        {
            _logicalRecordSegmentHeaderBusiness = logicalRecordSegmentHeaderBusiness;
            _logicalRecordSegmentEncryptionPacketBusiness = logicalRecordSegmentEncryptionPacketBusiness;
            _logicalRecordSegmentTrailerBusiness = logicalRecordSegmentTrailerBusiness;
            _componentBusiness = componentBusiness;
        }

        public LogicalRecordSegment ReadLogicalRecordSegment(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.IsAtEndOfStream()) { return null; }

            var header = _logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream);
            if (header == null) { return null; }

            var bodySize = header.LogicalRecordSegmentLength - 4;

            LogicalRecordSegmentEncryptionPacket encryptionPacket = null;
            if (header.EncryptionPacket)
            {
                encryptionPacket = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream);
                if (encryptionPacket == null) { return null; }
                bodySize -= encryptionPacket.Size;
            }

            LogicalRecordSegmentTrailer trailer = null;
            var trailerSize = 0;
            if (header.HasTrailer())
            {
                dlisStream.Seek(bodySize, SeekOrigin.Current);
                trailer = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, header);
                if (trailer == null) { return null; }
                trailerSize = header.TrailerSize(trailer);
                bodySize -= trailerSize;
                dlisStream.Seek(-(bodySize + trailerSize), SeekOrigin.Current);
            }

            var body = new List<Component>();
            using (var bodyStream = new MemoryStream(dlisStream.ReadBytes(bodySize)))
            {
                var component = _componentBusiness.ReadComponent(bodyStream);
                while(component != null)
                {
                    body.Add(component);
                    if (component.IsFileHeaderLogicalRecord())
                    {
                        body.AddRange(_componentBusiness.ReadFileHeaderLogicalRecord(bodyStream));
                    }
                    component = _componentBusiness.ReadComponent(bodyStream);
                }
            }
            dlisStream.Seek(trailerSize, SeekOrigin.Current);

            return new LogicalRecordSegment
            {
                Header = header,
                EncryptionPacket = encryptionPacket,
                Body = body,
                Trailer = trailer
            };
        }
    }
}
