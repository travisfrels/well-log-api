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

        public int GetTrailerSize(LogicalRecordSegmentHeader header, LogicalRecordSegmentTrailer trailer)
        {
            if (header == null) { return 0; }
            if (trailer == null) { return 0; }

            var trailerSize = 0;
            if (header.TrailingLength) { trailerSize += 2; }
            if (header.Checksum) { trailerSize += 2; }
            if (header.Padding) { trailerSize += trailer.PadCount; }

            return trailerSize;
        }

        public LogicalRecordSegment ReadLogicalRecordSegment(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.IsAtEndOfStream()) { return null; }

            var header = _logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream);

            var isEFLR = header.LogicalRecordStructure;

            var bodySize = header.LogicalRecordSegmentLength - 4;

            var encryptionPacket = new LogicalRecordSegmentEncryptionPacket();
            if (header.EncryptionPacket)
            {
                encryptionPacket = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream);
                bodySize -= encryptionPacket.Size;
            }

            dlisStream.Seek(bodySize, SeekOrigin.Current);
            var trailer = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, header);
            var trailerSize = GetTrailerSize(header, trailer);
            bodySize -= trailerSize;

            dlisStream.Seek(-(bodySize + trailerSize), SeekOrigin.Current);
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
