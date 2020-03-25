using NUnit.Framework;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Models;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class WellInformationDataBusinessTests
    {
        private WellInformationBusiness _wellInformationBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _wellInformationBusiness = new WellInformationBusiness();
        }

        [Test]
        public void WellInformationBusiness_FixWellInformation_Pass_NullSection()
        {
            Assert.DoesNotThrow(() => _wellInformationBusiness.FixWellInformation(null));
        }

        [Test]
        public void WellInformationBusiness_FixWellInformation_Pass_NullMnemonicLines()
        {
            Assert.DoesNotThrow(() => _wellInformationBusiness.FixWellInformation(new LasSection()));
        }

        [Test]
        public void WellInformationBusiness_UnWrapAsciiLogData_Pass_EmptyMnemonicLines()
        {
            Assert.DoesNotThrow(() => _wellInformationBusiness.FixWellInformation(new LasSection { MnemonicsLines = new LasMnemonicLine[0] }));
        }

        [Test]
        public void WellInformationBusiness_FixWellInformation_Pass()
        {
            var company = "ANY OIL COMPANY INC.";
            var well = "SOME WELL NAME";
            var field = "THAT FIELD OVER THERE";
            var location = "THIS LOCATION";
            var province = "ONTARIO";
            var county = "CREEK";
            var state = "OKLAHOMA";
            var country = "USA";
            var serviceCompany = "ONTARIO";
            var dateLogged = "CREEK";
            var uwi = "OKLAHOMA";
            var api = "USA";

            var wellInformationSection = new LasSection
            {
                SectionType = LasSectionType.WellInformation,
                MnemonicsLines = new LasMnemonicLine[]
                {
                    new LasMnemonicLine{ Mnemonic = "COMP", Units = "", Data = "COMPANY", Description = company },
                    new LasMnemonicLine{ Mnemonic = "WELL", Units = "", Data = "WELL NAME", Description = well },
                    new LasMnemonicLine{ Mnemonic = "FLD", Units = "", Data = "FIELD NAME", Description = field },
                    new LasMnemonicLine{ Mnemonic = "LOC", Units = "", Data = "LOCATION", Description = location },
                    new LasMnemonicLine{ Mnemonic = "PROV", Units = "", Data = "PROVINCE", Description = province },
                    new LasMnemonicLine{ Mnemonic = "CNTY", Units = "", Data = "COUNTY", Description = county },
                    new LasMnemonicLine{ Mnemonic = "STAT", Units = "", Data = "STATE", Description = state },
                    new LasMnemonicLine{ Mnemonic = "CTRY", Units = "", Data = "COUNTRY", Description = country },
                    new LasMnemonicLine{ Mnemonic = "SRVC", Units = "", Data = "SERVICE COMPANY", Description = serviceCompany },
                    new LasMnemonicLine{ Mnemonic = "DATE", Units = "", Data = "DATE LOGGED", Description = dateLogged },
                    new LasMnemonicLine{ Mnemonic = "UWI", Units = "", Data = "UNIQUE WELL ID", Description = uwi },
                    new LasMnemonicLine{ Mnemonic = "API", Units = "", Data = "API NUMBER", Description = api }
                }
            };

            _wellInformationBusiness.FixWellInformation(wellInformationSection);

            Assert.AreEqual(company, wellInformationSection.GetCompanyMnemonic().Data);
            Assert.AreEqual(well, wellInformationSection.GetWellMnemonic().Data);
            Assert.AreEqual(field, wellInformationSection.GetFieldMnemonic().Data);
            Assert.AreEqual(location, wellInformationSection.GetLocationMnemonic().Data);
            Assert.AreEqual(province, wellInformationSection.GetProvinceMnemonic().Data);
            Assert.AreEqual(county, wellInformationSection.GetCountyMnemonic().Data);
            Assert.AreEqual(state, wellInformationSection.GetStateMnemonic().Data);
            Assert.AreEqual(country, wellInformationSection.GetCountryMnemonic().Data);
            Assert.AreEqual(serviceCompany, wellInformationSection.GetServiceCompanyMnemonic().Data);
            Assert.AreEqual(dateLogged, wellInformationSection.GetDateMnemonic().Data);
            Assert.AreEqual(uwi, wellInformationSection.GetUwiMnemonic().Data);
            Assert.AreEqual(api, wellInformationSection.GetApiMnemonic().Data);
        }
    }
}
