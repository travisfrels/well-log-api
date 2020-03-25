using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class WellInformationBusiness : IWellInformationBusiness
    {
        public void FixWellInformation(LasSection lasSection)
        {
            lasSection.GetCompanyMnemonic().SwapDataDescription();
            lasSection.GetWellMnemonic().SwapDataDescription();
            lasSection.GetFieldMnemonic().SwapDataDescription();
            lasSection.GetLocationMnemonic().SwapDataDescription();
            lasSection.GetProvinceMnemonic().SwapDataDescription();
            lasSection.GetCountyMnemonic().SwapDataDescription();
            lasSection.GetStateMnemonic().SwapDataDescription();
            lasSection.GetCountryMnemonic().SwapDataDescription();
            lasSection.GetServiceCompanyMnemonic().SwapDataDescription();
            lasSection.GetDateMnemonic().SwapDataDescription();
            lasSection.GetUwiMnemonic().SwapDataDescription();
            lasSection.GetApiMnemonic().SwapDataDescription();
        }
    }
}
