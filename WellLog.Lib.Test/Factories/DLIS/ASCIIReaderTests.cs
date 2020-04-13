using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class ASCIIReaderTests
    {
        private UVARIReader _uvariReader;
        private ASCIIReader _asciiReader;

        [SetUp]
        public void PerTestSetup()
        {
            _uvariReader = new UVARIReader();
            _asciiReader = new ASCIIReader(_uvariReader);
        }

        [Test]
        public void ASCIIReader_ReadASCII_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_asciiReader.ReadASCII(s));
        }

        [Test]
        public void ASCIIReader_ReadASCII_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_asciiReader.ReadASCII(s));
        }

        [Test]
        public void ASCIIReader_ReadASCII_Pass_NoChars()
        {
            var s = new MemoryStream(new byte[] { 0x00 });
            Assert.AreEqual(string.Empty, _asciiReader.ReadASCII(s));
        }

        [Test]
        public void ASCIIReader_ReadASCII_Pass_TooFewChars()
        {
            var s = new MemoryStream(new byte[] { 0x03, 0x41, 0x42 });
            Assert.IsNull(_asciiReader.ReadASCII(s));
        }

        [Test]
        public void ASCIIReader_ReadASCII_Pass()
        {
            var s = new MemoryStream(new byte[] { 0x03, 0x41, 0x42, 0x43 });
            string expected = "ABC";
            Assert.AreEqual(expected, _asciiReader.ReadASCII(s));
        }
    }
}
