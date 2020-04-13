using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class USHORTReaderTests
    {
        private USHORTReader _ushortReader;

        [SetUp]
        public void PerTestSetup()
        {
            _ushortReader = new USHORTReader();
        }

        [Test]
        public void USHORTReader_ReadUSHORT_Pass_NullStream()
        {
            MemoryStream s = null;
            byte expected = 0;
            Assert.AreEqual(expected, _ushortReader.ReadUSHORT(s));
        }

        [Test]
        public void USHORTReader_ReadUSHORT_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[0]);
            byte expected = 0;
            Assert.AreEqual(expected, _ushortReader.ReadUSHORT(s));
        }

        [Test]
        public void USHORTReader_ReadUSHORT_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0000 });
            byte expected = 128;
            Assert.AreEqual(expected, _ushortReader.ReadUSHORT(s));
        }
    }
}
