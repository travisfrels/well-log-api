using WellLog.Lib.Models;
using System.Linq;

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
    }
}
