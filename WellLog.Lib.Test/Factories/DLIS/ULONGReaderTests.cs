using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class ULONGReaderTests
    {
        private ULONGReader _ulongReader;

        [SetUp]
        public void PerTestSetup()
        {
            _ulongReader = new ULONGReader();
        }

        [Test]
        public void ULONGReader_ReadULONG_Pass_NullStream()
        {
            MemoryStream s = null;
            uint expected = 0;
            Assert.AreEqual(expected, _ulongReader.ReadULONG(s));
        }

        [Test]
        public void ULONGReader_ReadULONG_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 255, 255, 255 });
            uint expected = 0;
            Assert.AreEqual(expected, _ulongReader.ReadULONG(s));
        }

        [Test]
        public void ULONGReader_ReadULONG_Pass_Positive()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 2147483648;
            Assert.AreEqual(expected, _ulongReader.ReadULONG(s));
        }
    }
}
