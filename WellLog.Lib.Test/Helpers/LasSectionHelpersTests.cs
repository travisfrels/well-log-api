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

        private static readonly LasAsciiLogDataLine emptyAsciiLogDataLine = new LasAsciiLogDataLine
        {
            Values = new string[0]
        };

        private static readonly LasSection nullSection = null;
        private static readonly LasSection emptySection = new LasSection();

        private static readonly LasSection mnemonicSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[] { mnemonicLine }
        };

        private static readonly LasSection asciiLogDataSection = new LasSection
        {
            AsciiLogDataLines = new LasAsciiLogDataLine[] { asciiLogDataLine }
        };

        private static readonly LasSection asciiLogDataSectionWithEmptyLine = new LasSection
        {
            AsciiLogDataLines = new LasAsciiLogDataLine[] { emptyAsciiLogDataLine }
        };

        [Test]
        public void LasSectionHelpers_HasMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasMnemonic("MNEM"));
            Assert.IsFalse(emptySection.HasMnemonic("MNEM"));
            Assert.IsFalse(mnemonicSection.HasMnemonic(null));
            Assert.IsFalse(mnemonicSection.HasMnemonic(string.Empty));
            Assert.IsFalse(mnemonicSection.HasMnemonic("A"));
            Assert.IsTrue(mnemonicSection.HasMnemonic("MNEM"));
        }

        [Test]
        public void LasSectionHelpers_HasAnyMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasAnyMnemonic("MNEM"));
            Assert.IsFalse(emptySection.HasAnyMnemonic("MNEM"));
            Assert.IsFalse(mnemonicSection.HasAnyMnemonic(null));
            Assert.IsFalse(mnemonicSection.HasAnyMnemonic(string.Empty));
            Assert.IsFalse(mnemonicSection.HasAnyMnemonic("A"));
            Assert.IsTrue(mnemonicSection.HasAnyMnemonic("MNEM"));
            Assert.IsTrue(mnemonicSection.HasAnyMnemonic("A", "B", "MNEM"));
        }

        [Test]
        public void LasSectionHelpers_GetMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetMnemonic("MNEM"));
            Assert.IsNull(emptySection.GetMnemonic("MNEM"));
            Assert.IsNull(mnemonicSection.GetMnemonic(null));
            Assert.IsNull(mnemonicSection.GetMnemonic(string.Empty));
            Assert.IsNull(mnemonicSection.GetMnemonic("A"));
            Assert.AreSame(mnemonicLine, mnemonicSection.GetMnemonic("MNEM"));
        }

        [Test]
        public void LasSectionHelpers_FirstMnemonic_Pass()
        {
            Assert.IsNull(nullSection.FirstMnemonic());
            Assert.IsNull(emptySection.FirstMnemonic());
            Assert.AreSame(mnemonicLine, mnemonicSection.FirstMnemonic());
        }

        [Test]
        public void LasSectionHelpers_EmptyAsciiLogDataLineCount_Pass()
        {
            Assert.AreEqual(0, nullSection.EmptyAsciiLogDataLineCount());
            Assert.AreEqual(0, emptySection.EmptyAsciiLogDataLineCount());
            Assert.AreEqual(0, asciiLogDataSection.EmptyAsciiLogDataLineCount());
            Assert.AreEqual(1, asciiLogDataSectionWithEmptyLine.EmptyAsciiLogDataLineCount());
        }
    }
}
