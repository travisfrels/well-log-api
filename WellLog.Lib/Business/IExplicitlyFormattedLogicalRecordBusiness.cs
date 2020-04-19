using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IExplicitlyFormattedLogicalRecordBusiness
    {
        ExplicitlyFormattedLogicalRecord ReadExplicitlyFormattedLogicalRecord(Stream dlisStream);
    }
}