using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface ILogicalRecordSegmentBusiness
    {
        LogicalRecordSegment ReadLogicalRecordSegment(Stream dlisStream);
    }
}