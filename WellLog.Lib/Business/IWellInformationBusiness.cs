using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public interface IWellInformationBusiness
    {
        void FixWellInformation(LasSection lasSection);
    }
}