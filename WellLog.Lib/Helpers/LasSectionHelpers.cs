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
            if (lasSection.MnemonicsLines == null || !lasSection.MnemonicsLines.Any()) { return false; }
            if (string.IsNullOrEmpty(mnemonic)) { return false; }
            return lasSection.MnemonicsLines.Any(x => string.Compare(x.Mnemonic, mnemonic, true) == 0);
        }

        public static bool HasAnyMnemonic(this LasSection lasSection, params string[] mnemonics)
        {
            if (lasSection == null) { return false; }
            if (lasSection.MnemonicsLines == null || !lasSection.MnemonicsLines.Any()) { return false; }
            if (mnemonics == null) { return false; }
            return lasSection.MnemonicsLines.Any(x => mnemonics.Any(y => string.Compare(x.Mnemonic, y, true) == 0));
        }

        public static LasMnemonicLine GetMnemonic(this LasSection lasSection, string mnemonic)
        {
            if (lasSection == null) { return null; }
            if (lasSection.MnemonicsLines == null || !lasSection.MnemonicsLines.Any()) { return null; }
            if (string.IsNullOrEmpty(mnemonic)) { return null; }
            return lasSection.MnemonicsLines.FirstOrDefault(x => string.Compare(x.Mnemonic, mnemonic, true) == 0);
        }

        public static LasMnemonicLine FirstMnemonic(this LasSection lasSection)
        {
            if (lasSection == null) { return null; }
            if (lasSection.MnemonicsLines == null || !lasSection.MnemonicsLines.Any()) { return null; }
            return lasSection.MnemonicsLines.FirstOrDefault();
        }

        public static int EmptyAsciiLogDataLineCount(this LasSection lasSection)
        {
            if (lasSection == null) { return 0; }
            if (lasSection.AsciiLogDataLines == null || !lasSection.AsciiLogDataLines.Any()) { return 0; }
            return lasSection.AsciiLogDataLines.Count(x => x.IsEmpty());
        }

        public static int MaxMnemonicWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null || !mnemonicLines.Any()) { return 0; }

            var values = mnemonicLines.Select(x => x.Mnemonic).Where(x => !string.IsNullOrEmpty(x));
            if (values == null || !values.Any()) { return 0; }

            return values.Max(x => x.Length);
        }

        public static int MaxUnitsWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null || !mnemonicLines.Any()) { return 0; }

            var values = mnemonicLines.Select(x => x.Units).Where(x => !string.IsNullOrEmpty(x));
            if (values == null || !values.Any()) { return 0; }

            return values.Max(x => x.Length);
        }

        public static int MaxDataWidth(this IEnumerable<LasMnemonicLine> mnemonicLines)
        {
            if (mnemonicLines == null || !mnemonicLines.Any()) { return 0; }

            var values = mnemonicLines.Select(x => x.Data).Where(x => !string.IsNullOrEmpty(x));
            if (values == null || !values.Any()) { return 0; }

            return values.Max(x => x.Length);
        }

        public static int MaxValueWidth(this IEnumerable<LasAsciiLogDataLine> asciiLogDataLines)
        {
            if (asciiLogDataLines == null || !asciiLogDataLines.Any()) { return 0; }

            var values = asciiLogDataLines
                .Where(x => x.Values != null && x.Values.Any())
                .SelectMany(x => x.Values)
                .Where(x => !string.IsNullOrEmpty(x));
            if (values == null || !values.Any()) { return 0; }

            return values.Max(x => x.Length);
        }
    }
}
