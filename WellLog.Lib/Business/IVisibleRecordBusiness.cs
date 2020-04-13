using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IVisibleRecordBusiness
    {
        VisibleRecord ReadVisibleRecord(Stream dlisStream);
    }
}
