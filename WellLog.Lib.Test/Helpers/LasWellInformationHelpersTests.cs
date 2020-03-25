using NUnit.Framework;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasWellInformationHelpersTests
    {
        private static readonly LasMnemonicLine startMnemonicLine = new LasMnemonicLine { Mnemonic = "STRT", Units = "FEET", Data = "100.0", Description = "START DEPTH" };
        private static readonly LasMnemonicLine stopMnemonicLine = new LasMnemonicLine { Mnemonic = "STOP", Units = "FEET", Data = "101.0", Description = "STOP DEPTH" };
        private static readonly LasMnemonicLine stepMnemonicLine = new LasMnemonicLine { Mnemonic = "STEP", Units = "FEET", Data = "0.5", Description = "DEPTH STEP" };
        private static readonly LasMnemonicLine nullMnemonicLine = new LasMnemonicLine { Mnemonic = "NULL", Data = "-999.25", Description = "NULL VALUE" };
        private static readonly LasMnemonicLine companyMnemonicLine = new LasMnemonicLine { Mnemonic = "COMP", Data = "ANY COMPANY", Description = "COMPANY" };
        private static readonly LasMnemonicLine wellMnemonicLine = new LasMnemonicLine { Mnemonic = "WELL", Data = "ANY WELL", Description = "WELL NAME" };
        private static readonly LasMnemonicLine fieldMnemonicLine = new LasMnemonicLine { Mnemonic = "FLD", Data = "ANY FIELD", Description = "FIELD NAME" };
        private static readonly LasMnemonicLine locationMnemonicLine = new LasMnemonicLine { Mnemonic = "LOC", Data = "ANY LOCATION", Description = "LOCATION" };
        private static readonly LasMnemonicLine provinceMnemonicLine = new LasMnemonicLine { Mnemonic = "PROV", Data = "ANY PROVINCE", Description = "PROVINCE" };
        private static readonly LasMnemonicLine countyMnemonicLine = new LasMnemonicLine { Mnemonic = "CNTY", Data = "ANY COUNTY", Description = "COUNTY" };
        private static readonly LasMnemonicLine stateMnemonicLine = new LasMnemonicLine { Mnemonic = "STAT", Data = "ANY STATE", Description = "STATE" };
        private static readonly LasMnemonicLine countryMnemonicLine = new LasMnemonicLine { Mnemonic = "CTRY", Data = "ANY COUNTRY", Description = "COUNTRY" };
        private static readonly LasMnemonicLine serviceCompanyMnemonicLine = new LasMnemonicLine { Mnemonic = "SRVC", Data = "ANY SERVICE COMPANY", Description = "SERVICE COMPANY" };
        private static readonly LasMnemonicLine dateMnemonicLine = new LasMnemonicLine { Mnemonic = "DATE", Data = "2020-03-25", Description = "DATE LOGGED" };
        private static readonly LasMnemonicLine uwiMnemonicLine = new LasMnemonicLine { Mnemonic = "UWI", Data = "123456789012", Description = "UNIQUE WELL ID" };
        private static readonly LasMnemonicLine apiMnemonicLine = new LasMnemonicLine { Mnemonic = "API", Data = "123456789012", Description = "API NUMBER" };

        private static readonly LasSection nullSection = null;
        private static readonly LasSection emptySection = new LasSection();
        private static readonly LasSection wellInformationSection = new LasSection
        {
            MnemonicsLines = new LasMnemonicLine[]
            {
                startMnemonicLine,
                stopMnemonicLine,
                stepMnemonicLine,
                nullMnemonicLine,
                companyMnemonicLine,
                wellMnemonicLine,
                fieldMnemonicLine,
                locationMnemonicLine,
                provinceMnemonicLine,
                countyMnemonicLine,
                stateMnemonicLine,
                countryMnemonicLine,
                serviceCompanyMnemonicLine,
                dateMnemonicLine,
                uwiMnemonicLine,
                apiMnemonicLine
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
        public void LasWellInformationHelpers_GetCompanyMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetCompanyMnemonic());
            Assert.IsNull(emptySection.GetCompanyMnemonic());
            Assert.AreSame(companyMnemonicLine, wellInformationSection.GetCompanyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetWellMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetWellMnemonic());
            Assert.IsNull(emptySection.GetWellMnemonic());
            Assert.AreSame(wellMnemonicLine, wellInformationSection.GetWellMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetFieldMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetFieldMnemonic());
            Assert.IsNull(emptySection.GetFieldMnemonic());
            Assert.AreSame(fieldMnemonicLine, wellInformationSection.GetFieldMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetLocationMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetLocationMnemonic());
            Assert.IsNull(emptySection.GetLocationMnemonic());
            Assert.AreSame(locationMnemonicLine, wellInformationSection.GetLocationMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetProvinceMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetProvinceMnemonic());
            Assert.IsNull(emptySection.GetProvinceMnemonic());
            Assert.AreSame(provinceMnemonicLine, wellInformationSection.GetProvinceMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetCountyMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetCountyMnemonic());
            Assert.IsNull(emptySection.GetCountyMnemonic());
            Assert.AreSame(countyMnemonicLine, wellInformationSection.GetCountyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetStateMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetStateMnemonic());
            Assert.IsNull(emptySection.GetStateMnemonic());
            Assert.AreSame(stateMnemonicLine, wellInformationSection.GetStateMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetCountryMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetCountryMnemonic());
            Assert.IsNull(emptySection.GetCountryMnemonic());
            Assert.AreSame(countryMnemonicLine, wellInformationSection.GetCountryMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetServiceCompanyMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetServiceCompanyMnemonic());
            Assert.IsNull(emptySection.GetServiceCompanyMnemonic());
            Assert.AreSame(serviceCompanyMnemonicLine, wellInformationSection.GetServiceCompanyMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetDateMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetDateMnemonic());
            Assert.IsNull(emptySection.GetDateMnemonic());
            Assert.AreSame(dateMnemonicLine, wellInformationSection.GetDateMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetUwiMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetUwiMnemonic());
            Assert.IsNull(emptySection.GetUwiMnemonic());
            Assert.AreSame(uwiMnemonicLine, wellInformationSection.GetUwiMnemonic());
        }

        [Test]
        public void LasWellInformationHelpers_GetApiMnemonic_Pass()
        {
            Assert.IsNull(nullSection.GetApiMnemonic());
            Assert.IsNull(emptySection.GetApiMnemonic());
            Assert.AreSame(apiMnemonicLine, wellInformationSection.GetApiMnemonic());
        }
    }
}
