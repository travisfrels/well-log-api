using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class SLONGReaderTests
    {
        private SLONGReader _slongReader;

        [SetUp]
        public void PerTestSetup()
        {
            _slongReader = new SLONGReader();
        }

        [Test]
        public void SLONGReader_ReadSLONG_Pass_NullStream()
        {
            MemoryStream s = null;
            int expected = 0;
            Assert.AreEqual(expected, _slongReader.ReadSLONG(s));
        }

        [Test]
        public void SLONGReader_ReadSLONG_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 255, 255, 255 });
            int expected = 0;
            Assert.AreEqual(expected, _slongReader.ReadSLONG(s));
        }

        [Test]
        public void SLONGReader_ReadSLONG_Pass_Positive()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0010 });
            int expected = 2;
            Assert.AreEqual(expected, _slongReader.ReadSLONG(s));
        }

        [Test]
        public void SLONGReader_ReadSLONG_Pass_Negative()
        {
            var s = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1111, 0b_1111_1111, 0b_1111_1110 });
            int expected = -2;
            Assert.AreEqual(expected, _slongReader.ReadSLONG(s));
        }
    }
}
