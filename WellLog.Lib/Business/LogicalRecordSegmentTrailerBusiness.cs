using System.IO;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentTrailerBusiness : ILogicalRecordSegmentTrailerBusiness
    {
        private readonly IUSHORTReader _ushortReader;
        private readonly IUNORMReader _unormReader;

        public LogicalRecordSegmentTrailerBusiness(IUSHORTReader ushortReader, IUNORMReader unormReader)
        {
            _ushortReader = ushortReader;
            _unormReader = unormReader;
        }

        public LogicalRecordSegmentTrailer ReadLogicalRecordSegmentTrailer(Stream dlisStream, LogicalRecordSegmentHeader header)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.IsAtBeginningOfStream()) { return null; }
            if (header == null) { return null; }
            if (!header.HasTrailer()) { return null; }

            var trailer = new LogicalRecordSegmentTrailer();

            if (header.TrailingLength) { dlisStream.Seek(-2, SeekOrigin.Current); }
            if (header.Checksum) { dlisStream.Seek(-2, SeekOrigin.Current); }

            if (header.Padding)
            {
                dlisStream.Seek(-1, SeekOrigin.Current);
                trailer.PadCount = _ushortReader.ReadUSHORT(dlisStream);

                dlisStream.Seek(-trailer.PadCount, SeekOrigin.Current);
                trailer.Padding = dlisStream.ReadBytes(trailer.PadCount);
            }

            if (header.Checksum) { trailer.Checksum = _unormReader.ReadUNORM(dlisStream); }
            if (header.TrailingLength) { trailer.TrailingLength = _unormReader.ReadUNORM(dlisStream); }

            return trailer;
        }
    }
}
