using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class STATUSReaderTests
    {
        private STATUSReader _statusReader;

        [SetUp]
        public void PerTestSetup()
        {
            _statusReader = new STATUSReader();
        }

        [Test]
        public void STATUSReader_ReadSTATUS_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsFalse(_statusReader.ReadSTATUS(s));
        }

        [Test]
        public void STATUSReader_ReadSTATUS_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsFalse(_statusReader.ReadSTATUS(s));
        }

        [Test]
        public void STATUSReader_ReadSTATUS_Pass_True()
        {
            var s = new MemoryStream(new byte[] { 0x01 });
            Assert.IsTrue(_statusReader.ReadSTATUS(s));
        }

        [Test]
        public void STATUSReader_ReadSTATUS_Pass_False()
        {
            var s = new MemoryStream(new byte[] { 0x00 });
            Assert.IsFalse(_statusReader.ReadSTATUS(s));
        }
    }
}
