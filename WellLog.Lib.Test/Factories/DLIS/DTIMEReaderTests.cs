using NUnit.Framework;
using System;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class DTIMEReaderTests
    {
        private DTIMEReader _dtimeReader;

        [SetUp]
        public void PerTestSetup()
        {
            _dtimeReader = new DTIMEReader();
        }

        [Test]
        public void DTIMEReader_ReadDTIME_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.AreEqual(DateTime.MinValue, _dtimeReader.ReadDTIME(s));
        }

        [Test]
        public void DTIMEReader_ReadDTIME_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.AreEqual(DateTime.MinValue, _dtimeReader.ReadDTIME(s));
        }

        [Test]
        public void DTIMEReader_ReadDTIME_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(DateTime.MinValue, _dtimeReader.ReadDTIME(s));
        }

        [Test]
        public void DTIMEReader_ReadDTIME_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0101_0111, 0b_0001_0100, 0b_0001_0011, 0b_0001_0101, 0b_0001_0100, 0b_0000_1111, 0b_0000_0010, 0b_0110_1100 });
            DateTime expected = new DateTime(1987, 4, 19, 21, 20, 15, 620, DateTimeKind.Local);
            Assert.AreEqual(expected, _dtimeReader.ReadDTIME(s));
        }
    }
}
