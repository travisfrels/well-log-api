using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasWellInformationHelpersTests
    {
        private static readonly LasMnemonicLine uwiLine = new LasMnemonicLine { Mnemonic = "UWI" };
        private static readonly LasMnemonicLine apiLine = new LasMnemonicLine { Mnemonic = "API" };

        private static readonly LasSection nullSection = null;
        private static readonly LasSection emptySection = new LasSection();
        private static readonly LasSection wellInformationSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[]
            {
                new LasMnemonicLine { Mnemonic = "STRT" },
                new LasMnemonicLine { Mnemonic = "STOP" },
                new LasMnemonicLine { Mnemonic = "STEP" },
                new LasMnemonicLine { Mnemonic = "NULL" },
                new LasMnemonicLine { Mnemonic = "COMP" },
                new LasMnemonicLine { Mnemonic = "WELL" },
                new LasMnemonicLine { Mnemonic = "FLD" },
                new LasMnemonicLine { Mnemonic = "LOC" },
                new LasMnemonicLine { Mnemonic = "PROV" },
                new LasMnemonicLine { Mnemonic = "CNTY" },
                new LasMnemonicLine { Mnemonic = "STAT" },
                new LasMnemonicLine { Mnemonic = "CTRY" },
                new LasMnemonicLine { Mnemonic = "SRVC" },
                new LasMnemonicLine { Mnemonic = "DATE" },
                uwiLine,
                apiLine
            }
        };

        [Test]
        public void LasWellInformationHelpers_HasStartMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasStartMnemonic());
            Assert.IsFalse(emptySection.HasStartMnemonic());
            Assert.IsTrue(wellInformationSection.HasStartMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasStopMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasStopMnemonic());
            Assert.IsFalse(emptySection.HasStopMnemonic());
            Assert.IsTrue(wellInformationSection.HasStopMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasStepMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasStepMnemonic());
            Assert.IsFalse(emptySection.HasStepMnemonic());
            Assert.IsTrue(wellInformationSection.HasStepMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasNullMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasNullMnemonic());
            Assert.IsFalse(emptySection.HasNullMnemonic());
            Assert.IsTrue(wellInformationSection.HasNullMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasCompanyMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasCompanyMnemonic());
            Assert.IsFalse(emptySection.HasCompanyMnemonic());
            Assert.IsTrue(wellInformationSection.HasCompanyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasWellMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasWellMnemonic());
            Assert.IsFalse(emptySection.HasWellMnemonic());
            Assert.IsTrue(wellInformationSection.HasWellMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasFieldMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasFieldMnemonic());
            Assert.IsFalse(emptySection.HasFieldMnemonic());
            Assert.IsTrue(wellInformationSection.HasFieldMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasLocationMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasLocationMnemonic());
            Assert.IsFalse(emptySection.HasLocationMnemonic());
            Assert.IsTrue(wellInformationSection.HasLocationMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasProvinceMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasProvinceMnemonic());
            Assert.IsFalse(emptySection.HasProvinceMnemonic());
            Assert.IsTrue(wellInformationSection.HasProvinceMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasCountyMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasCountyMnemonic());
            Assert.IsFalse(emptySection.HasCountyMnemonic());
            Assert.IsTrue(wellInformationSection.HasCountyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasStateMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasStateMnemonic());
            Assert.IsFalse(emptySection.HasStateMnemonic());
            Assert.IsTrue(wellInformationSection.HasStateMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasCountryMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasCountryMnemonic());
            Assert.IsFalse(emptySection.HasCountryMnemonic());
            Assert.IsTrue(wellInformationSection.HasCountryMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasAreaMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasAreaMnemonic());
            Assert.IsFalse(emptySection.HasAreaMnemonic());
            Assert.IsTrue(wellInformationSection.HasAreaMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasServiceCompanyMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasServiceCompanyMnemonic());
            Assert.IsFalse(emptySection.HasServiceCompanyMnemonic());
            Assert.IsTrue(wellInformationSection.HasServiceCompanyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasDateMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasDateMnemonic());
            Assert.IsFalse(emptySection.HasDateMnemonic());
            Assert.IsTrue(wellInformationSection.HasDateMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasUwiMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasUwiMnemonic());
            Assert.IsFalse(emptySection.HasUwiMnemonic());
            Assert.IsTrue(wellInformationSection.HasUwiMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasApiMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasApiMnemonic());
            Assert.IsFalse(emptySection.HasApiMnemonic());
            Assert.IsTrue(wellInformationSection.HasApiMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_HasIdentifierMnemonic_Pass()
        {
            Assert.IsFalse(nullSection.HasIdentifierMnemonic());
            Assert.IsFalse(emptySection.HasIdentifierMnemonic());
            Assert.IsTrue(wellInformationSection.HasIdentifierMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetUwiMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetUwiMnemonic());
            Assert.IsNull(emptySection.GetUwiMnemonic());
            Assert.AreSame(uwiLine, wellInformationSection.GetUwiMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetApiMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetApiMnemonic());
            Assert.IsNull(emptySection.GetApiMnemonic());
            Assert.AreSame(apiLine, wellInformationSection.GetApiMnemonic());
        }
    }
}
