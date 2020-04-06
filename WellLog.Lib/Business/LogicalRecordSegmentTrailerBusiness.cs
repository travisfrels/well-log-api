using System;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentTrailerBusiness : ILogicalRecordSegmentTrailerBusiness
    {
        public const byte DEFAULT_PADDING = 0;
        public const ushort DEFAULT_CHECKSUM = 0;
        public const ushort DEFAULT_TRAILING_LENGTH = 0;

        public LogicalRecordSegmentTrailer ReadLogicalRecordSegmentTrailer(Stream dlisStream, LogicalRecordSegmentHeader logicalRecordSegmentHeader)
        {
            if (dlisStream == null) { return null; }
            if (logicalRecordSegmentHeader == null) { throw new ArgumentNullException(nameof(logicalRecordSegmentHeader)); }

            return new LogicalRecordSegmentTrailer
            {
                Padding = logicalRecordSegmentHeader.Padding ? dlisStream.ReadUSHORT() : DEFAULT_PADDING,
                Checksum = logicalRecordSegmentHeader.Checksum ? dlisStream.ReadUNORM() : DEFAULT_CHECKSUM,
                TrailingLength = logicalRecordSegmentHeader.TrailingLength ? dlisStream.ReadUNORM() : DEFAULT_TRAILING_LENGTH
            };
        }
    }
}
