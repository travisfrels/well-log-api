using System.IO;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentHeaderBusiness : ILogicalRecordSegmentHeaderBusiness
    {
        private readonly IUNORMReader _unormReader;
        private readonly IUSHORTReader _ushortReader;

        public LogicalRecordSegmentHeaderBusiness(IUNORMReader unormReader, IUSHORTReader ushortReader)
        {
            _unormReader = unormReader;
            _ushortReader = ushortReader;
        }

        public LogicalRecordSegmentHeader ReadLogicalRecordSegmentHeader(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.BytesRemaining() < 4) { return null; }

            return new LogicalRecordSegmentHeader
            {
                LogicalRecordSegmentLength = _unormReader.ReadUNORM(dlisStream),
                LogicalRecordSegmentAttributes = _ushortReader.ReadUSHORT(dlisStream),
                LogicalRecordType = _ushortReader.ReadUSHORT(dlisStream)
            };
        }
    }
}
