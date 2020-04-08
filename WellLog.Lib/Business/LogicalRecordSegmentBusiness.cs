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

        public LogicalRecordSegmentBusiness(ILogicalRecordSegmentHeaderBusiness logicalRecordSegmentHeaderBusiness, ILogicalRecordSegmentEncryptionPacketBusiness logicalRecordSegmentEncryptionPacketBusiness, ILogicalRecordSegmentTrailerBusiness logicalRecordSegmentTrailerBusiness)
        {
            _logicalRecordSegmentHeaderBusiness = logicalRecordSegmentHeaderBusiness;
            _logicalRecordSegmentEncryptionPacketBusiness = logicalRecordSegmentEncryptionPacketBusiness;
            _logicalRecordSegmentTrailerBusiness = logicalRecordSegmentTrailerBusiness;
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

            var body = dlisStream.ReadBytes(bodySize);
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
