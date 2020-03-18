using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasSectionHelpersTests
    {
        private static readonly LasMnemonicLine mnemonicLine = new LasMnemonicLine
        {
            Mnemonic = "MNEM",
            Units = "UNIT",
            Data = "DATA",
            Description = "DESCRIPTION"
        };

        private static readonly LasAsciiLogDataLine asciiLogDataLine = new LasAsciiLogDataLine
        {
            Values = new string[] { "100.0", "-999.25" }
        };

        private static readonly LasSection nullSection = null;
        private static readonly LasSection noLinesSection = new LasSection();

        private static readonly LasSection mnemonicLineSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[] { mnemonicLine }
        };

        private static readonly LasSection asciiLogDataLineSection = new LasSection
        {
            AsciiLogDataLines = new LasAsciiLogDataLine[] { asciiLogDataLine }
        };

        [Test]
        public void LasSectionHelpers_HasMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasMnemonic("MNEM"));
            Assert.IsFalse(noLinesSection.HasMnemonic("MNEM"));
            Assert.IsFalse(mnemonicLineSection.HasMnemonic(null));
            Assert.IsFalse(mnemonicLineSection.HasMnemonic(string.Empty));
            Assert.IsFalse(mnemonicLineSection.HasMnemonic("A"));
            Assert.IsTrue(mnemonicLineSection.HasMnemonic("MNEM"));
        }

        [Test]
        public void LasSectionHelpers_HasAnyMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasAnyMnemonic("MNEM"));
            Assert.IsFalse(noLinesSection.HasAnyMnemonic("MNEM"));
            Assert.IsFalse(mnemonicLineSection.HasAnyMnemonic(null));
            Assert.IsFalse(mnemonicLineSection.HasAnyMnemonic(string.Empty));
            Assert.IsFalse(mnemonicLineSection.HasAnyMnemonic("A"));
            Assert.IsTrue(mnemonicLineSection.HasAnyMnemonic("MNEM"));
            Assert.IsTrue(mnemonicLineSection.HasAnyMnemonic("A", "B", "MNEM"));
        }

        [Test]
        public void LasSectionHelpers_GetMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetMnemonic("MNEM"));
            Assert.IsNull(noLinesSection.GetMnemonic("MNEM"));
            Assert.IsNull(mnemonicLineSection.GetMnemonic(null));
            Assert.IsNull(mnemonicLineSection.GetMnemonic(string.Empty));
            Assert.IsNull(mnemonicLineSection.GetMnemonic("A"));
            Assert.AreSame(mnemonicLine, mnemonicLineSection.GetMnemonic("MNEM"));
        }

        [Test]
        public void LasSectionHelpers_FirstMnemonic_Pass()
        {
            Assert.IsNull(nullSection.FirstMnemonic());
            Assert.IsNull(noLinesSection.FirstMnemonic());
            Assert.AreSame(mnemonicLine, mnemonicLineSection.FirstMnemonic());
        }

        [Test]
        public void LasSectionHelpers_MnemonicCount_Pass()
        {
            Assert.AreEqual(0, nullSection.MnemonicCount());
            Assert.AreEqual(0, noLinesSection.MnemonicCount());
            Assert.AreEqual(1, mnemonicLineSection.MnemonicCount());
        }

        [Test]
        public void LasSectionHelpers_AsciiLogDataCount_Pass()
        {
            Assert.AreEqual(0, nullSection.AsciiLogDataCount());
            Assert.AreEqual(0, noLinesSection.AsciiLogDataCount());
            Assert.AreEqual(1, asciiLogDataLineSection.AsciiLogDataCount());
        }
    }
}
