using System.IO;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentHeaderBusiness : ILogicalRecordSegmentHeaderBusiness
    {
        public LogicalRecordSegmentHeader ReadLogicalRecordSegmentHeader(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.BytesRemaining() < 4) { return null; }

            return new LogicalRecordSegmentHeader
            {
                LogicalRecordSegmentLength = dlisStream.ReadUNORM(),
                LogicalRecordSegmentAttributes = dlisStream.ReadUSHORT(),
                LogicalRecordType = dlisStream.ReadUSHORT()
            };
        }
    }
}
