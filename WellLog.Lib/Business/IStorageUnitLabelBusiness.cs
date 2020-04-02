using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IStorageUnitLabelBusiness
    {
        StorageUnitLabel ReadStorageUnitLabel(Stream dlisStream);
    }
}