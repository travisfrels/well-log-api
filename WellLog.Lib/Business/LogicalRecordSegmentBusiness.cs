using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentBusiness : ILogicalRecordSegmentBusiness
    {
        private readonly ILogicalRecordSegmentHeaderBusiness _logicalRecordSegmentHeaderBusiness;

        public LogicalRecordSegmentBusiness(ILogicalRecordSegmentHeaderBusiness logicalRecordSegmentHeaderBusiness)
        {
            _logicalRecordSegmentHeaderBusiness = logicalRecordSegmentHeaderBusiness;
        }

        public LogicalRecordSegment ReadLogicalRecordSegment(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new LogicalRecordSegment
            {
                Header = _logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream)
            };
        }
    }
}
