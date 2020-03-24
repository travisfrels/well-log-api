using Moq;
using NUnit.Framework;
using System;
using System.IO;
using WellLog.Lib.Business;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LasLogBusinessTests
    {
        private Mock<ILasSectionBusiness> _lasSectionBusiness;
        private Mock<IAsciiLogDataBusiness> _asciiLogDataBusiness;
        private Mock<IWellInformationBusiness> _wellInformationBusiness;
        private LasLogBusiness _lasLogBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _lasSectionBusiness = new Mock<ILasSectionBusiness>();
            _asciiLogDataBusiness = new Mock<IAsciiLogDataBusiness>();
            _wellInformationBusiness = new Mock<IWellInformationBusiness>();

            _lasLogBusiness = new LasLogBusiness(_lasSectionBusiness.Object, _asciiLogDataBusiness.Object, _wellInformationBusiness.Object);
        }

        [Test]
        public void LasLogBusiness_ReadStream_Fail_NullStream()
        {
            Assert.Throws<ArgumentNullException>(() => { _lasLogBusiness.ReadStream(null); });
        }

        [Test]
        public void LasLogBusiness_ReadStream_Pass()
        {
            var lasStream = new MemoryStream(new byte[0]);
            Assert.DoesNotThrow(() => { _lasLogBusiness.ReadStream(lasStream); });
        }
    }
}
