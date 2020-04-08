using NUnit.Framework;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentEncryptionPacketBusinessTests
    {
        private LogicalRecordSegmentEncryptionPacketBusiness _logicalRecordSegmentEncryptionPacketBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _logicalRecordSegmentEncryptionPacketBusiness = new LogicalRecordSegmentEncryptionPacketBusiness();
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_NullStream()
        {
            MemoryStream dlisStream = null;
            Assert.IsNull(_logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream));
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(_logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream));
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_EmptyPacket()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x04, 0x00, 0x02, 0x03 });
            var result = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream);
            Assert.AreEqual(4, result.Size);
            Assert.AreEqual(2, result.CompanyCode);
            Assert.AreEqual(0, result.EncryptionInfo.Count());
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x05, 0x00, 0x02, 0x03 });
            var result = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(dlisStream);
            Assert.AreEqual(5, result.Size);
            Assert.AreEqual(2, result.CompanyCode);
            Assert.AreEqual(1, result.EncryptionInfo.Count());
        }
    }
}
