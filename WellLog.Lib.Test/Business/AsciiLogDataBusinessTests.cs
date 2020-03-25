using NUnit.Framework;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Enumerations;
using WellLog.Lib.Exceptions;
using WellLog.Lib.Models;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class AsciiLogDataBusinessTests
    {
        private AsciiLogDataBusiness _asciiLogDataBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _asciiLogDataBusiness = new AsciiLogDataBusiness();
        }

        [Test]
        public void AsciiLogDataBusiness_UnWrapAsciiLogData_Pass_NullSection()
        {
            Assert.DoesNotThrow(() => _asciiLogDataBusiness.UnWrapAsciiLogData(null));
        }

        [Test]
        public void AsciiLogDataBusiness_UnWrapAsciiLogData_Pass_NullAsciiLogData()
        {
            Assert.DoesNotThrow(() => _asciiLogDataBusiness.UnWrapAsciiLogData(new LasSection()));
        }

        [Test]
        public void AsciiLogDataBusiness_UnWrapAsciiLogData_Pass_EmptyAsciiLogData()
        {
            Assert.DoesNotThrow(() => _asciiLogDataBusiness.UnWrapAsciiLogData(new LasSection { AsciiLogDataLines = new LasAsciiLogDataLine[0] }));
        }

        [Test]
        public void AsciiLogDataBusiness_UnWrapAsciiLogData_Fail_InvalidLineWrapping()
        {
            var asciiLogData = new LasSection
            {
                SectionType = LasSectionType.AsciiLogData,
                AsciiLogDataLines = new LasAsciiLogDataLine[]
                {
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } }
                }
            };

            Assert.Throws<LasLogFormatException>(() => _asciiLogDataBusiness.UnWrapAsciiLogData(asciiLogData));
        }

        [Test]
        public void AsciiLogDataBusiness_UnWrapAsciiLogData_Pass()
        {
            var asciiLogData = new LasSection
            {
                SectionType = LasSectionType.AsciiLogData,
                AsciiLogDataLines = new LasAsciiLogDataLine[]
                {
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } },
                    new LasAsciiLogDataLine{ Values = new string[] { "-999.25", "-999.25", "-999.25" } }
                }
            };

            _asciiLogDataBusiness.UnWrapAsciiLogData(asciiLogData);

            Assert.AreEqual(2, asciiLogData.AsciiLogDataLines.Count());
        }
    }
}
