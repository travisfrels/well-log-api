using NUnit.Framework;
using System;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class LasStreamHelpersTests
    {
        private static readonly MemoryStream nullStream = null;

        [Test]
        public void LasStreamHelpers_ToLasByte_Pass_CarriageReturn()
        {
            Assert.AreEqual(Convert.ToByte(10), Convert.ToByte(10).ToLasByte());
        }

        [Test]
        public void LasStreamHelpers_ToLasByte_Pass_LineFeed()
        {
            Assert.AreEqual(Convert.ToByte(13), Convert.ToByte(13).ToLasByte());
        }

        [Test]
        public void LasStreamHelpers_ToLasByte_Pass_AcceptedCharacters()
        {
            Assert.AreEqual(Convert.ToByte(32), Convert.ToByte(32).ToLasByte());
            Assert.AreEqual(Convert.ToByte(126), Convert.ToByte(126).ToLasByte());
        }

        [Test]
        public void LasStreamHelpers_ToLasByte_Pass_InvalidCharacterToSpace()
        {
            Assert.AreEqual(Convert.ToByte(32), Convert.ToByte(9).ToLasByte());
        }

        [Test]
        public void LasStreamHelpers_ReadLasByte_Pass_NullStream()
        {
            Assert.AreEqual(0, nullStream.ReadLasByte());
        }

        [Test]
        public void LasStreamHelpers_ReadLasByte_Pass_EmptyStream()
        {
            Assert.AreEqual(0, (new MemoryStream()).ReadLasByte());
        }

        [Test]
        public void LasStreamHelpers_ReadLasByte_Pass()
        {
            Assert.AreEqual(126, new MemoryStream(new byte[] { 126 }).ReadLasByte());
        }

        [Test]
        public void LasStreamHelpers_ReadLasLine_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadLasLine());
        }

        [Test]
        public void LasStreamHelpers_ReadLasLine_Pass_EmptyStream()
        {
            Assert.IsNull((new MemoryStream()).ReadLasLine());
        }

        [Test]
        public void LasStreamHelpers_ReadLasLine_Pass()
        {
            Assert.AreEqual("~V", new MemoryStream(new byte[] { 126, 86, 13, 10 }).ReadLasLine());
        }

        [Test]
        public void LasStreamHelpers_SeekBackLine_Pass_NullStream()
        {
            Assert.DoesNotThrow(() => { nullStream.SeekBackLine("~V"); });
        }

        [Test]
        public void LasStreamHelpers_SeekBackLine_Pass_EmptyStream()
        {
            Assert.DoesNotThrow(() => { (new MemoryStream()).SeekBackLine("~V"); });
        }

        [Test]
        public void LasStreamHelpers_SeekBackLine_Pass()
        {
            var lasStream = new MemoryStream(new byte[] { 126, 86, 13, 10 });
            var lasLine1 = lasStream.ReadLasLine();
            lasStream.SeekBackLine(lasLine1);
            var lasLine2 = lasStream.ReadLasLine();

            Assert.AreEqual(lasLine1, lasLine2);
        }

        [Test]
        public void LasStreamHelpers_WriteLasLine_Pass_NullStream()
        {
            Assert.DoesNotThrow(() => nullStream.WriteLasLine("~V"));
        }

        [Test]
        public void LasStreamHelpers_WriteLasLine_Pass_ReadOnlyStream()
        {
            Assert.DoesNotThrow(() => (new MemoryStream(new byte[0], false)).WriteLasLine("~V"));
        }

        [Test]
        public void LasStreamHelpers_WriteLasLine_Pass_WriteEmptyString()
        {
            var lasStream = new MemoryStream();
            lasStream.WriteLasLine(string.Empty);
            Assert.AreEqual(2, lasStream.Length);
        }

        [Test]
        public void LasStreamHelpers_WriteLasLine_Pass_WriteWhiteSpace()
        {
            var lasStream = new MemoryStream();
            lasStream.WriteLasLine(" \t ");
            Assert.AreEqual(2, lasStream.Length);
        }

        [Test]
        public void LasStreamHelpers_WriteLasLine_Pass()
        {
            var lasStream = new MemoryStream();
            lasStream.WriteLasLine("~V");
            Assert.AreEqual(4, lasStream.Length);
        }
    }
}
