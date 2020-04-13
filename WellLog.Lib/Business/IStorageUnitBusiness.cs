using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IStorageUnitBusiness
    {
        StorageUnit ReadStorageUnit(Stream dlisStream);
    }
}
