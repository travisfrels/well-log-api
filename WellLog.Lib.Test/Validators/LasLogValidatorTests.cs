using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Models;
using WellLog.Lib.Validators;

namespace WellLog.Lib.Test.Validators
{
    [TestFixture]
    public class LasLogValidatorTests
    {
        private const string WELL_ID = "49009282000000";

        private LasMnemonicLine _versionMnemonic;
        private LasMnemonicLine _wrapMnemonic;

        private LasMnemonicLine _startMnemonic;
        private LasMnemonicLine _stopMnemonic;
        private LasMnemonicLine _stepMnemonic;
        private LasMnemonicLine _nullMnemonic;
        private LasMnemonicLine _companyMnemonic;
        private LasMnemonicLine _wellNameMnemonic;
        private LasMnemonicLine _fieldMnemonic;
        private LasMnemonicLine _locationMnemonic;
        private LasMnemonicLine _provinceMnemonic;
        private LasMnemonicLine _countyMnemonic;
        private LasMnemonicLine _stateMnemonic;
        private LasMnemonicLine _countryMnemonic;
        private LasMnemonicLine _serviceCompanyMnemonic;
        private LasMnemonicLine _dateLoggedMnemonic;
        private LasMnemonicLine _uniqueWellIdMnemonic;
        private LasMnemonicLine _apiNumberMnemonic;

        private LasMnemonicLine _depthChannelMnemonic;
        private LasMnemonicLine _gammaChannelMnemonic;

        private LasSection _versionInformationSection;
        private LasSection _wellInformationSection;
        private LasSection _curveInformationSection;
        private LasSection _parameterInformationSection;
        private LasSection _otherInformationSection;
        private LasSection _asciiLogDataSection;

        private LasLog _lasLog;

        private LasLogValidator _lasLogValidator;

        [SetUp]
        public void PerTestSetup()
        {
            _versionMnemonic = new LasMnemonicLine { Mnemonic = "VERS", Data = "2.0", Description = "LAS VERSION" };
            _wrapMnemonic = new LasMnemonicLine { Mnemonic = "WRAP", Data = "NO", Description = "LINE WRAP" };

            _versionInformationSection = new LasSection
            {
                SectionType = LasSectionType.VersionInformation,
                MnemonicsLines = new List<LasMnemonicLine> { _versionMnemonic, _wrapMnemonic }
            };


            _startMnemonic = new LasMnemonicLine { Mnemonic = "STRT", Units = "FEET", Data = "100.0", Description = "START DEPTH" };
            _stopMnemonic = new LasMnemonicLine { Mnemonic = "STOP", Units = "FEET", Data = "101.0", Description = "STOP DEPTH" };
            _stepMnemonic = new LasMnemonicLine { Mnemonic = "STEP", Units = "FEET", Data = "0.5", Description = "STEP INCREMENT" };
            _nullMnemonic = new LasMnemonicLine { Mnemonic = "NULL", Data = "-999.25", Description = "NULL VALUE" };
            _companyMnemonic = new LasMnemonicLine { Mnemonic = "COMP", Data = "DAVIS PETROLEUM CORP", Description = "COMPANY" };
            _wellNameMnemonic = new LasMnemonicLine { Mnemonic = "WELL", Data = "TYLER DEEP UNIT #1", Description = "WELL NAME" };
            _fieldMnemonic = new LasMnemonicLine { Mnemonic = "FLD", Data = "WILDCAT", Description = "FIELD" };
            _locationMnemonic = new LasMnemonicLine { Mnemonic = "LOC", Data = "630' FSL & 1790' FWL", Description = "LOCATION" };
            _provinceMnemonic = new LasMnemonicLine { Mnemonic = "PROV", Data = "", Description = "PROVINCE" };
            _countyMnemonic = new LasMnemonicLine { Mnemonic = "CNTY", Data = "CONVERSE", Description = "COUNTY" };
            _stateMnemonic = new LasMnemonicLine { Mnemonic = "STAT", Data = "WYOMING", Description = "STATE" };
            _countryMnemonic = new LasMnemonicLine { Mnemonic = "CTRY", Data = "USA", Description = "COUNTRY" };
            _serviceCompanyMnemonic = new LasMnemonicLine { Mnemonic = "SRVC", Data = "BAKER ATLAS", Description = "SERVICE COMPANY" };
            _dateLoggedMnemonic = new LasMnemonicLine { Mnemonic = "DATE", Data = "2008-10-18", Description = "DATE LOGGED" };
            _uniqueWellIdMnemonic = new LasMnemonicLine { Mnemonic = "UWI", Data = WELL_ID, Description = "UNIQUE WELL ID" };
            _apiNumberMnemonic = new LasMnemonicLine { Mnemonic = "API", Data = WELL_ID, Description = "API NUMBER" };

            _wellInformationSection = new LasSection
            {
                SectionType = LasSectionType.WellInformation,
                MnemonicsLines = new List<LasMnemonicLine>
                {
                    _startMnemonic,
                    _stopMnemonic,
                    _stepMnemonic,
                    _nullMnemonic,
                    _companyMnemonic,
                    _wellNameMnemonic,
                    _fieldMnemonic,
                    _locationMnemonic,
                    _provinceMnemonic,
                    _countyMnemonic,
                    _stateMnemonic,
                    _countryMnemonic,
                    _serviceCompanyMnemonic,
                    _dateLoggedMnemonic,
                    _uniqueWellIdMnemonic,
                    _apiNumberMnemonic
                }
            };

            _depthChannelMnemonic = new LasMnemonicLine { Mnemonic = "DEPT", Units = "FEET", Description = "DEPTH" };
            _gammaChannelMnemonic = new LasMnemonicLine { Mnemonic = "GR", Units = "RAD", Description = "GAMMA RAY" };

            _curveInformationSection = new LasSection
            {
                SectionType = LasSectionType.CurveInformation,
                MnemonicsLines = new List<LasMnemonicLine>
                {
                    _depthChannelMnemonic,
                    new LasMnemonicLine{ Mnemonic = "PRSR", Units = "PSI",  Description = "PRESSURE" },
                    new LasMnemonicLine{ Mnemonic = "TEMP", Units = "DEGC", Description = "TEMPURATURE" }
                }
            };

            _parameterInformationSection = new LasSection
            {
                SectionType = LasSectionType.ParameterInformation
            };

            _otherInformationSection = new LasSection
            {
                SectionType = LasSectionType.OtherInformation
            };

            _asciiLogDataSection = new LasSection
            {
                SectionType = LasSectionType.AsciiLogData,
                AsciiLogDataLines = new List<LasAsciiLogDataLine>
                {
                    new LasAsciiLogDataLine{ Values = new string[] { "100.0", "0.500", "90.0" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "100.5", "0.510", "92.0" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "101.0", "0.520", "94.0" } }
                }
            };

            _lasLog = new LasLog
            {
                Sections = new List<LasSection>
                {
                    _versionInformationSection,
                    _wellInformationSection,
                    _curveInformationSection,
                    _parameterInformationSection,
                    _otherInformationSection,
                    _asciiLogDataSection
                }
            };

            _lasLogValidator = new LasLogValidator();
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Pass()
        {
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(0, verr.Count());
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoVersionInformationSection()
        {
            ((List<LasSection>)_lasLog.Sections).Remove(_versionInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyVersionInformationSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(0, _versionInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoVersionMnemonic()
        {
            ((List<LasMnemonicLine>)_versionInformationSection.MnemonicsLines).Remove(_versionMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).VersionInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoWrapMnemonic()
        {
            ((List<LasMnemonicLine>)_versionInformationSection.MnemonicsLines).Remove(_wrapMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).VersionInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoWellInformationSection()
        {
            ((List<LasSection>)_lasLog.Sections).Remove(_wellInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual("LasLog().Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyWellInformationSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(1, _wellInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoStartMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_startMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoStopMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_stopMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoStepMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_stepMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoNullMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_nullMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoCompanyMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_companyMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoWellNameMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_wellNameMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoFieldMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_fieldMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoLocationMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_locationMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoAreaMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_provinceMnemonic);
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_countyMnemonic);
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_stateMnemonic);
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_countryMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoServiceCompanyMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_serviceCompanyMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoDateLoggedMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_dateLoggedMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoWellIdMnemonic()
        {
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_uniqueWellIdMnemonic);
            ((List<LasMnemonicLine>)_wellInformationSection.MnemonicsLines).Remove(_apiNumberMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual("LasLog().WellInformation", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoCurveInformationSection()
        {
            ((List<LasSection>)_lasLog.Sections).Remove(_curveInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyCurveInformationSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(2, _curveInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoIndexChannelMnemonic()
        {
            ((List<LasMnemonicLine>)_curveInformationSection.MnemonicsLines).Remove(_depthChannelMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(2, verr.Count());
            Assert.IsTrue(verr.Any(x => string.Compare(x.Field, $"LasLog({WELL_ID}).CurveInformation") == 0));
            Assert.IsTrue(verr.Any(x => string.Compare(x.Field, $"LasLog({WELL_ID}).AsciiLogData") == 0));
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyParameterInformationSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(3, _parameterInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyOtherInformationSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(4, _otherInformationSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_NoAsciiLogDataSection()
        {
            ((List<LasSection>)_lasLog.Sections).Remove(_asciiLogDataSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_TooManyAsciiLogDataSections()
        {
            ((List<LasSection>)_lasLog.Sections).Insert(5, _asciiLogDataSection);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).Sections", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_EmptyAsciiLogDataLine()
        {
            _lasLog.AsciiLogData.AsciiLogDataLines.First().Values = new string[0];
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).AsciiLogData", verr.First().Field);
        }

        [Test]
        public void LasLogValidator_ValidateLasLog_Fail_MissingChannel()
        {
            ((List<LasMnemonicLine>)_lasLog.CurveInformation.MnemonicsLines).Add(_gammaChannelMnemonic);
            var verr = _lasLogValidator.ValidateLasLog(_lasLog);
            Assert.AreEqual(1, verr.Count());
            Assert.AreEqual($"LasLog({WELL_ID}).AsciiLogData", verr.First().Field);
        }
    }
}
