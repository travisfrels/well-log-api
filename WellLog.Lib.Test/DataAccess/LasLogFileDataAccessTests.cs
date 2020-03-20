using Moq;
using NUnit.Framework;
using System;
using WellLog.Lib.Business;
using WellLog.Lib.DataAccess;

namespace WellLog.Lib.Test.DataAccess
{
    [TestFixture]
    public class LasLogFileDataAccessTests
    {
        private Mock<ILasLogBusiness> _lasLogBusiness;
        private LasLogFileDataAccess _lasLogFileDataAccess;

        [SetUp]
        public void PerTestSetup()
        {
            _lasLogBusiness = new Mock<ILasLogBusiness>();
            _lasLogFileDataAccess = new LasLogFileDataAccess(_lasLogBusiness.Object);
        }

        [Test]
        public void Read_Fail_NullFileName()
        {
            Assert.Throws<ArgumentNullException>(() => { _lasLogFileDataAccess.Read(null); });
        }
    }
}
