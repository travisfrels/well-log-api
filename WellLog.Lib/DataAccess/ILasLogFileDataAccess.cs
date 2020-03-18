using WellLog.Lib.Models;

namespace WellLog.Lib.DataAccess
{
    public interface ILasLogFileDataAccess
    {
        LasLog Read(string fileName);
    }
}