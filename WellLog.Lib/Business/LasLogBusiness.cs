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
    public class LasLogBusiness : ILasLogBusiness
    {
        private readonly ILasSectionBusiness _lasSectionBusiness;

        public LasLogBusiness(ILasSectionBusiness lasSectionBusiness)
        {
            _lasSectionBusiness = lasSectionBusiness;
        }

        private void UnWrapAsciiLogData(LasLog lasLog)
        {
            if (!lasLog.UsesLineWrap()) { return; }

            var asciiLogDataSection = lasLog.GetSection(LasSectionType.AsciiLogData);
            if (asciiLogDataSection == null) { return; }

            var asciiLogDataLines = new List<LasAsciiLogDataLine>();
            List<string> lineValues = null;
            foreach (var line in asciiLogDataSection.AsciiLogDataLines)
            {
                var valueCount = line.Values.Count();
                if (valueCount < 1) { continue; }
                if (valueCount == 1)
                {
                    if (lineValues != null) { asciiLogDataLines.Add(new LasAsciiLogDataLine { Values = lineValues.ToArray() }); }
                    lineValues = new List<string>();
                }

                if (lineValues == null) { throw new LasLogFormatException("Invalid line wrapping.  In wrap mode, the index channel must be on its own line.", lasLog); }
                lineValues.AddRange(line.Values);
            }
            if (lineValues != null) { asciiLogDataLines.Add(new LasAsciiLogDataLine { Values = lineValues.ToArray() }); }

            asciiLogDataSection.AsciiLogDataLines = asciiLogDataLines.ToArray();
        }

        public LasLog ReadStream(Stream lasStream)
        {
            if (lasStream == null) { throw new ArgumentNullException(nameof(lasStream)); }

            var lasLog = new LasLog();
            var sections = new List<LasSection>();

            lasStream.Seek(0, SeekOrigin.Begin);
            var lasLine = lasStream.ReadLasLine();
            while (!string.IsNullOrEmpty(lasLine))
            {
                if (lasLine.IsLasSectionHeader())
                {
                    lasStream.SeekBackLine(lasLine);
                    sections.Add(_lasSectionBusiness.ReadSection(lasStream));
                }

                lasLine = lasStream.ReadLasLine();
            }
            lasLog.Sections = sections.ToArray();

            UnWrapAsciiLogData(lasLog);

            return lasLog;
        }
    }
}
