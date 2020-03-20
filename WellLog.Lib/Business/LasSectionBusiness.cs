using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class LasSectionBusiness : ILasSectionBusiness
    {
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
    }
}
