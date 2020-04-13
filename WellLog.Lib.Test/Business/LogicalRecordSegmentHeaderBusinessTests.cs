using NUnit.Framework;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentHeaderBusinessTests
    {
        private UNORMReader _unormReader;
        private USHORTReader _ushortReader;
        private LogicalRecordSegmentHeaderBusiness _logicalRecordSegmentHeaderBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _unormReader = new UNORMReader();
            _ushortReader = new USHORTReader();
            _logicalRecordSegmentHeaderBusiness = new LogicalRecordSegmentHeaderBusiness(_unormReader, _ushortReader);
        }

        [Test]
        public void LogicalRecordSegmentHeaderBusiness_ReadLogicalRecordSegmentHeader_Pass_NullStream()
        {
            MemoryStream dlisStream = null;
            Assert.IsNull(_logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream));
        }

        [Test]
        public void LogicalRecordSegmentHeaderBusiness_ReadLogicalRecordSegmentHeader_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(_logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream));
        }

        [Test]
        public void LogicalRecordSegmentHeaderBusiness_ReadLogicalRecordSegmentHeader_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x55, 0x03 });
            var result = _logicalRecordSegmentHeaderBusiness.ReadLogicalRecordSegmentHeader(dlisStream);
            Assert.AreEqual(1, result.LogicalRecordSegmentLength);
            Assert.IsFalse(result.LogicalRecordStructure);
            Assert.IsTrue(result.Predecessor);
            Assert.IsFalse(result.Successor);
            Assert.IsTrue(result.Encryption);
            Assert.IsFalse(result.EncryptionPacket);
            Assert.IsTrue(result.Checksum);
            Assert.IsFalse(result.TrailingLength);
            Assert.IsTrue(result.Padding);
            Assert.AreEqual(3, result.LogicalRecordType);
        }
    }
}
