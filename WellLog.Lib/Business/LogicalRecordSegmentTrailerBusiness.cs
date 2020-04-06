using System;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentTrailerBusiness : ILogicalRecordSegmentTrailerBusiness
    {
        public LogicalRecordSegmentTrailer ReadLogicalRecordSegmentTrailer(Stream dlisStream, LogicalRecordSegmentHeader header)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.IsAtBeginningOfStream()) { return null; }
            if (header == null) { throw new ArgumentNullException(nameof(header)); }

            var trailer = new LogicalRecordSegmentTrailer();

            if (header.TrailingLength) { dlisStream.Seek(-2, SeekOrigin.Current); }
            if (header.Checksum) { dlisStream.Seek(-2, SeekOrigin.Current); }
            if (header.Padding)
            {
                dlisStream.Seek(-1, SeekOrigin.Current);
                trailer.PadCount = dlisStream.ReadUSHORT();

                dlisStream.Seek(-trailer.PadCount, SeekOrigin.Current);
                trailer.Padding = dlisStream.ReadBytes(trailer.PadCount);
            }
            if (header.Checksum) { trailer.Checksum = dlisStream.ReadUNORM(); }
            if (header.TrailingLength) { trailer.TrailingLength = dlisStream.ReadUNORM(); }

            return trailer;
        }
    }
}
