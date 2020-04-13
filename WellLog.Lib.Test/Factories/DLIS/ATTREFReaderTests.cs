using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class ATTREFReaderTests
    {
        private IDENTReader _identReader;
        private UVARIReader _uvariReader;
        private USHORTReader _ushortReader;
        private OBNAMEReader _obnameReader;
        private ATTREFReader _attrefReader;

        [SetUp]
        public void PerTestSetup()
        {
            _identReader = new IDENTReader();
            _uvariReader = new UVARIReader();
            _ushortReader = new USHORTReader();
            _obnameReader = new OBNAMEReader(_uvariReader, _ushortReader, _identReader);
            _attrefReader = new ATTREFReader(_identReader, _obnameReader);
        }

        [Test]
        public void ATTREFReader_ReadATTREF_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_attrefReader.ReadATTREF(s));
        }

        [Test]
        public void ATTREFReader_ReadATTREF_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_attrefReader.ReadATTREF(s));
        }

        [Test]
        public void ATTREFReader_ReadATTREF_Pass()
        {
            var s = new MemoryStream(new byte[] { 0x01, 0x41, 0x01, 0x02, 0x01, 0x42, 0x01, 0x43 });

            var attRef = _attrefReader.ReadATTREF(s);
            Assert.AreEqual("A", attRef.ObjectType);
            Assert.AreEqual(1, attRef.Name.Origin);
            Assert.AreEqual(2, attRef.Name.CopyNumber);
            Assert.AreEqual("B", attRef.Name.Identifier);
            Assert.AreEqual("C", attRef.Label);
        }
    }
}
