using WellLog.Lib.Enumerations;
using WellLog.Lib.Models;

namespace WellLog.Lib.Helpers
{
    public static class LasVersionInformationHelpers
    {
        private const string MNEM_VERS = "VERS";
        private const string MNEM_WRAP = "WRAP";

        public static bool HasVersionMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_VERS);
        }

        public static bool HasWrapMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_WRAP);
        }

        public static LasMnemonicLine GetVersionMnemonic(this LasSection lasSection)
        {
            return lasSection.GetMnemonic(MNEM_VERS);
        }
    }
}
