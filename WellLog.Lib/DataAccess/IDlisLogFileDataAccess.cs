using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public interface IDlisLogFileDataAccess
    {
        StorageUnit Read(string fileName);
    }
}