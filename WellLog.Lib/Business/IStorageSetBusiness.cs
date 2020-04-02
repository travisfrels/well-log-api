using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IStorageSetBusiness
    {
        StorageSet ReadStream(Stream dlisStream);
    }
}