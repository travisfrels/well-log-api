using NUnit.Framework;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasLogHelpersTests
    {
        private const string WELL_ID = "123456789012";

        private static readonly LasSection noWrapVersionSection = new LasSection
        {
            SectionType = LasSectionType.VersionInformation,
            MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "WRAP", Data = "NO" } }
        };

        private static readonly LasSection wrapVersionSection = new LasSection
        {
            SectionType = LasSectionType.VersionInformation,
            MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "WRAP", Data = "YES" } }
        };

        private static readonly LasSection uwiWellInformationSection = new LasSection
        {
            SectionType = LasSectionType.WellInformation,
            MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "UWI", Data = WELL_ID } }
        };

        private static readonly LasSection apiWellInformationSection = new LasSection
        {
            SectionType = LasSectionType.WellInformation,
            MnemonicsLines = new LasMnemonicLine[] { new LasMnemonicLine { Mnemonic = "API", Data = WELL_ID } }
        };

        private static readonly LasSection noIdWellInformationSection = new LasSection
        {
            SectionType = LasSectionType.WellInformation,
            MnemonicsLines = new LasMnemonicLine[0]
        };

        private static readonly LasSection twoChannelCurveSection = new LasSection
        {
            SectionType = LasSectionType.CurveInformation,
            MnemonicsLines = new LasMnemonicLine[]
            {
                new LasMnemonicLine { Mnemonic = "DEPTH", Units = "FEET" },
                new LasMnemonicLine { Mnemonic = "GR", Units = "RAD" }
            }
        };

        private static readonly LasSection threeChannelCurveSection = new LasSection
        {
            SectionType = LasSectionType.CurveInformation,
            MnemonicsLines = new LasMnemonicLine[]
            {
                new LasMnemonicLine { Mnemonic = "DEPTH", Units = "FEET" },
                new LasMnemonicLine { Mnemonic = "GR", Units = "RAD" },
                new LasMnemonicLine { Mnemonic = "TEMP", Units = "DEGF" }
            }
        };

        private static readonly LasSection oneChannelAsciiLogData = new LasSection
        {
            SectionType = LasSectionType.AsciiLogData,
            AsciiLogDataLines = new LasAsciiLogDataLine[]
            {
                new LasAsciiLogDataLine { Values = new string[] { "100.0" } },
                new LasAsciiLogDataLine { Values = new string[] { "101.0" } },
                new LasAsciiLogDataLine { Values = new string[] { "102.0" } }
            }
        };

        private static readonly LasSection twoChannelAsciiLogData = new LasSection
        {
            SectionType = LasSectionType.AsciiLogData,
            AsciiLogDataLines = new LasAsciiLogDataLine[]
            {
                new LasAsciiLogDataLine { Values = new string[] { "100.0", "-999.25" } },
                new LasAsciiLogDataLine { Values = new string[] { "101.0", "-999.25" } },
                new LasAsciiLogDataLine { Values = new string[] { "102.0", "-999.25" } }
            }
        };

        private static readonly LasSection threeChannelAsciiLogData = new LasSection
        {
            SectionType = LasSectionType.AsciiLogData,
            AsciiLogDataLines = new LasAsciiLogDataLine[]
            {
                new LasAsciiLogDataLine { Values = new string[] { "100.0", "-999.25", "-999.25" } },
                new LasAsciiLogDataLine { Values = new string[] { "101.0", "-999.25", "-999.25" } },
                new LasAsciiLogDataLine { Values = new string[] { "102.0", "-999.25", "-999.25" } }
            }
        };

        private static readonly LasLog nullLog = null;
        private static readonly LasLog noSections = new LasLog();
        private static readonly LasLog withNoWrapVersionSection = new LasLog { Sections = new LasSection[] { noWrapVersionSection } };
        private static readonly LasLog withWrapVersionSection = new LasLog { Sections = new LasSection[] { wrapVersionSection } };

        private static readonly LasLog withCurveChannelsNoAsciiLogData = new LasLog { Sections = new LasSection[] { noWrapVersionSection, twoChannelCurveSection } };
        private static readonly LasLog withTooFewCurveChannels = new LasLog { Sections = new LasSection[] { noWrapVersionSection, twoChannelCurveSection, oneChannelAsciiLogData } };
        private static readonly LasLog withCorrectCurveChannels = new LasLog { Sections = new LasSection[] { noWrapVersionSection, twoChannelCurveSection, twoChannelAsciiLogData } };
        private static readonly LasLog withTooManyCurveChannels = new LasLog { Sections = new LasSection[] { noWrapVersionSection, twoChannelCurveSection, threeChannelAsciiLogData } };

        private static readonly LasLog withUwiWellId = new LasLog { Sections = new LasSection[] { uwiWellInformationSection } };
        private static readonly LasLog withApiWellId = new LasLog { Sections = new LasSection[] { apiWellInformationSection } };
        private static readonly LasLog withNoWellId = new LasLog { Sections = new LasSection[] { noIdWellInformationSection } };

        [Test]
        public void LasLogHelpers_HasSection_Pass()
        {
            Assert.IsFalse(nullLog.HasSection(LasSectionType.VersionInformation));
            Assert.IsFalse(noSections.HasSection(LasSectionType.VersionInformation));
            Assert.IsFalse(withNoWrapVersionSection.HasSection(LasSectionType.AsciiLogData));
            Assert.IsTrue(withNoWrapVersionSection.HasSection(LasSectionType.VersionInformation));
        }

        [Test]
        public void LasLogHelpers_GetSection_Pass()
        {
            Assert.IsNull(nullLog.GetSection(LasSectionType.VersionInformation));
            Assert.IsNull(noSections.GetSection(LasSectionType.VersionInformation));
            Assert.IsNull(withNoWrapVersionSection.GetSection(LasSectionType.AsciiLogData));
            Assert.AreSame(noWrapVersionSection, withNoWrapVersionSection.GetSection(LasSectionType.VersionInformation));
        }

        [Test]
        public void LasLogHelpers_SectionCount_Pass()
        {
            Assert.AreEqual(0, nullLog.SectionCount(LasSectionType.VersionInformation));
            Assert.AreEqual(0, noSections.SectionCount(LasSectionType.VersionInformation));
            Assert.AreEqual(0, withNoWrapVersionSection.SectionCount(LasSectionType.AsciiLogData));
            Assert.AreEqual(1, withNoWrapVersionSection.SectionCount(LasSectionType.VersionInformation));
        }

        [Test]
        public void LasLogHelpers_UsesLineWrap_Pass()
        {
            Assert.IsFalse(nullLog.UsesLineWrap());
            Assert.IsFalse(noSections.UsesLineWrap());
            Assert.IsFalse(withNoWrapVersionSection.UsesLineWrap());
            Assert.IsTrue(withWrapVersionSection.UsesLineWrap());
        }

        [Test]
        public void LasLogHelpers_AsciiLogDataHasCurveChannels_Pass()
        {
            Assert.IsTrue(nullLog.AsciiLogDataHasCurveChannels());
            Assert.IsTrue(noSections.AsciiLogDataHasCurveChannels());
            Assert.IsTrue(withCurveChannelsNoAsciiLogData.AsciiLogDataHasCurveChannels());
            Assert.IsFalse(withTooFewCurveChannels.AsciiLogDataHasCurveChannels());
            Assert.IsFalse(withTooManyCurveChannels.AsciiLogDataHasCurveChannels());
            Assert.IsTrue(withCorrectCurveChannels.AsciiLogDataHasCurveChannels());
        }

        [Test]
        public void LasLogHelpers_WellIdentifier_Pass()
        {
            Assert.AreEqual(string.Empty, nullLog.WellIdentifier());
            Assert.AreEqual(string.Empty, noSections.WellIdentifier());
            Assert.AreEqual(string.Empty, withNoWellId.WellIdentifier());
            Assert.AreEqual(WELL_ID, withUwiWellId.WellIdentifier());
            Assert.AreEqual(WELL_ID, withApiWellId.WellIdentifier());
        }
    }
}
