using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public interface IDlisLogFileDataAccess
    {
        DlisLog Read(string fileName);
    }
}
