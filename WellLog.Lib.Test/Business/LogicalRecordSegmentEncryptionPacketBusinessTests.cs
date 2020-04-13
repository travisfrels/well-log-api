using NUnit.Framework;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentEncryptionPacketBusinessTests
    {
        private UNORMReader _unormReader;
        private LogicalRecordSegmentEncryptionPacketBusiness _logicalRecordSegmentEncryptionPacketBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _unormReader = new UNORMReader();
            _logicalRecordSegmentEncryptionPacketBusiness = new LogicalRecordSegmentEncryptionPacketBusiness(_unormReader);
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_NullStream()
        {
            MemoryStream s = null;
            Assert.IsNull(_logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(s));
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            Assert.IsNull(_logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(s));
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass_EmptyPacket()
        {
            var s = new MemoryStream(new byte[] { 0x00, 0x04, 0x00, 0x02, 0x03 });
            var result = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(s);
            Assert.AreEqual(4, result.Size);
            Assert.AreEqual(2, result.CompanyCode);
            Assert.AreEqual(0, result.EncryptionInfo.Count());
        }

        [Test]
        public void LogicalRecordSegmentEncryptionPacketBusiness_ReadLogicalRecordSegmentEncryptionPacket_Pass()
        {
            var s = new MemoryStream(new byte[] { 0x00, 0x05, 0x00, 0x02, 0x03 });
            var result = _logicalRecordSegmentEncryptionPacketBusiness.ReadLogicalRecordSegmentEncryptionPacket(s);
            Assert.AreEqual(5, result.Size);
            Assert.AreEqual(2, result.CompanyCode);
            Assert.AreEqual(1, result.EncryptionInfo.Count());
        }
    }
}
