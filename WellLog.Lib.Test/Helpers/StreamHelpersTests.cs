using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    class StreamHelpersTests
    {
        private static readonly MemoryStream nullStream = null;

        [Test]
        public void StreamHelpers_ReadBytes_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadBytes(1));
        }

        [Test]
        public void StreamHelpers_ReadBytes_Pass_NegativeBytes()
        {
            var ms = new MemoryStream();
            Assert.IsNull(ms.ReadBytes(-1));
        }

        [Test]
        public void StreamHelpers_ReadBytes_Pass_ZeroBytes()
        {
            var ms = new MemoryStream();
            Assert.AreEqual(0, ms.ReadBytes(0).Length);
        }

        [Test]
        public void StreamHelpers_ReadBytes_Pass_ShortStream()
        {
            var ms = new MemoryStream(new byte[3] { 1, 2, 3 });
            Assert.IsNull(ms.ReadBytes(4));
        }

        [Test]
        public void StreamHelpers_ReadBytes_Pass()
        {
            var expected = new byte[4] { 1, 2, 3, 4 };
            var ms = new MemoryStream(expected);
            Assert.AreEqual(expected, ms.ReadBytes(4));
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_NullStream()
        {
            Assert.IsFalse(nullStream.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_EmptyStream()
        {
            var ms = new MemoryStream();
            Assert.IsTrue(ms.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_BeginningOfStream()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            Assert.IsFalse(ms.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_SeekEndOfStream()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            ms.Seek(3, SeekOrigin.Begin);
            Assert.IsTrue(ms.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_SeekNearEndOfStream()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            ms.Seek(2, SeekOrigin.Begin);
            Assert.IsFalse(ms.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_ReadEndOfStream()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            var buffer = new byte[3];
            ms.Read(buffer, 0, 3);
            Assert.IsTrue(ms.IsAtEndOfStream());
        }

        [Test]
        public void StreamHelpers_IsAtEndOfStream_Pass_ReadNearEndOfStream()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            var buffer = new byte[2];
            ms.Read(buffer, 0, 2);
            Assert.IsFalse(ms.IsAtEndOfStream());
        }
    }
}
