using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class FSING2ReaderTests
    {
        private FSINGLReader _fsinglReader;
        private FSING2Reader _fsing2Reader;

        [SetUp]
        public void PerTestSetup()
        {
            _fsinglReader = new FSINGLReader();
            _fsing2Reader = new FSING2Reader(_fsinglReader);
        }

        [Test]
        public void FSING2Reader_ReadFSING2_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_fsing2Reader.ReadFSING2(s));
        }

        [Test]
        public void FSING2Reader_ReadFSING2_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_fsing2Reader.ReadFSING2(s));
        }

        [Test]
        public void FSING2Reader_ReadFSING2_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_1100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_0100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var result = _fsing2Reader.ReadFSING2(s);
            Assert.AreEqual(153f, result.V);
            Assert.AreEqual(-153f, result.A);
            Assert.AreEqual(3.14159274f, result.B);
        }
    }
}
