using WellLog.Lib.Models;
using System.Linq;
using System.Collections.Generic;

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

        public static int MaxMnemonicWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null) { return 0; }
            if (!mnemonicLines.Any()) { return 0; }

            var validMnemonicLines = mnemonicLines.Where(x => !string.IsNullOrEmpty(x.Mnemonic));
            if (!validMnemonicLines.Any()) { return 0; }

            return validMnemonicLines.Max(x => x.Mnemonic.Length);
        }

        public static int MaxUnitsWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null) { return 0; }
            if (!mnemonicLines.Any()) { return 0; }

            var validMnemonicLines = mnemonicLines.Where(x => !string.IsNullOrEmpty(x.Units));
            if (!validMnemonicLines.Any()) { return 0; }

            return validMnemonicLines.Max(x => x.Units.Length);
        }

        public static int MaxDataWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null) { return 0; }
            if (!mnemonicLines.Any()) { return 0; }

            var validMnemonicLines = mnemonicLines.Where(x => !string.IsNullOrEmpty(x.Data));
            if (!validMnemonicLines.Any()) { return 0; }

            return validMnemonicLines.Max(x => x.Data.Length);
        }

        public static int MaxDescriptionWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null) { return 0; }
            if (!mnemonicLines.Any()) { return 0; }

            var validMnemonicLines = mnemonicLines.Where(x => !string.IsNullOrEmpty(x.Description));
            if (!validMnemonicLines.Any()) { return 0; }

            return validMnemonicLines.Max(x => x.Description.Length);
        }
    }
}
