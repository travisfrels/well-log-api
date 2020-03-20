using System.Linq;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class LasSectionLineBusiness : ILasSectionLineBusiness
    {
        private const char VERSION_INFORMATION = 'V';
        private const char WELL_INFORMATION = 'W';
        private const char CURVE_INFORMATION = 'C';
        private const char PARAMETER_INFORMATION = 'P';
        private const char OTHER_INFORMATION = 'O';
        private const char ASCII_LOG_DATA = 'A';

        public LasSectionType ToSectionTypeFromLasLine(string lasLine)
        {
            if (!lasLine.IsLasSectionHeader()) { throw new LasLineException("Not a section header.", lasLine); }

            var trimmedLasLine = lasLine.TrimStart();
            if (trimmedLasLine.Length < 2) { throw new LasLineException("No section identifier.", lasLine); }

            return (trimmedLasLine[1]) switch
            {
                VERSION_INFORMATION => LasSectionType.VersionInformation,
                WELL_INFORMATION => LasSectionType.WellInformation,
                CURVE_INFORMATION => LasSectionType.CurveInformation,
                PARAMETER_INFORMATION => LasSectionType.ParameterInformation,
                OTHER_INFORMATION => LasSectionType.OtherInformation,
                ASCII_LOG_DATA => LasSectionType.AsciiLogData,
                _ => throw new LasLineException("Invalid section identifier.", lasLine),
            };
        }

        public LasMnemonicLine ToMnemonicLineFromLasLine(string lasLine)
        {
            if (string.IsNullOrWhiteSpace(lasLine) || lasLine.IsLasComment()) { return null; }
            if (lasLine.IsLasSectionHeader()) { throw new LasLineException("Invalid mnemonic line.", lasLine); }

            var indexOfFirstDot = -1;
            var indexOfFirstSpaceAfterDot = -1;
            var indexOfLastColon = -1;

            for (var i = 0; i < lasLine.Length; i++)
            {
                if (lasLine[i] == '.' && indexOfFirstDot == -1) { indexOfFirstDot = i; }
                if (lasLine[i] == ' ' && indexOfFirstDot > -1 && indexOfFirstSpaceAfterDot == -1) { indexOfFirstSpaceAfterDot = i; }
                if (lasLine[i] == ':') { indexOfLastColon = i; }
            }

            if (indexOfFirstDot == -1 || indexOfFirstSpaceAfterDot == -1 || indexOfLastColon == -1)
            {
                throw new LasLineException("Not a delimited line.", lasLine);
            }

            var response = new LasMnemonicLine
            {
                Mnemonic = lasLine[0..indexOfFirstDot].Trim(),
                Units = lasLine[(indexOfFirstDot + 1)..indexOfFirstSpaceAfterDot].Trim(),
                Data = lasLine[(indexOfFirstSpaceAfterDot + 1)..indexOfLastColon].Trim(),
                Description = lasLine[(indexOfLastColon + 1)..lasLine.Length].Trim()
            };
            return response;
        }

        public LasAsciiLogDataLine ToAsciiLogDataLineFromLasLine(string lasLine)
        {
            if (string.IsNullOrWhiteSpace(lasLine) || lasLine.IsLasComment() || lasLine.IsLasSectionHeader())
            {
                throw new LasLineException("Invalid ASCII Log Data.", lasLine);
            }

            return new LasAsciiLogDataLine
            {
                Values = lasLine.Split().Where(x => !string.IsNullOrEmpty(x))
            };
        }
    }
}
