using NUnit.Framework;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Test.Business
{
    [TestFixture]
    public class LogicalRecordSegmentTrailerBusinessTests
    {
        private LogicalRecordSegmentTrailerBusiness _logicalRecordSegmentTrailerBusiness;

        [SetUp]
        public void PerTestSetup()
        {
            _logicalRecordSegmentTrailerBusiness = new LogicalRecordSegmentTrailerBusiness();
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_NullStream()
        {
            MemoryStream dlisStream = null;
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader();
            Assert.IsNull(_logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader));
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader();
            Assert.IsNull(_logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader));
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x02, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set padding, checksum, and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0111
            };

            dlisStream.Seek(5, SeekOrigin.Current);
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);

            Assert.AreEqual(1, result.PadCount);
            Assert.AreEqual(1, result.Padding.Count());
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_NoPadding()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x02, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set checksum and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0110
            };

            dlisStream.Seek(4, SeekOrigin.Current);
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);

            Assert.AreEqual(0, result.PadCount);
            Assert.IsNull(result.Padding);
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_ExtraPadding()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x00, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set checksum and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0001
            };

            dlisStream.Seek(3, SeekOrigin.Current);
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);

            Assert.AreEqual(3, result.PadCount);
            Assert.AreEqual(3, result.Padding.Count());
            Assert.AreEqual(0, result.Checksum);
            Assert.AreEqual(0, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_NoChecksum()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x03 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set padding and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0011
            };

            dlisStream.Seek(3, SeekOrigin.Current);
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);
 
            Assert.AreEqual(1, result.PadCount);
            Assert.AreEqual(1, result.Padding.Count());
            Assert.AreEqual(0, result.Checksum);
            Assert.AreEqual(3, result.TrailingLength);
        }

        [Test]
        public void LogicalRecordSegmentTrailerBusiness_ReadLogicalRecordSegmentTrailer_Pass_NoTrailingLength()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x01, 0x00, 0x02 });
            var logicalRecordSegmentHeader = new LogicalRecordSegmentHeader
            {
                /* set checksum and trailing length flags to true */
                LogicalRecordSegmentAttributes = 0b_0000_0101
            };

            dlisStream.Seek(3, SeekOrigin.Current);
            var result = _logicalRecordSegmentTrailerBusiness.ReadLogicalRecordSegmentTrailer(dlisStream, logicalRecordSegmentHeader);

            Assert.AreEqual(1, result.PadCount);
            Assert.AreEqual(1, result.Padding.Count());
            Assert.AreEqual(2, result.Checksum);
            Assert.AreEqual(0, result.TrailingLength);
        }
    }
}
