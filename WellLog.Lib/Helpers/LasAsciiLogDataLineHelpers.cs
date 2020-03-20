using WellLog.Lib.Models;
using System.Linq;

namespace WellLog.Lib.Helpers
{
    public static class LasAsciiLogDataLineHelpers
    {
        public static bool IsEmpty(this LasAsciiLogDataLine lasAsciiLogDataLine)
        {
            if (lasAsciiLogDataLine == null) { return true; }
            if (lasAsciiLogDataLine.Values == null) { return true; }
            return lasAsciiLogDataLine.Values.Count() < 1;
        }
    }
}
