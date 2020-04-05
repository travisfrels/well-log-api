using NUnit.Framework;
using System;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Test.Helpers
{
    [TestFixture]
    public class DlisStreamHelpersTests
    {
        private static readonly MemoryStream nullStream = null;

        [Test]
        public void DlisStreamHelpers_ReadFSHORT_Pass_NullStream()
        {
            var expected = 0f;
            Assert.AreEqual(expected, nullStream.ReadFSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSHORT_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            var expected = 0f;
            Assert.AreEqual(expected, dlisStream.ReadFSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSHORT_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0000 });
            var expected = 0f;
            Assert.AreEqual(expected, dlisStream.ReadFSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSHORT_Pass_Positive()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0001_1000 });
            var expected = 0.125f;
            Assert.AreEqual(expected, dlisStream.ReadFSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSHORT_Pass_Negative()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1000 });
            var expected = -0.125f;
            Assert.AreEqual(expected, dlisStream.ReadFSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSINGL_Pass_NullStream()
        {
            var expected = 0f;
            Assert.AreEqual(expected, nullStream.ReadFSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSINGL_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            var expected = 0f;
            Assert.AreEqual(expected, dlisStream.ReadFSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSINGL_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0000, 0b_0100_1001, 0b_0000_1111 });
            var expected = 0f;
            Assert.AreEqual(expected, dlisStream.ReadFSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSINGL_Pass_PositivePi()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var expected = 3.14159274f;
            Assert.AreEqual(expected, dlisStream.ReadFSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSINGL_Pass_NegativePi()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var expected = -3.14159274f;
            Assert.AreEqual(expected, dlisStream.ReadFSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING1_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadFSING1());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING1_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadFSING1());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING1_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_1100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000 });
            var fSing1 = dlisStream.ReadFSING1();
            Assert.AreEqual(153f, fSing1.V);
            Assert.AreEqual(-153f, fSing1.A);
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING2_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadFSING2());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING2_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadFSING2());
        }

        [Test]
        public void DlisStreamHelpers_ReadFSING2_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_1100_0011, 0b_0001_1001, 0b_0000_0000, 0b_0000_0000, 0b_0100_0000, 0b_0100_1001, 0b_0000_1111, 0b_1101_1011 });
            var fSing1 = dlisStream.ReadFSING2();
            Assert.AreEqual(153f, fSing1.V);
            Assert.AreEqual(-153f, fSing1.A);
            Assert.AreEqual(3.14159274f, fSing1.B);
        }

        [Test]
        public void DlisStreamHelpers_ReadISINGL_Pass_NullStream()
        {
            Assert.AreEqual(0f, nullStream.ReadISINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadISINGL_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.AreEqual(0f, dlisStream.ReadISINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadISINGL_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0xC2, 0x76, 0xA0 });
            Assert.AreEqual(0f, dlisStream.ReadISINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadISINGL_Pass_Positive()
        {
            var ibmFloats = new byte[]
            {
                0xC2, 0x76, 0xA0, 0x00,
                0x42, 0x6C, 0xAD, 0x15
            };
            var dlisStream = new MemoryStream(ibmFloats);
            Assert.AreEqual(-118.625f, dlisStream.ReadISINGL());
            Assert.AreEqual(108.676102f, dlisStream.ReadISINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadVSINGL_Pass_NullStream()
        {
            Assert.AreEqual(0f, nullStream.ReadVSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadVSINGL_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.AreEqual(0f, dlisStream.ReadVSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadVSINGL_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0x80, 0x40, 0x00 });
            Assert.AreEqual(0f, dlisStream.ReadVSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadVSINGL_Pass_Positive()
        {
            var vaxFloats = new byte[] {
                0x80, 0x40, 0x00, 0x00,
                0x80, 0xC0, 0x00, 0x00,
                0x60, 0x41, 0x00, 0x00,
                0x60, 0xC1, 0x00, 0x00,
                0x49, 0x41, 0xD0, 0x0F,
                0x49, 0xC1, 0xD0, 0x0F,
                0xF0, 0x7D, 0xC2, 0xBD,
                0xF0, 0xFD, 0xC2, 0xBD,
                0x08, 0x03, 0xEA, 0x1C,
                0x08, 0x83, 0xEA, 0x1C,
                0x9E, 0x40, 0x52, 0x06,
                0x9E, 0xC0, 0x52, 0x06
            };

            var dlisStream = new MemoryStream(vaxFloats);

            Assert.AreEqual(1f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-1f, dlisStream.ReadVSINGL());
            Assert.AreEqual(3.5f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-3.5f, dlisStream.ReadVSINGL());
            Assert.AreEqual(3.14159f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-3.14159f, dlisStream.ReadVSINGL());
            Assert.AreEqual(9.9999999E+36f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-9.9999999E+36f, dlisStream.ReadVSINGL());
            Assert.AreEqual(9.9999999E-38f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-9.9999999E-38f, dlisStream.ReadVSINGL());
            Assert.AreEqual(1.23456788f, dlisStream.ReadVSINGL());
            Assert.AreEqual(-1.23456788f, dlisStream.ReadVSINGL());
        }

        [Test]
        public void DlisStreamHelpers_ReadSSHORT_Pass_NullStream()
        {
            sbyte expected = 0;
            Assert.AreEqual(expected, nullStream.ReadSSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadSSHORT_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            sbyte expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadSSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadSSHORT_Pass_Positive()
        {
            var dlisStream = new MemoryStream(new byte[] { 127 });
            sbyte expected = 127;
            Assert.AreEqual(expected, dlisStream.ReadSSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadSSHORT_Pass_Negative()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1111_1110 });
            sbyte expected = -2;
            Assert.AreEqual(expected, dlisStream.ReadSSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadSNORM_Pass_NullStream()
        {
            short expected = 0;
            Assert.AreEqual(expected, nullStream.ReadSNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadSNORM_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 255 });
            short expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadSNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadSNORM_Pass_Positive()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0000_0010 });
            short expected = 2;
            Assert.AreEqual(expected, dlisStream.ReadSNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadSNORM_Pass_Negative()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1110 });
            short expected = -2;
            Assert.AreEqual(expected, dlisStream.ReadSNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadSLONG_Pass_NullStream()
        {
            int expected = 0;
            Assert.AreEqual(expected, nullStream.ReadSLONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadSLONG_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 255, 255, 255 });
            int expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadSLONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadSLONG_Pass_Positive()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0010 });
            int expected = 2;
            Assert.AreEqual(expected, dlisStream.ReadSLONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadSLONG_Pass_Negative()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1111_1111, 0b_1111_1111, 0b_1111_1111, 0b_1111_1110 });
            int expected = -2;
            Assert.AreEqual(expected, dlisStream.ReadSLONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadUSHORT_Pass_NullStream()
        {
            byte expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadUSHORT_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[0]);
            byte expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadUSHORT_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0000 });
            byte expected = 128;
            Assert.AreEqual(expected, dlisStream.ReadUSHORT());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNORM_Pass_NullStream()
        {
            ushort expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNORM_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 255 });
            ushort expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNORM_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0000_0000 });
            ushort expected = 32768;
            Assert.AreEqual(expected, dlisStream.ReadUNORM());
        }

        [Test]
        public void DlisStreamHelpers_ReadULONG_Pass_NullStream()
        {
            uint expected = 0;
            Assert.AreEqual(expected, nullStream.ReadULONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadULONG_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 255, 255, 255 });
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadULONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadULONG_Pass_Positive()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 2147483648;
            Assert.AreEqual(expected, dlisStream.ReadULONG());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI1_Pass_NullStream()
        {
            uint expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUVARI1());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI1_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[0]);
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUVARI1());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI1_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, dlisStream.ReadUVARI1());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI2_Pass_NullStream()
        {
            uint expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUVARI2());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI2_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_1111 });
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUVARI2());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI2_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0000, 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, dlisStream.ReadUVARI2());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI4_Pass_NullStream()
        {
            uint expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUVARI4());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI4_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1100_0000, 0b_1111_1111, 0b_1111_1111 });
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUVARI4());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI4_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1100_0000, 0b_0000_0000, 0b_0000_0000, 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, dlisStream.ReadUVARI4());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_NullStream()
        {
            uint expected = 0;
            Assert.AreEqual(expected, nullStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_UVARI1()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0100_0000 });
            uint expected = 64;
            Assert.AreEqual(expected, dlisStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_ShortUVARI2()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0001 });
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_UVARI2()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1000_0001, 0b_0000_0000 });
            uint expected = 256;
            Assert.AreEqual(expected, dlisStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_ShortUVARI4()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1100_0001, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 0;
            Assert.AreEqual(expected, dlisStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadUVARI_Pass_UVARI4()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_1100_0001, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000 });
            uint expected = 16777216;
            Assert.AreEqual(expected, dlisStream.ReadUVARI());
        }

        [Test]
        public void DlisStreamHelpers_ReadIDENT_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadIDENT());
        }

        [Test]
        public void DlisStreamHelpers_ReadIDENT_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream(new byte[0]);
            Assert.IsNull(dlisStream.ReadIDENT());
        }

        [Test]
        public void DlisStreamHelpers_ReadIDENT_Pass_NoChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(string.Empty, dlisStream.ReadIDENT());
        }

        [Test]
        public void DlisStreamHelpers_ReadIDENT_Pass_TooFewChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010 });
            Assert.IsNull(dlisStream.ReadIDENT());
        }

        [Test]
        public void DlisStreamHelpers_ReadIDENT_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010, 0b_0100_0011 });
            string expected = "ABC";
            Assert.AreEqual(expected, dlisStream.ReadIDENT());
        }

        [Test]
        public void DlisStreamHelpers_ReadASCII_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadASCII());
        }

        [Test]
        public void DlisStreamHelpers_ReadASCII_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadASCII());
        }

        [Test]
        public void DlisStreamHelpers_ReadASCII_Pass_NoChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(string.Empty, dlisStream.ReadASCII());
        }

        [Test]
        public void DlisStreamHelpers_ReadASCII_Pass_TooFewChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010 });
            Assert.IsNull(dlisStream.ReadASCII());
        }

        [Test]
        public void DlisStreamHelpers_ReadASCII_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010, 0b_0100_0011 });
            string expected = "ABC";
            Assert.AreEqual(expected, dlisStream.ReadASCII());
        }

        [Test]
        public void DlisStreamHelpers_ReadDTIME_Pass_NullStream()
        {
            Assert.AreEqual(DateTime.MinValue, nullStream.ReadDTIME());
        }

        [Test]
        public void DlisStreamHelpers_ReadDTIME_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.AreEqual(DateTime.MinValue, dlisStream.ReadDTIME());
        }

        [Test]
        public void DlisStreamHelpers_ReadDTIME_Pass_ShortStream()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(DateTime.MinValue, dlisStream.ReadDTIME());
        }

        [Test]
        public void DlisStreamHelpers_ReadDTIME_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0101_0111, 0b_0001_0100, 0b_0001_0011, 0b_0001_0101, 0b_0001_0100, 0b_0000_1111, 0b_0000_0010, 0b_0110_1100 });
            DateTime expected = new DateTime(1987, 4, 19, 21, 20, 15, 620, DateTimeKind.Local);
            Assert.AreEqual(expected, dlisStream.ReadDTIME());
        }

        [Test]
        public void DlisStreamHelpers_ReadOBNAME_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadOBNAME());
        }

        [Test]
        public void DlisStreamHelpers_ReadOBNAME_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadOBNAME());
        }

        [Test]
        public void DlisStreamHelpers_ReadOBNAME_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0001, 0b_0000_0010, 0b_0000_0001, 0b_0100_0001 });
            var obName = dlisStream.ReadOBNAME();
            Assert.AreEqual(1, obName.Origin);
            Assert.AreEqual(2, obName.CopyNumber);
            Assert.AreEqual("A", obName.Identifier);
        }

        [Test]
        public void DlisStreamHelpers_ReadOBJREF_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadOBJREF());
        }

        [Test]
        public void DlisStreamHelpers_ReadOBJREF_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadOBJREF());
        }

        [Test]
        public void DlisStreamHelpers_ReadOBJREF_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0001, 0b_0100_0001, 0b_0000_0001, 0b_0000_0010, 0b_0000_0001, 0b_0100_0010 });

            var objRef = dlisStream.ReadOBJREF();
            Assert.AreEqual("A", objRef.ObjectType);
            Assert.AreEqual(1, objRef.Name.Origin);
            Assert.AreEqual(2, objRef.Name.CopyNumber);
            Assert.AreEqual("B", objRef.Name.Identifier);
        }

        [Test]
        public void DlisStreamHelpers_ReadATTREF_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadATTREF());
        }

        [Test]
        public void DlisStreamHelpers_ReadATTREF_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsNull(dlisStream.ReadATTREF());
        }

        [Test]
        public void DlisStreamHelpers_ReadATTREF_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0001, 0b_0100_0001, 0b_0000_0001, 0b_0000_0010, 0b_0000_0001, 0b_0100_0010, 0b_0000_0001, 0b_0100_0011 });

            var attRef = dlisStream.ReadATTREF();
            Assert.AreEqual("A", attRef.ObjectType);
            Assert.AreEqual(1, attRef.Name.Origin);
            Assert.AreEqual(2, attRef.Name.CopyNumber);
            Assert.AreEqual("B", attRef.Name.Identifier);
            Assert.AreEqual("C", attRef.Label);
        }

        [Test]
        public void DlisStreamHelpers_ReadSTATUS_Pass_NullStream()
        {
            Assert.IsFalse(nullStream.ReadSTATUS());
        }

        [Test]
        public void DlisStreamHelpers_ReadSTATUS_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream();
            Assert.IsFalse(dlisStream.ReadSTATUS());
        }

        [Test]
        public void DlisStreamHelpers_ReadSTATUS_Pass_True()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0001 });
            Assert.IsTrue(dlisStream.ReadSTATUS());
        }

        [Test]
        public void DlisStreamHelpers_ReadSTATUS_Pass_False()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.IsFalse(dlisStream.ReadSTATUS());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNITS_Pass_NullStream()
        {
            Assert.IsNull(nullStream.ReadUNITS());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNITS_Pass_EmptyStream()
        {
            var dlisStream = new MemoryStream(new byte[0]);
            Assert.IsNull(dlisStream.ReadUNITS());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNITS_Pass_NoChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0000 });
            Assert.AreEqual(string.Empty, dlisStream.ReadUNITS());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNITS_Pass_TooFewChars()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010 });
            Assert.IsNull(dlisStream.ReadUNITS());
        }

        [Test]
        public void DlisStreamHelpers_ReadUNITS_Pass()
        {
            var dlisStream = new MemoryStream(new byte[] { 0b_0000_0011, 0b_0100_0001, 0b_0100_0010, 0b_0100_0011 });
            string expected = "ABC";
            Assert.AreEqual(expected, dlisStream.ReadUNITS());
        }
    }
}
