using WellLog.Lib.Enumerations;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public interface ILasSectionLineBusiness
    {
        LasSectionType ToSectionTypeFromLasLine(string lasLine);
        LasMnemonicLine ToMnemonicLineFromLasLine(string lasLine);
        LasAsciiLogDataLine ToAsciiLogDataLineFromLasLine(string lasLine);
    }
}