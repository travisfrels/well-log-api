using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasStreamHelpersTests
    {
        private static readonly MemoryStream nullStream = null;
        private static readonly MemoryStream emptyStream = new MemoryStream();
        private static readonly MemoryStream badCharStream = new MemoryStream(new byte[] { 9, 13, 10 });
        private static readonly MemoryStream lasStream = new MemoryStream(new byte[] { 126, 86, 13, 10 });

        [SetUp]
        public void PerTestSetup()
        {
            badCharStream.Seek(0, SeekOrigin.Begin);
            lasStream.Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void LasStreamHelpers_ReadLasByte_Pass()
        {
            Assert.AreEqual(0, nullStream.ReadLasByte());
            Assert.AreEqual(0, emptyStream.ReadLasByte());
            Assert.AreEqual(32, badCharStream.ReadLasByte());
            Assert.AreEqual(126, lasStream.ReadLasByte());
            Assert.AreEqual(86, lasStream.ReadLasByte());
            Assert.AreEqual(13, lasStream.ReadLasByte());
            Assert.AreEqual(10, lasStream.ReadLasByte());
        }

        [Test]
        public void LasStreamHelpers_ReadLasLine_Pass()
        {
            Assert.IsNull(nullStream.ReadLasLine());
            Assert.IsNull(emptyStream.ReadLasLine());
            Assert.AreEqual(" ", badCharStream.ReadLasLine());
            Assert.AreEqual("~V", lasStream.ReadLasLine());
        }

        [Test]
        public void LasStreamHelpers_SeekBackLine_Pass()
        {
            Assert.DoesNotThrow(() => { nullStream.SeekBackLine("~V"); });
            Assert.DoesNotThrow(() => { emptyStream.SeekBackLine("~V"); });

            var lasLine1 = lasStream.ReadLasLine();
            lasStream.SeekBackLine(lasLine1);
            var lasLine2 = lasStream.ReadLasLine();

            Assert.AreEqual(lasLine1, lasLine2);
        }
    }
}
