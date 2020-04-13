using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class ISINGLReaderTests
    {
        private ISINGLReader _isinglReader;

        [SetUp]
        public void PerTestSetup()
        {
            _isinglReader = new ISINGLReader();
        }

        [Test]
        public void ISINGLReader_ReadISINGL_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.AreEqual(0f, _isinglReader.ReadISINGL(s));
        }

        [Test]
        public void ISINGLReader_ReadISINGL_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.AreEqual(0f, _isinglReader.ReadISINGL(s));
        }

        [Test]
        public void ISINGLReader_ReadISINGL_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0xC2, 0x76, 0xA0 });
            Assert.AreEqual(0f, _isinglReader.ReadISINGL(s));
        }

        [Test]
        public void ISINGLReader_ReadISINGL_Pass_Positive()
        {
            var ibmFloats = new byte[]
            {
                0xC2, 0x76, 0xA0, 0x00,
                0x42, 0x6C, 0xAD, 0x15
            };
            var s = new MemoryStream(ibmFloats);
            Assert.AreEqual(-118.625f, _isinglReader.ReadISINGL(s));
            Assert.AreEqual(108.676102f, _isinglReader.ReadISINGL(s));
        }
    }
}
