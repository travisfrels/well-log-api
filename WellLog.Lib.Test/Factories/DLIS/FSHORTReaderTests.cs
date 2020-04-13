using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class FSHORTReaderTests
    {
        private FSHORTReader _fshortReader;

        [SetUp]
        public void PerTestSetup()
        {
            _fshortReader = new FSHORTReader();
        }

        [Test]
        public void FSHORTReader_ReadFSHORT_Pass_NullStream()
        {
            MemoryStream s = null;
            var expected = 0f;
            Assert.AreEqual(expected, _fshortReader.ReadFSHORT(s));
        }

        [Test]
        public void FSHORTReader_ReadFSHORT_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            var expected = 0f;
            Assert.AreEqual(expected, _fshortReader.ReadFSHORT(s));
        }

        [Test]
        public void FSHORTReader_ReadFSHORT_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0000 });
            var expected = 0f;
            Assert.AreEqual(expected, _fshortReader.ReadFSHORT(s));
        }

        [Test]
        public void FSHORTReader_ReadFSHORT_Pass_Positive()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0001_1000 });
            var expected = 0.125f;
            Assert.AreEqual(expected, _fshortReader.ReadFSHORT(s));
        }

        [Test]
        public void FSHORTReader_ReadFSHORT_Pass_Negative()
        {
            var s = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1000 });
            var expected = -0.125f;
            Assert.AreEqual(expected, _fshortReader.ReadFSHORT(s));
        }
    }
}
