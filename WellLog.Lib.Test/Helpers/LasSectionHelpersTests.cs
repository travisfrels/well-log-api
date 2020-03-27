using NUnit.Framework;
using System.Collections.Generic;
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

        [Test]
        public void LasSectionhelpers_MaxMnemonicWidth_Pass_NullEnumerable()
        {
            IEnumerable<LasMnemonicLine> mnemonicLines = null;
            Assert.AreEqual(0, mnemonicLines.MaxMnemonicWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxMnemonicWidth_Pass_EmptyEnumerable()
        {
            var mnemonicLines = new LasMnemonicLine[0];
            Assert.AreEqual(0, mnemonicLines.MaxMnemonicWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxMnemonicWidth_Pass_NoMnemonic()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine() };
            Assert.AreEqual(0, mnemonicLines.MaxMnemonicWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxMnemonicWidth_Pass_EmptyMnemonic()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = string.Empty } };
            Assert.AreEqual(0, mnemonicLines.MaxMnemonicWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxMnemonicWidth_Pass()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "MNEM" } };
            Assert.AreEqual(4, mnemonicLines.MaxMnemonicWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxUnitsWidth_Pass_NullEnumerable()
        {
            IEnumerable<LasMnemonicLine> mnemonicLines = null;
            Assert.AreEqual(0, mnemonicLines.MaxUnitsWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxUnitsWidth_Pass_EmptyEnumerable()
        {
            var mnemonicLines = new LasMnemonicLine[0];
            Assert.AreEqual(0, mnemonicLines.MaxUnitsWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxUnitsWidth_Pass_NoUnits()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine() };
            Assert.AreEqual(0, mnemonicLines.MaxUnitsWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxUnitsWidth_Pass_EmptyUnits()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Units = string.Empty } };
            Assert.AreEqual(0, mnemonicLines.MaxUnitsWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxUnitsWidth_Pass()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Units = "UNIT" } };
            Assert.AreEqual(4, mnemonicLines.MaxUnitsWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxDataWidth_Pass_NullEnumerable()
        {
            IEnumerable<LasMnemonicLine> mnemonicLines = null;
            Assert.AreEqual(0, mnemonicLines.MaxDataWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxDataWidth_Pass_EmptyEnumerable()
        {
            var mnemonicLines = new LasMnemonicLine[0];
            Assert.AreEqual(0, mnemonicLines.MaxDataWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxDataWidth_Pass_NoData()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine() };
            Assert.AreEqual(0, mnemonicLines.MaxDataWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxDataWidth_Pass_EmptyData()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Data = string.Empty } };
            Assert.AreEqual(0, mnemonicLines.MaxDataWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxDataWidth_Pass()
        {
            var mnemonicLines = new LasMnemonicLine[] { new LasMnemonicLine { Data = "DATA" } };
            Assert.AreEqual(4, mnemonicLines.MaxDataWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass_NullEnumerable()
        {
            IEnumerable<LasAsciiLogDataLine> asciiLogDataLines = null;
            Assert.AreEqual(0, asciiLogDataLines.MaxValueWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass_EmptyEnumerable()
        {
            var asciiLogDataLines = new LasAsciiLogDataLine[0];
            Assert.AreEqual(0, asciiLogDataLines.MaxValueWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass_NullValuesArray()
        {
            var asciiLogDataLines = new LasAsciiLogDataLine[] { new LasAsciiLogDataLine() };
            Assert.AreEqual(0, asciiLogDataLines.MaxValueWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass_EmptyValuesArray()
        {
            var asciiLogDataLines = new LasAsciiLogDataLine[] { new LasAsciiLogDataLine { Values = new string[0] } };
            Assert.AreEqual(0, asciiLogDataLines.MaxValueWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass_ValuesArrayHasNullOrEmptyString()
        {
            var asciiLogDataLines = new LasAsciiLogDataLine[] { new LasAsciiLogDataLine { Values = new string[] { null, string.Empty } } };
            Assert.AreEqual(0, asciiLogDataLines.MaxValueWidth());
        }

        [Test]
        public void LasSectionhelpers_MaxValueWidth_Pass()
        {
            var asciiLogDataLines = new LasAsciiLogDataLine[] { new LasAsciiLogDataLine { Values = new string[] { "-999.25", "-999.2", "-9999" } } };
            Assert.AreEqual(7, asciiLogDataLines.MaxValueWidth());
        }
    }
}
