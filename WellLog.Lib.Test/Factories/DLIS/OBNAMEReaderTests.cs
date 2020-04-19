using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class OBNAMEReaderTests
    {
        private UVARIReader _uvariReader;
        private USHORTReader _ushortReader;
        private IDENTReader _identReader;
        private OBNAMEReader _obnameReader;

        [SetUp]
        public void PerTestSetup()
        {
            _uvariReader = new UVARIReader();
            _ushortReader = new USHORTReader();
            _identReader = new IDENTReader();
            _obnameReader = new OBNAMEReader(_uvariReader, _ushortReader, _identReader);
        }

        [Test]
        public void OBNAMEReader_ReadOBNAME_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_obnameReader.ReadOBNAME(s));
        }

        [Test]
        public void OBNAMEReader_ReadOBNAME_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_obnameReader.ReadOBNAME(s));
        }

        [Test]
        public void OBNAMEReader_ReadOBNAME_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0000_0001, 0b_0000_0010, 0b_0000_0001, 0b_0100_0001 });
            var obName = _obnameReader.ReadOBNAME(s);
            Assert.AreEqual(1, obName.Origin);
            Assert.AreEqual(2, obName.CopyNumber);
            Assert.AreEqual("A", obName.Identifier);
        }
    }
}
