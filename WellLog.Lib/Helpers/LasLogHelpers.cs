using WellLog.Lib.Enumerations;
using WellLog.Lib.Models;
using System.Linq;

namespace WellLog.Lib.Helpers
{
    public static class LasLogHelpers
    {
        private const string MNEM_WRAP = "WRAP";
        private const string WRAP_YES = "YES";

        public static bool HasSection(this LasLog lasLog, LasSectionType lasSectionType)
        {
            if (lasLog == null) { return false; }
            if (lasLog.Sections == null) { return false; }
            return lasLog.Sections.Any(x => x.SectionType == lasSectionType);
        }

        public static LasSection GetSection(this LasLog lasLog, LasSectionType lasSectionType)
        {
            if (lasLog == null) { return null; }
            if (lasLog.Sections == null) { return null; }
            return lasLog.Sections.FirstOrDefault(x => x.SectionType == lasSectionType);
        }

        public static int SectionCount(this LasLog lasLog, LasSectionType lasSectionType)
        {
            if (lasLog == null) { return 0; }
            if (lasLog.Sections == null) { return 0; }
            return lasLog.Sections.Count(x => x.SectionType == lasSectionType);
        }

        public static bool UsesLineWrap(this LasLog lasLog)
        {
            var versionInformation = lasLog.GetSection(LasSectionType.VersionInformation);
            if (versionInformation == null) { return false; }

            var wrapMnemonic = versionInformation.GetMnemonic(MNEM_WRAP);
            if (wrapMnemonic == null) { return false; }

            if (string.Compare(wrapMnemonic.Data, WRAP_YES, true) == 0) { return true; }
            return false;
        }

        public static bool AsciiLogDataHasCurveChannels(this LasLog lasLog)
        {
            if (lasLog == null) { return true; }

            var curveInformation = lasLog.GetSection(LasSectionType.CurveInformation);
            if (curveInformation == null) { return true; }
            if (curveInformation.MnemonicsLines == null) { return true; }

            var asciiLogData = lasLog.GetSection(LasSectionType.AsciiLogData);
            if (asciiLogData == null) { return true; }
            if (asciiLogData.AsciiLogDataLines == null) { return true; }

            if (asciiLogData.AsciiLogDataLines.Any(x => x.Values.Count() > 0 && x.Values.Count() != curveInformation.MnemonicsLines.Count()))
            {
                return false;
            }

            return true;
        }

        public static string WellIdentifier(this LasLog lasLog)
        {
            if (lasLog == null) { return string.Empty; }

            var wellInformationSection = lasLog.GetSection(LasSectionType.WellInformation);
            if (wellInformationSection == null) { return string.Empty; }

            var uwiMnemonic = wellInformationSection.GetUwiMnemonic();
            if (uwiMnemonic != null) { return uwiMnemonic.Data; }

            var apiMnemonic = wellInformationSection.GetApiMnemonic();
            if (apiMnemonic != null) { return apiMnemonic.Data; }

            return string.Empty;
        }
    }
}
