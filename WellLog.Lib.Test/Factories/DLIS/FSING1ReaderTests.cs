using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class FSING1ReaderTests
    {
        private FSINGLReader _fsinglReader;
        private FSING1Reader _fsing1Reader;

        [SetUp]
        public void PerTestSetup()
        {
            _fsinglReader = new FSINGLReader();
            _fsing1Reader = new FSING1Reader(_fsinglReader);
        }

        [Test]
        public void FSING1Reader_ReadFSING1_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_fsing1Reader.ReadFSING1(s));
        }

        [Test]
        public void FSING1Reader_ReadFSING1_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_fsing1Reader.ReadFSING1(s));
        }

        [Test]
        public void FSING1Reader_ReadFSING1_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_1100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000 });
            var result = _fsing1Reader.ReadFSING1(s);
            Assert.AreEqual(153f, result.V);
            Assert.AreEqual(-153f, result.A);
        }
    }
}
