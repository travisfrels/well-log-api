using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class SNORMReaderTests
    {
        private SNORMReader _snormReader;

        [SetUp]
        public void PerTestSetup()
        {
            _snormReader = new SNORMReader();
        }

        [Test]
        public void SNORMReader_ReadSNORM_Pass_NullStream()
        {
            MemoryStream s = null;
            short expected = 0;
            Assert.AreEqual(expected, _snormReader.ReadSNORM(s));
        }

        [Test]
        public void SNORMReader_ReadSNORM_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 255 });
            short expected = 0;
            Assert.AreEqual(expected, _snormReader.ReadSNORM(s));
        }

        [Test]
        public void SNORMReader_ReadSNORM_Pass_Positive()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0000_0010 });
            short expected = 2;
            Assert.AreEqual(expected, _snormReader.ReadSNORM(s));
        }

        [Test]
        public void SNORMReader_ReadSNORM_Pass_Negative()
        {
            var s = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1110 });
            short expected = -2;
            Assert.AreEqual(expected, _snormReader.ReadSNORM(s));
        }
    }
}
