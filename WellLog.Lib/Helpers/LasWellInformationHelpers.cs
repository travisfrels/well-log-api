using WellLog.Lib.Models;

namespace WellLog.Lib.Helpers
{
    public static class LasWellInformationHelpers
    {
        private const string MNEM_STRT = "STRT";
        private const string MNEM_STOP = "STOP";
        private const string MNEM_STEP = "STEP";
        private const string MNEM_NULL = "NULL";
        private const string MNEM_COMP = "COMP";
        private const string MNEM_WELL = "WELL";
        private const string MNEM_FLD = "FLD";
        private const string MNEM_LOC = "LOC";
        private const string MNEM_PROV = "PROV";
        private const string MNEM_CNTY = "CNTY";
        private const string MNEM_STAT = "STAT";
        private const string MNEM_CTRY = "CTRY";
        private const string MNEM_SRVC = "SRVC";
        private const string MNEM_DATE = "DATE";
        private const string MNEM_UWI = "UWI";
        private const string MNEM_API = "API";

        public static bool HasStartMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_STRT);
        }

        public static bool HasStopMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_STOP);
        }

        public static bool HasStepMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_STEP);
        }

        public static bool HasNullMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_NULL);
        }

        public static bool HasCompanyMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_COMP);
        }

        public static bool HasWellMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_WELL);
        }

        public static bool HasFieldMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_FLD);
        }

        public static bool HasLocationMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_LOC);
        }

        public static bool HasProvinceMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_PROV);
        }

        public static bool HasCountyMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_CNTY);
        }

        public static bool HasStateMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_STAT);
        }

        public static bool HasCountryMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_CTRY);
        }

        public static bool HasAreaMnemonic(this LasSection lasSection)
        {
            return lasSection.HasAnyMnemonic(MNEM_PROV, MNEM_CNTY, MNEM_STAT, MNEM_CTRY);
        }

        public static bool HasServiceCompanyMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_SRVC);
        }

        public static bool HasDateMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_DATE);
        }

        public static bool HasUwiMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_UWI);
        }

        public static bool HasApiMnemonic(this LasSection lasSection)
        {
            return lasSection.HasMnemonic(MNEM_API);
        }

        public static bool HasIdentifierMnemonic(this LasSection lasSection)
        {
            return lasSection.HasAnyMnemonic(MNEM_UWI, MNEM_API);
        }

        public static LasMnemonicLine GetUwiMnemonic(this LasSection lasSection)
        {
            return lasSection.GetMnemonic(MNEM_UWI);
        }

        public static LasMnemonicLine GetApiMnemonic(this LasSection lasSection)
        {
            return lasSection.GetMnemonic(MNEM_API);
        }
    }
}
