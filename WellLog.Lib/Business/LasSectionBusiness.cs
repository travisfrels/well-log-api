using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class LasSectionBusiness : ILasSectionBusiness
    {
        private const string DIVIDER = "#-------------------------------------------------------------------------------";

        private readonly ILasSectionLineBusiness _lasSectionLineBusiness;

        public LasSectionBusiness(ILasSectionLineBusiness lasSectionLineBusiness)
        {
            _lasSectionLineBusiness = lasSectionLineBusiness;
        }

        public IEnumerable<LasMnemonicLine> ReadMnemonicSection(Stream lasStream)
        {
            if (lasStream == null) { return null; }

            var mnemonicLines = new List<LasMnemonicLine>();
            var lasLine = lasStream.ReadLasLine();
            while (!lasLine.IsLasSectionHeader() && lasLine != null)
            {
                if (!(string.IsNullOrWhiteSpace(lasLine) || lasLine.IsLasComment()))
                {
                    mnemonicLines.Add(_lasSectionLineBusiness.ToMnemonicLineFromLasLine(lasLine));
                }
                lasLine = lasStream.ReadLasLine();
            }
            if (lasLine.IsLasSectionHeader()) { lasStream.SeekBackLine(lasLine); }
            return mnemonicLines.ToArray();
        }

        public IEnumerable<LasAsciiLogDataLine> ReadAsciiLogDataSection(Stream lasStream)
        {
            if (lasStream == null) { return null; }

            var asciiLogDataLines = new List<LasAsciiLogDataLine>();
            var lasLine = lasStream.ReadLasLine();
            while (!lasLine.IsLasSectionHeader() && lasLine != null)
            {
                if (string.IsNullOrWhiteSpace(lasLine)) { throw new LasLineException("Embedded blank lines are not allowed in the ~A section.", lasLine); }
                if (lasLine.IsLasComment()) { throw new LasLineException("Comments are not allowed in the ~A section.", lasLine); }
                asciiLogDataLines.Add(_lasSectionLineBusiness.ToAsciiLogDataLineFromLasLine(lasLine));
                lasLine = lasStream.ReadLasLine();
            }

            if (lasLine.IsLasSectionHeader()) { lasStream.SeekBackLine(lasLine); }
            return asciiLogDataLines.ToArray();
        }

        public IEnumerable<string> ReadOtherSection(Stream lasStream)
        {
            if (lasStream == null) { return null; }

            var otherLines = new List<string>();
            var lasLine = lasStream.ReadLasLine();
            while (!lasLine.IsLasSectionHeader() && lasLine != null)
            {
                if (!(string.IsNullOrWhiteSpace(lasLine) || lasLine.IsLasComment()))
                {
                    otherLines.Add(lasLine);
                }
                lasLine = lasStream.ReadLasLine();
            }
            if (lasLine.IsLasSectionHeader()) { lasStream.SeekBackLine(lasLine); }
            return otherLines.ToArray();
        }

        public LasSection ReadSection(Stream lasStream)
        {
            if (lasStream == null) { return null; }

            var lasLine = lasStream.ReadLasLine();
            if (!lasLine.IsLasSectionHeader()) { throw new LasStreamException("Stream is not positioned at a section header."); }

            var lasSection = new LasSection
            {
                SectionType = _lasSectionLineBusiness.ToSectionTypeFromLasLine(lasLine)
            };

            switch (lasSection.SectionType)
            {
                case LasSectionType.VersionInformation:
                case LasSectionType.WellInformation:
                case LasSectionType.CurveInformation:
                case LasSectionType.ParameterInformation:
                    lasSection.MnemonicsLines = ReadMnemonicSection(lasStream);
                    break;
                case LasSectionType.OtherInformation:
                    lasSection.OtherLines = ReadOtherSection(lasStream);
                    break;
                case LasSectionType.AsciiLogData:
                    lasSection.AsciiLogDataLines = ReadAsciiLogDataSection(lasStream);
                    break;
                default:
                    throw new LasStreamException("Invalid section header.");
            }

            return lasSection;
        }

        public string GetMnemonicSectionLabel(int mnemonicWidth, int unitWidth, int dataWidth)
        {
            if (mnemonicWidth < 0) { throw new ArgumentOutOfRangeException(nameof(mnemonicWidth), mnemonicWidth, "mnemonic width is less than zero"); }
            if (unitWidth < 0) { throw new ArgumentOutOfRangeException(nameof(unitWidth), unitWidth, "unit width is less than zero"); }
            if (dataWidth < 0) { throw new ArgumentOutOfRangeException(nameof(dataWidth), dataWidth, "data width is less than zero"); }

            return string.Format
                (
                    "#{0}.{1} {2}:DESCRIPTION",
                    "MNEM".PadRight(mnemonicWidth),
                    "UNITS".PadRight(unitWidth),
                    "DATA".PadRight(dataWidth)
                );
        }

        public void WriteMnemonicSection(Stream lasStream, IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (lasStream == null) { return; }
            if (mnemonicLines == null || !mnemonicLines.Any()) { return; }

            var mnemonicWidth = Math.Max(4, mnemonicLines.MaxMnemonicWidth());
            var unitWidth = Math.Max(5, mnemonicLines.MaxUnitsWidth());
            var dataWidth = Math.Max(4, mnemonicLines.MaxDataWidth());

            lasStream.WriteLasLine(DIVIDER);
            lasStream.WriteLasLine(GetMnemonicSectionLabel(mnemonicWidth, unitWidth, dataWidth));
            lasStream.WriteLasLine(DIVIDER);
            foreach (var line in mnemonicLines)
            {
                lasStream.WriteLasLine(line.GetLasLine(mnemonicWidth, unitWidth, dataWidth));
            }
            lasStream.WriteLasLine(string.Empty);
        }

        public void WriteAsciiLogDataSection(Stream lasStream, IEnumerable<LasAsciiLogDataLine> asciiLogDataLines)
        {
            if (lasStream == null) { return; }
            if (asciiLogDataLines == null || !asciiLogDataLines.Any()) { return; }

            var valueWidth = 1 + asciiLogDataLines.MaxValueWidth();
            foreach (var line in asciiLogDataLines)
            {
                lasStream.WriteLasLine(string.Join(' ', line.Values.Select(x => x.PadLeft(valueWidth))));
            }
        }

        public void WriteOtherSection(Stream lasStream, IEnumerable<string> lasLines)
        {
            if (lasStream == null) { return; }
            if (lasLines == null || !lasLines.Any()) { return; }

            lasStream.WriteLasLine(DIVIDER);
            foreach (var lasLine in lasLines)
            {
                lasStream.WriteLasLine(lasLine);
            }
            lasStream.WriteLasLine(string.Empty);
        }

        public void WriteSection(Stream lasStream, LasSection lasSection)
        {
            if (lasStream == null) { return; }
            if (lasSection == null) { return; }

            switch(lasSection.SectionType)
            {
                case LasSectionType.VersionInformation:
                    lasStream.WriteLasLine("~VERSION INFORMATION");
                    WriteMnemonicSection(lasStream, lasSection.MnemonicsLines);
                    break;
                case LasSectionType.WellInformation:
                    lasStream.WriteLasLine("~WELL INFORMATION");
                    WriteMnemonicSection(lasStream, lasSection.MnemonicsLines);
                    break;
                case LasSectionType.CurveInformation:
                    lasStream.WriteLasLine("~CURVE INFORMATION");
                    WriteMnemonicSection(lasStream, lasSection.MnemonicsLines);
                    break;
                case LasSectionType.ParameterInformation:
                    lasStream.WriteLasLine("~PARAMETER INFORMATION");
                    WriteMnemonicSection(lasStream, lasSection.MnemonicsLines);
                    break;
                case LasSectionType.OtherInformation:
                    lasStream.WriteLasLine("~OTHER INFORMATION");
                    WriteOtherSection(lasStream, lasSection.OtherLines);
                    break;
                case LasSectionType.AsciiLogData:
                    lasStream.WriteLasLine("~ASCII LOG DATA");
                    WriteAsciiLogDataSection(lasStream, lasSection.AsciiLogDataLines);
                    break;
            }
        }
    }
}
