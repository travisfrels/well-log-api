using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public interface IDlisLogFileDataAccess
    {
        StorageSet Read(string fileName);
    }
}