using WellLog.Lib.Models;

namespace WellLog.Lib.Helpers
{
    public static class LasCurveInformationHelpers
    {
        private const string MNEM_DEPT = "DEPT";
        private const string MNEM_DEPTH = "DEPTH";
        private const string MNEM_TIME = "TIME";
        private const string MNEM_INDEX = "INDEX";

        public static bool HasDepthMnemonic(this LasSection lasSection)
        {
            return lasSection.HasAnyMnemonic(MNEM_DEPT, MNEM_DEPTH);
        }

        public static bool HasTimeMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_TIME);
        }

        public static bool HasIndexMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_INDEX);
        }

        public static bool HasIndexChannel(this LasSection lasSection)
        {
            return lasSection.FirstMnemonic().IsMnemonic(MNEM_DEPT, MNEM_DEPTH, MNEM_TIME, MNEM_INDEX);
        }
    }
}
