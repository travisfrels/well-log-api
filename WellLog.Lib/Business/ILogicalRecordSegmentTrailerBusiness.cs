using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface ILogicalRecordSegmentTrailerBusiness
    {
        LogicalRecordSegmentTrailer ReadLogicalRecordSegmentTrailer(Stream dlisStream, LogicalRecordSegmentHeader logicalRecordSegmentHeader);
    }
}
