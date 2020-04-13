using NUnit.Framework;
using System.IO;
using WellLog.Lib.Factories.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class UVARIReaderTests
    {
        private UVARIReader _uvariReader;

        [SetUp]
        public void PerTestSetup()
        {
            _uvariReader = new UVARIReader();
        }

        [Test]
        public void UVARIReader_ReadUVARI1_Pass_NullStream()
        {
            MemoryStream s = null;
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI1(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI1_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[0]);
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI1(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI1_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI1(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI2_Pass_NullStream()
        {
            MemoryStream s = null;
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI2(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI2_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_1111 });
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI2(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI2_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI2(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI4_Pass_NullStream()
        {
            MemoryStream s = null;
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI4(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI4_Pass_ShortStream()
        {
            var s = new MemoryStream(new byte[] { 0b_1100_0000, 0b_1111_1111, 0b_1111_1111 });
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI4(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI4_Pass()
        {
            var s = new MemoryStream(new byte[] { 0b_1100_0000, 0b_0000_0000, 0b_0000_0000, 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI4(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_NullStream()
        {
            MemoryStream s = null;
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_UVARI1()
        {
            var s = new MemoryStream(new byte[] { 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_ShortUVARI2()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0001 });
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_UVARI2()
        {
            var s = new MemoryStream(new byte[] { 0b_1000_0001, 0b_0000_0000 });
            uint expected = 256;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_ShortUVARI4()
        {
            var s = new MemoryStream(new byte[] { 0b_1100_0001, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 0;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }

        [Test]
        public void UVARIReader_ReadUVARI_Pass_UVARI4()
        {
            var s = new MemoryStream(new byte[] { 0b_1100_0001, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 16777216;
            Assert.AreEqual(expected, _uvariReader.ReadUVARI(s));
        }
    }
}
