using WellLog.Lib.Enumerations;
using System.Collections.Generic;

namespace WellLog.Lib.Models
{
    public class LasSection
    {
        public LasSectionType SectionType { get; set; }
        public IEnumerable<LasMnemonicLine> MnemonicsLines { get; set; }
        public IEnumerable<string> OtherLines { get; set; }
        public IEnumerable<LasAsciiLogDataLine> AsciiLogDataLines { get; set; }
    }
}
