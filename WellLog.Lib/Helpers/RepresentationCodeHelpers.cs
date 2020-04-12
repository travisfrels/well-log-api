using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class RepresentationCodeHelpers
    {
        public static ushort FSHORT_EXP_MASK = 0x000F;
        public static int FSHORT_EXP_SIZE = 4;
        public static ushort FSHORT_FRAC_MASK = 0xFFF0;
        public static int FSHORT_FRAC_SIZE = 11;
        public static uint FSHORT_SIGN_MASK = 0xFFFFF000;

        public static float ReadFSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 2) { return 0f; }

            var buffer = dlisStream.ReadBytes(2);
            if (buffer == null) { return 0f; }

            var fshortData = buffer.ConvertToUshort(false);

            /* MMMMMMMMMMMM | EEEE */
            /* M: 2s complement 12 bit signed integer fraction */
            /* E: 4 bit unsigned integer */
            /* V = M * (2 ^ E) */
            var sign = fshortData >> FSHORT_EXP_SIZE + FSHORT_FRAC_SIZE;
            var exponent = fshortData & FSHORT_EXP_MASK;
            var fractional = (fshortData & FSHORT_FRAC_MASK) >> FSHORT_EXP_SIZE;

            if (fractional == 0) { return 0f; }

            /*
             * shifting right, to get the fraction, shifted in zeros.  the sign bit will tell us if
             * the fraction was supposed to be a negative number.  if the fraction is supposed to
             * be negative, then flip all of the leading zeros to ones.
             */
            if (sign == 1) { fractional |= (int)FSHORT_SIGN_MASK; }
            var mantissa = Convert.ToDouble(fractional) / (1 << FSHORT_FRAC_SIZE);

            /* (1 << exponent) == (2 ^ exponent) */
            return Convert.ToSingle(mantissa * (1 << exponent));
        }

        public static IEnumerable<float> ReadFSHORT(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 2)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFSHORT(); }
        }

        public static float ReadFSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0f; }
            return dlisStream.ReadBytes(4).ConvertToFloat(false);
        }

        public static IEnumerable<float> ReadFSINGL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFSINGL(); }
        }

        public static FSING1 ReadFSING1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 8) { return null; }
            return new FSING1
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL()
            };
        }

        public static IEnumerable<FSING1> ReadFSING1(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 8)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFSING1(); }
        }

        public static FSING2 ReadFSING2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 12) { return null; }
            return new FSING2
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL(),
                B = dlisStream.ReadFSINGL()
            };
        }

        public static IEnumerable<FSING2> ReadFSING2(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 12)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFSING2(); }
        }

        public static int IBM_EXP_MASK = 0x7F000000;
        public static int IBM_EXP_SIZE = 7;
        public static int IBM_FRAC_MASK = 0x00FFFFFF;
        public static int IBM_FRAC_SIZE = 24;

        public static float ReadISINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            var ibmData = buffer.ConvertToUInt(false);

            /* S | EEEEEEE | MMMMMMMMMMMMMMMMMMMMMMMM */
            /* S: sign bit */
            /* E: 7 bit unsigned integer */
            /* M: 24 bit unsigned integer fraction */
            /* V = (-1 ^ S) * M * (16 ^ (E - 64)) */
            var sign = ibmData >> (IBM_EXP_SIZE + IBM_FRAC_SIZE);
            var exponent = (ibmData & IBM_EXP_MASK) >> IBM_FRAC_SIZE;
            var fractional = ibmData & IBM_FRAC_MASK;

            if (fractional == 0) { return 0f; }

            var mantissa = Convert.ToDouble(fractional) / (1 << IBM_FRAC_SIZE);
            return (sign == 0 ? 1f : -1f) * Convert.ToSingle(mantissa * Math.Pow(16, exponent - 64));
        }

        public static IEnumerable<float> ReadISINGL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadISINGL(); }
        }

        public static int VAX_EXP_MASK = 0x7F800000;
        public static int VAX_EXP_SIZE = 8;
        public static int VAX_FRAC_MASK = 0x007FFFFF;
        public static int VAX_FRAC_SIZE = 23;

        public static float ReadVSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            var vaxData = (new byte[] { buffer[1], buffer[0], buffer[3], buffer[2] }).ConvertToUInt(false);

            /* S | EEEEEEEE | MMMMMMMMMMMMMMMMMMMMMMM */
            /* S: sign bit */
            /* E: 8 bit unsigned integer */
            /* M: 23 bit unsigned integer fraction */
            /* V = (-1 ^ S) * (0.5 + M) * (2 ^ (E - 128)) */
            var sign = vaxData >> (VAX_EXP_SIZE + VAX_FRAC_SIZE);
            var exponent = (vaxData & VAX_EXP_MASK) >> VAX_FRAC_SIZE;
            var fractional = vaxData & VAX_FRAC_MASK;

            if (sign == 0 && exponent == 0) { return 0f; }

            var mantissa = Convert.ToDouble(fractional) / (2 << VAX_FRAC_SIZE);
            return (sign == 0 ? 1f : -1f) * Convert.ToSingle((0.5d + mantissa) * Math.Pow(2, exponent - 128));
        }

        public static IEnumerable<float> ReadVSINGL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadVSINGL(); }
        }

        public static double ReadFDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 8) { return 0d; }
            return dlisStream.ReadBytes(8).ConvertToDouble(false);
        }

        public static IEnumerable<double> ReadFDOUBL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 8)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFDOUBL(); }
        }

        public static FDOUB1 ReadFDOUB1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 16) { return null; }
            return new FDOUB1
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL()
            };
        }

        public static IEnumerable<FDOUB1> ReadFDOUB1(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 16)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFDOUB1(); }
        }

        public static FDOUB2 ReadFDOUB2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 24) { return null; }
            return new FDOUB2
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL(),
                B = dlisStream.ReadFDOUBL()
            };
        }

        public static IEnumerable<FDOUB2> ReadFDOUB2(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 24)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadFDOUB2(); }
        }

        public static CSINGL ReadCSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 8) { return null; }
            return new CSINGL
            {
                R = dlisStream.ReadFSINGL(),
                I = dlisStream.ReadFSINGL()
            };
        }

        public static IEnumerable<CSINGL> ReadCSINGL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 8)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadCSINGL(); }
        }

        public static CDOUBL ReadCDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 16) { return null; }
            return new CDOUBL
            {
                R = dlisStream.ReadFDOUBL(),
                I = dlisStream.ReadFDOUBL()
            };
        }

        public static IEnumerable<CDOUBL> ReadCDOUBL(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 16)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadCDOUBL(); }
        }

        public static sbyte ReadSSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return 0; }
            return dlisStream.ReadBytes(1).ConvertToSbyte();
        }

        public static IEnumerable<sbyte> ReadSSHORT(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadSSHORT(); }
        }

        public static short ReadSNORM(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 2) { return 0; }
            return dlisStream.ReadBytes(2).ConvertToShort(false);
        }

        public static IEnumerable<short> ReadSNORM(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 2)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadSNORM(); }
        }

        public static int ReadSLONG(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0; }
            return dlisStream.ReadBytes(4).ConvertToInt(false);
        }

        public static IEnumerable<int> ReadSLONG(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadSLONG(); }
        }

        public static byte ReadUSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return 0; }
            return dlisStream.ReadBytes(1).ConvertToByte();
        }

        public static IEnumerable<byte> ReadUSHORT(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadUSHORT(); }
        }

        public static ushort ReadUNORM(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 2) { return 0; }
            return dlisStream.ReadBytes(2).ConvertToUshort(false);
        }

        public static IEnumerable<ushort> ReadUNORM(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 2)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadUNORM(); }
        }

        public static uint ReadULONG(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0; }
            return dlisStream.ReadBytes(4).ConvertToUInt(false);
        }

        public static IEnumerable<uint> ReadULONG(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadULONG(); }
        }

        public static uint ReadUVARI1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return 0; }
            return Convert.ToUInt32(dlisStream.ReadBytes(1).ConvertToByte());
        }

        public static uint ReadUVARI2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 2) { return 0; }

            var buffer = dlisStream.ReadBytes(2);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].ClearBitUsingMask(0b_1000_0000);
            return Convert.ToUInt32(buffer.ConvertToUshort(false));
        }

        public static uint ReadUVARI4(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].ClearBitUsingMask(0b_1100_0000);
            return buffer.ConvertToUInt(false);
        }

        public static uint ReadUVARI(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return 0; }

            var buffer = dlisStream.ReadBytes(1);
            if (buffer == null) { return 0; }

            dlisStream.Seek(-1, SeekOrigin.Current);
            if (!buffer[0].GetBitUsingMask(0b_1000_0000)) { return dlisStream.ReadUVARI1(); }
            if (!buffer[0].GetBitUsingMask(0b_0100_0000)) { return dlisStream.ReadUVARI2(); }
            return dlisStream.ReadUVARI4();
        }

        public static IEnumerable<uint> ReadUVARI(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadUVARI(); }
        }

        public static string ReadIDENT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return null; }

            var length = dlisStream.ReadByte();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(length);
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public static IEnumerable<string> ReadIDENT(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadIDENT(); }
        }

        public static string ReadASCII(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return null; }

            var length = dlisStream.ReadUVARI();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(Convert.ToInt32(length));
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public static IEnumerable<string> ReadASCII(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadASCII(); }
        }

        public static DateTimeKind ToDateTimeKind(this byte b)
        {
            if (b == 1 || b == 2) { return DateTimeKind.Local; }
            return DateTimeKind.Utc;
        }

        public static DateTime ReadDTIME(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 8) { return DateTime.MinValue; }

            var buffer = dlisStream.ReadBytes(8);
            if (buffer == null) { return DateTime.MinValue; }

            var year = 1900 + buffer[0];
            var tz = buffer[1].ClearBitUsingMask(0b_0000_1111).ShiftRight(4);
            var month = buffer[1].ClearBitUsingMask(0b_1111_0000);
            var day = buffer[2];
            var hour = buffer[3];
            var minute = buffer[4];
            var second = buffer[5];
            var millisecond = buffer[6..8].ConvertToUshort(false);

            return new DateTime(year, month, day, hour, minute, second, millisecond, tz.ToDateTimeKind());
        }

        public static IEnumerable<DateTime> ReadDTIME(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 8)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadDTIME(); }
        }

        public static OBNAME ReadOBNAME(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 3) { return null; }
            return new OBNAME
            {
                Origin = dlisStream.ReadUVARI(),
                CopyNumber = dlisStream.ReadUSHORT(),
                Identifier = dlisStream.ReadIDENT()
            };
        }

        public static IEnumerable<OBNAME> ReadOBNAME(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 3)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadOBNAME(); }
        }

        public static OBJREF ReadOBJREF(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return null; }
            return new OBJREF
            {
                ObjectType = dlisStream.ReadIDENT(),
                Name = dlisStream.ReadOBNAME()
            };
        }

        public static IEnumerable<OBJREF> ReadOBJREF(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadOBJREF(); }
        }

        public static ATTREF ReadATTREF(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 5) { return null; }
            return new ATTREF
            {
                ObjectType = dlisStream.ReadIDENT(),
                Name = dlisStream.ReadOBNAME(),
                Label = dlisStream.ReadIDENT()
            };
        }

        public static IEnumerable<ATTREF> ReadATTREF(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 5)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadATTREF(); }
        }

        public static bool ReadSTATUS(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return false; }
            return dlisStream.ReadUSHORT() != 0;
        }

        public static IEnumerable<bool> ReadSTATUS(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadSTATUS(); }
        }

        public static string ReadUNITS(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 1) { return null; }

            var length = dlisStream.ReadByte();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(length);
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public static IEnumerable<string> ReadUNITS(this Stream dlisStream, uint count)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return dlisStream.ReadUNITS(); }
        }
    }
}
