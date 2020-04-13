using WellLog.Lib.Models.DLIS;

namespace DLIS
{
    public interface IDlisLogPrinter
    {
        void PrintLasLog(StorageUnit storageUnit);
    }
}
