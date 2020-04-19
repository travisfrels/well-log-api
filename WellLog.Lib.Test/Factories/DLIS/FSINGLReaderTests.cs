using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class FSINGLReaderTests
    {
        private FSINGLReader _fsinglReader;

        [SetUp]
        public void PerTestSetup()
        {
            _fsinglReader = new FSINGLReader();
        }

        [Test]
        public void FSINGLReader_ReadFSINGL_Pass_NullStream()
        {
            MemoryStream s = null;
            var expected = 0f;
            Assert.AreEqual(expected, _fsinglReader.ReadFSINGL(s));
        }

        [Test]
        public void FSINGLReader_ReadFSINGL_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            var expected = 0f;
            Assert.AreEqual(expected, _fsinglReader.ReadFSINGL(s));
        }

        [Test]
        public void FSINGLReader_ReadFSINGL_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0000, 0b_0100_1001, 0b_0000_1111 });
            var expected = 0f;
            Assert.AreEqual(expected, _fsinglReader.ReadFSINGL(s));
        }

        [Test]
        public void FSINGLReader_ReadFSINGL_Pass_PositivePi()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var expected = 3.14159274f;
            Assert.AreEqual(expected, _fsinglReader.ReadFSINGL(s));
        }

        [Test]
        public void FSINGLReader_ReadFSINGL_Pass_NegativePi()
        {
            var s = new MemoryStream(new byte[] { 0b_1100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var expected = -3.14159274f;
            Assert.AreEqual(expected, _fsinglReader.ReadFSINGL(s));
        }
    }
}
