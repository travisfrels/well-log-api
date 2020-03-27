using System.Linq;
using WellLog.Lib.Models;

namespace WellLog.Lib.Helpers
{
    public static class LasMnemonicLineHelpers
    {
        public static bool IsMnemonic(this LasMnemonicLine lasMnemonicLine, params string[] mnemonics)
        {
            if (lasMnemonicLine == null) { return false; }
            if (mnemonics == null) { return false; }
            return mnemonics.Any(x => string.Compare(x, lasMnemonicLine.Mnemonic, true) == 0);
        }

        public static void SwapDataDescription(this LasMnemonicLine lasMnemonicLine)
        {
            if (lasMnemonicLine == null) { return; }
            var temp = lasMnemonicLine.Data;
            lasMnemonicLine.Data = lasMnemonicLine.Description;
            lasMnemonicLine.Description = temp;
        }

        public static string GetLasLine(this LasMnemonicLine lasMnemonicLine, int mnemonicWidth, int unitWidth, int dataWidth)
        {
            return string.Format
                (
                    " {0}.{1} {2}:{3}",
                    lasMnemonicLine.Mnemonic.PadRight(mnemonicWidth),
                    lasMnemonicLine.Units.PadRight(unitWidth),
                    lasMnemonicLine.Data.PadRight(dataWidth),
                    lasMnemonicLine.Description
                );
        }
    }
}
