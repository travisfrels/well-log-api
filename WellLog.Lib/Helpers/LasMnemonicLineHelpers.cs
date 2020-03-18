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
    }
}
