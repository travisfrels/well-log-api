using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class UNITSReaderTests
    {
        private UNITSReader _unitsReader;

        [SetUp]
        public void PerTestSetup()
        {
            _unitsReader = new UNITSReader();
        }

        [Test]
        public void UNITSReader_ReadUNITS_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_unitsReader.ReadUNITS(s));
        }

        [Test]
        public void UNITSReader_ReadUNITS_Pass_EmptyStream()
        {
            var s = new MemoryStream(new byte[0]);
            Assert.IsNull(_unitsReader.ReadUNITS(s));
        }

        [Test]
        public void UNITSReader_ReadUNITS_Pass_NoChars()
        {
            var s = new MemoryStream(new byte[] { 0x00 });
            Assert.AreEqual(string.Empty, _unitsReader.ReadUNITS(s));
        }

        [Test]
        public void UNITSReader_ReadUNITS_Pass_TooFewChars()
        {
            var s = new MemoryStream(new byte[] { 0x03, 0x41, 0x42 });
            Assert.IsNull(_unitsReader.ReadUNITS(s));
        }

        [Test]
        public void UNITSReader_ReadUNITS_Pass()
        {
            var s = new MemoryStream(new byte[] { 0x03, 0x41, 0x42, 0x43 });
            string expected = "ABC";
            Assert.AreEqual(expected, _unitsReader.ReadUNITS(s));
        }
    }
}
