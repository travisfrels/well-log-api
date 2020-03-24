using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public class AsciiLogDataBusiness : IAsciiLogDataBusiness
    {
        public void UnWrapAsciiLogData(LasSection lasSection)
        {
            if (lasSection == null) { return; }
            if (lasSection.AsciiLogDataLines == null) { return; }

            var asciiLogDataLines = new List<LasAsciiLogDataLine>();
            List<string> lineValues = null;
            foreach (var line in lasSection.AsciiLogDataLines)
            {
                var valueCount = line.Values.Count();
                if (valueCount < 1) { continue; }
                if (valueCount == 1)
                {
                    if (lineValues != null) { asciiLogDataLines.Add(new LasAsciiLogDataLine { Values = lineValues.ToArray() }); }
                    lineValues = new List<string>();
                }

                if (lineValues == null) { throw new LasLogFormatException("Invalid line wrapping.  In wrap mode, the index channel must be on its own line."); }
                lineValues.AddRange(line.Values);
            }
            if (lineValues != null) { asciiLogDataLines.Add(new LasAsciiLogDataLine { Values = lineValues.ToArray() }); }

            lasSection.AsciiLogDataLines = asciiLogDataLines.ToArray();
        }
    }
}
