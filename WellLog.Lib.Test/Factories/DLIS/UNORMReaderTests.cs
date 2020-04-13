using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class UNORMReaderTests
    {
        private UNORMReader _unormReader;

        [SetUp]
        public void PerTestSetup()
        {
            _unormReader = new UNORMReader();
        }

        [Test]
        public void UNORMReader_ReadUNORM_Pass_NullStream()
        {
            MemoryStream s = null;
            ushort expected = 0;
            Assert.AreEqual(expected, _unormReader.ReadUNORM(s));
        }

        [Test]
        public void UNORMReader_ReadUNORM_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 255 });
            ushort expected = 0;
            Assert.AreEqual(expected, _unormReader.ReadUNORM(s));
        }

        [Test]
        public void UNORMReader_ReadUNORM_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0000_0000 });
            ushort expected = 32768;
            Assert.AreEqual(expected, _unormReader.ReadUNORM(s));
        }
    }
}
