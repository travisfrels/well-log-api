using Moq;
using NUnit.Framework;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentBusinessTests
    {
        private Mock<ILogicalRecordSegmentHeaderBusiness> _logicalRecordSegmentHeaderBusiness;
        private Mock<ILogicalRecordSegmentEncryptionPacketBusiness> _logicalRecordSegmentEncryptionPacketBusiness;
        private Mock<ILogicalRecordSegmentTrailerBusiness> _logicalRecordSegmentTrailerBusiness;
        private LogicalRecordSegmentBusiness _logicalRecordSegmentBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _logicalRecordSegmentHeaderBusiness = new Mock<ILogicalRecordSegmentHeaderBusiness>();
            _logicalRecordSegmentEncryptionPacketBusiness = new Mock<ILogicalRecordSegmentEncryptionPacketBusiness>();
            _logicalRecordSegmentTrailerBusiness = new Mock<ILogicalRecordSegmentTrailerBusiness>();
            _logicalRecordSegmentBusiness = new LogicalRecordSegmentBusiness(_logicalRecordSegmentHeaderBusiness.Object, _logicalRecordSegmentEncryptionPacketBusiness.Object, _logicalRecordSegmentTrailerBusiness.Object);
        }

        [Test]
        public void LogicalRecordSegmentBusiness_ReadLogicalRecordSegment_Pass_NullStream()
        {
            MemoryStream dlisStream = null;
            Assert.IsNull(_logicalRecordSegmentBusiness.ReadLogicalRecordSegment(dlisStream));
        }

        [Test]
        public void LogicalRecordSegmentBusiness_ReadLogicalRecordSegment_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(_logicalRecordSegmentBusiness.ReadLogicalRecordSegment(dlisStream));
        }


        [Test]
        public void LogicalRecordSegmentBusiness_ReadLogicalRecordSegment_Pass()
        {
            var dlisData = new byte[]
            {
                0x00, 0x14, 0xFF, 0x00,
                0x00, 0x06, 0x00, 0x01,
                0x00, 0x00, 0x0B, 0x0C,
                0x0D, 0x0E, 0x00, 0x02,
                0x00, 0x01, 0x00, 0x14
            };
            var dlisStream = new MemoryStream(dlisData);
            var header = new LogicalRecordSegmentHeader
            {
                LogicalRecordSegmentLength = 20,
                LogicalRecordSegmentAttributes = 0xFF
            };
            var encryptionPacket = new LogicalRecordSegmentEncryptionPacket
            {
                Size = 6,
                CompanyCode = 1,
                EncryptionInfo = new byte[] { 0, 0 }
            };
            var trailer = new LogicalRecordSegmentTrailer
            {
                PadCount = 2,
                Padding = new byte[] { 0, 2 },
                Checksum = 1,
                TrailingLength = 20
            };

            _logicalRecordSegmentHeaderBusiness.Setup(x => x.ReadLogicalRecordSegmentHeader(dlisStream)).Returns(header);
            _logicalRecordSegmentEncryptionPacketBusiness.Setup(x => x.ReadLogicalRecordSegmentEncryptionPacket(dlisStream)).Returns(encryptionPacket);
            _logicalRecordSegmentTrailerBusiness.Setup(x => x.ReadLogicalRecordSegmentTrailer(dlisStream, header)).Returns(trailer);

            /* simulate the mocked objects reading the header and encryption packet by seeking 10 bytes */
            dlisStream.Seek(10, SeekOrigin.Begin);
            var result = _logicalRecordSegmentBusiness.ReadLogicalRecordSegment(dlisStream);

            Assert.AreEqual(0x0B, ((byte[])result.Body)[0]);
            Assert.AreEqual(0x0C, ((byte[])result.Body)[1]);
            Assert.AreEqual(0x0D, ((byte[])result.Body)[2]);
            Assert.AreEqual(0x0E, ((byte[])result.Body)[3]);
        }
    }
}
