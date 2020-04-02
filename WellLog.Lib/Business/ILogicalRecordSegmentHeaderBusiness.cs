using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface ILogicalRecordSegmentHeaderBusiness
    {
        LogicalRecordSegmentHeader ReadLogicalRecordSegmentHeader(Stream dlisStream);
    }
}