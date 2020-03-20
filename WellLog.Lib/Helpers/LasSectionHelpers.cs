using WellLog.Lib.Models;
using System.Linq;

namespace WellLog.Lib.Helpers
{
    public static class LasSectionHelpers
    {
        public static bool HasMnemonic(this LasSection lasSection, string mnemonic)
        {
            if (lasSection == null) { return false; }
            if (lasSection.MnemonicsLines == null) { return false; }
            if (string.IsNullOrEmpty(mnemonic)) { return false; }
            return lasSection.MnemonicsLines.Any(x => string.Compare(x.Mnemonic, mnemonic, true) == 0);
        }

        public static bool HasAnyMnemonic(this LasSection lasSection, params string[] mnemonics)
        {
            if (lasSection == null) { return false; }
            if (lasSection.MnemonicsLines == null) { return false; }
            if (mnemonics == null) { return false; }
            return lasSection.MnemonicsLines.Any(x => mnemonics.Any(y => string.Compare(x.Mnemonic, y, true) == 0));
        }

        public static LasMnemonicLine GetMnemonic(this LasSection lasSection, string mnemonic)
        {
            if (lasSection == null) { return null; }
            if (lasSection.MnemonicsLines == null) { return null; }
            if (string.IsNullOrEmpty(mnemonic)) { return null; }
            return lasSection.MnemonicsLines.FirstOrDefault(x => string.Compare(x.Mnemonic, mnemonic, true) == 0);
        }

        public static LasMnemonicLine FirstMnemonic(this LasSection lasSection)
        {
            if (lasSection == null) { return null; }
            if (lasSection.MnemonicsLines == null) { return null; }
            return lasSection.MnemonicsLines.FirstOrDefault();
        }

        public static int EmptyAsciiLogDataLineCount(this LasSection lasSection)
        {
            if (lasSection == null) { return 0; }
            if (lasSection.AsciiLogDataLines == null) { return 0; }
            return lasSection.AsciiLogDataLines.Count(x => x.IsEmpty());
        }
    }
}
