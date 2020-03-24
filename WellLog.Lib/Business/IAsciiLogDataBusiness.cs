using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public interface IAsciiLogDataBusiness
    {
        void UnWrapAsciiLogData(LasSection lasSection);
    }
}