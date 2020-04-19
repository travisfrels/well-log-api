using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class IDENTReaderTests
    {
        private IDENTReader _identReader;

        [SetUp]
        public void PerTestSetup()
        {
            _identReader = new IDENTReader();
        }

        [Test]
        public void IDENTReader_ReadIDENT_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_identReader.ReadIDENT(s));
        }

        [Test]
        public void IDENTReader_ReadIDENT_Pass_EmptyStream()
        {
            var s = new MemoryStream(new byte[0]);
            Assert.IsNull(_identReader.ReadIDENT(s));
        }

        [Test]
        public void IDENTReader_ReadIDENT_Pass_NoChars()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(string.Empty, _identReader.ReadIDENT(s));
        }

        [Test]
        public void IDENTReader_ReadIDENT_Pass_TooFewChars()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010 });
            Assert.IsNull(_identReader.ReadIDENT(s));
        }

        [Test]
        public void IDENTReader_ReadIDENT_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010, 0b_0100_0011 });
            string expected = "ABC";
            Assert.AreEqual(expected, _identReader.ReadIDENT(s));
        }
    }
}
