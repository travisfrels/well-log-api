using NUnit.Framework;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentTrailerBusinessBusinessTests
    {
        private LogicalRecordSegmentTrailerBusiness _logicalRecordSegmentTrailerBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _logicalRecordSegmentTrailerBusiness = new LogicalRecordSegmentTrailerBusiness();
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass_NullStream()
        {
            MemoryStream dlisStream = null;
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader();
            Assert.IsNull(_logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader));
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                LogicalRecordSegmentAttributes = 0x03
            };
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
            Assert.AreEqual(0, result.Padding);
            Assert.AreEqual(0, result.Checksum);
            Assert.AreEqual(0, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x02, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set padding, checksum, and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0111
            };
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
            Assert.AreEqual(1, result.Padding);
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass_NoPadding()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x02, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set checksum and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0110
            };
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
            Assert.AreEqual(0, result.Padding);
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass_NoChecksum()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set padding and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0011
            };
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
            Assert.AreEqual(1, result.Padding);
            Assert.AreEqual(0, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentHeader_Pass_NoTrailingLength()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x02, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set checksum and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0101
            };
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
            Assert.AreEqual(1, result.Padding);
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(0, result.TrailingLength);
        }
    }
}
