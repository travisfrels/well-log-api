using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class SSHORTReaderTests
    {
        private SSHORTReader _sshortReader;

        [SetUp]
        public void PerTestSetup()
        {
            _sshortReader = new SSHORTReader();
        }

        [Test]
        public void SSHORTReader_ReadSSHORT_Pass_NullStream()
        {
            MemoryStream s = null;
            sbyte expected = 0;
            Assert.AreEqual(expected, _sshortReader.ReadSSHORT(s));
        }

        [Test]
        public void SSHORTReader_ReadSSHORT_Pass_EmptyStream()
        {
            var s = new MemoryStream();
            sbyte expected = 0;
            Assert.AreEqual(expected, _sshortReader.ReadSSHORT(s));
        }

        [Test]
        public void SSHORTReader_ReadSSHORT_Pass_Positive()
        {
            var s = new MemoryStream(new byte[] { 127 });
            sbyte expected = 127;
            Assert.AreEqual(expected, _sshortReader.ReadSSHORT(s));
        }

        [Test]
        public void SSHORTReader_ReadSSHORT_Pass_Negative()
        {
            var s = new MemoryStream(new byte[] { 0b_1111_1110 });
            sbyte expected = -2;
            Assert.AreEqual(expected, _sshortReader.ReadSSHORT(s));
        }
    }
}
