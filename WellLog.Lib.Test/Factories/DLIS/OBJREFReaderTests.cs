using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class OBJREFReaderTests
    {
        private IDENTReader _identReader;
        private UVARIReader _uvariReader;
        private USHORTReader _ushortReader;
        private OBNAMEReader _obnameReader;
        private OBJREFReader _objrefReader;

        [SetUp]
        public void PerTestSetup()
        {
            _identReader = new IDENTReader();
            _uvariReader = new UVARIReader();
            _ushortReader = new USHORTReader();
            _obnameReader = new OBNAMEReader(_uvariReader, _ushortReader, _identReader);
            _objrefReader = new OBJREFReader(_identReader, _obnameReader);
        }

        [Test]
        public void OBJREFReader_ReadOBJREF_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_objrefReader.ReadOBJREF(s));
        }

        [Test]
        public void OBJREFReader_ReadOBJREF_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_objrefReader.ReadOBJREF(s));
        }

        [Test]
        public void OBJREFReader_ReadOBJREF_Pass()
        {
            var s = new MemoryStream(new byte[] { 0x01, 0x41, 0x01, 0x02, 0x01, 0x42 });

            var objRef = _objrefReader.ReadOBJREF(s);
            Assert.AreEqual("A", objRef.ObjectType);
            Assert.AreEqual(1, objRef.Name.Origin);
            Assert.AreEqual(2, objRef.Name.CopyNumber);
            Assert.AreEqual("B", objRef.Name.Identifier);
        }
    }
}
