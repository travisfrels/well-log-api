using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class VSINGLReaderTests
    {
        private VSINGLReader _vsinglReader;

        [SetUp]
        public void PerTestSetup()
        {
            _vsinglReader = new VSINGLReader();
        }

        [Test]
        public void VSINGLReader_ReadVSINGL_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.AreEqual(0f, _vsinglReader.ReadVSINGL(s));
        }

        [Test]
        public void VSINGLReader_ReadVSINGL_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.AreEqual(0f, _vsinglReader.ReadVSINGL(s));
        }

        [Test]
        public void VSINGLReader_ReadVSINGL_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0x80, 0x40, 0x00 });
            Assert.AreEqual(0f, _vsinglReader.ReadVSINGL(s));
        }

        [Test]
        public void VSINGLReader_ReadVSINGL_Pass_Positive()
        {
            var vaxFloats = new byte[] {
                0x80, 0x40, 0x00, 0x00,
                0x80, 0xC0, 0x00, 0x00,
                0x60, 0x41, 0x00, 0x00,
                0x60, 0xC1, 0x00, 0x00,
                0x49, 0x41, 0xD0, 0x0F,
                0x49, 0xC1, 0xD0, 0x0F,
                0xF0, 0x7D, 0xC2, 0xBD,
                0xF0, 0xFD, 0xC2, 0xBD,
                0x08, 0x03, 0xEA, 0x1C,
                0x08, 0x83, 0xEA, 0x1C,
                0x9E, 0x40, 0x52, 0x06,
                0x9E, 0xC0, 0x52, 0x06
            };

            var s = new MemoryStream(vaxFloats);

            Assert.AreEqual(1f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-1f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(3.5f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-3.5f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(3.14159f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-3.14159f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(9.9999999E+36f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-9.9999999E+36f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(9.9999999E-38f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-9.9999999E-38f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(1.23456788f, _vsinglReader.ReadVSINGL(s));
            Assert.AreEqual(-1.23456788f, _vsinglReader.ReadVSINGL(s));
        }
    }
}
