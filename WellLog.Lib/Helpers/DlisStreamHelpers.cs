using System;
using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class DlisStreamHelpers
    {
        public static float ReadFSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(2);
            if (buffer == null) { return 0f; }

            var sign = buffer[0].GetBitUsingMask(0b_1000_0000);
            var mantissaBuffer = buffer.ShiftRight(4);
            mantissaBuffer[0] = mantissaBuffer[0].AssignBitUsingMask(0b_1111_0000, sign);

            var mantissa = mantissaBuffer.ConvertToShort(false);
            var exponent = buffer[1].ClearBitUsingMask(0b_1111_0000);
            return Convert.ToSingle(mantissa << exponent) / 2048f;
        }

        public static float ReadFSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }
            return dlisStream.ReadBytes(4).ConvertToFloat(false);
        }

        public static FSING1 ReadFSING1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new FSING1
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL()
            };
        }

        public static FSING2 ReadFSING2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new FSING2
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL(),
                B = dlisStream.ReadFSINGL()
            };
        }

        public static uint IBM_SIGN_MASK = 0x80000000;
        public static int IBM_EXPONENT_MASK = 0x7F000000;
        public static int IBM_EXPONENT_SIZE = 7;
        public static int IBM_MANTISSA_MASK = 0x00FFFFFF;
        public static int IBM_MANTISSA_SIZE = 24;

        public static float ReadISINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            var ibmData = buffer.ConvertToInt(false);
            var sign = ibmData & (int)IBM_SIGN_MASK;
            var exponent = ibmData & IBM_EXPONENT_MASK;
            var mantissa = ibmData & IBM_MANTISSA_MASK;

            exponent >>= 1;
            mantissa >>= 1;

            return BitConverter.GetBytes(sign | exponent | mantissa).ConvertToFloat();
        }

        public static uint VAX_SIGN_MASK = 0x80000000;
        public static int VAX_EXPONENT_MASK = 0x7F800000;
        public static int VAX_MANTISSA_MASK = 0x007FFFFF;
        public static int VAX_MANTISSA_SIZE = 23;
        public static int VAX_EXPONENT_ADJUSTMENT = 2;
        public static int VAX_IN_PLACE_EXPONENT_ADJUSTMENT = 16777216;
        public static int VAX_HIDDEN_BIT = 0x00800000;

        public static float ReadVSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            var vaxData = (new byte[] { buffer[1], buffer[0], buffer[3], buffer[2] }).ConvertToInt(false);

            var exponent = vaxData & VAX_EXPONENT_MASK;
            if (exponent == 0) { return 0f; }
            exponent >>= VAX_MANTISSA_SIZE;

            /* The biased VAX exponent has to be adjusted to account for the right shift of the
             * IEEE mantissa binary point and the difference betweenthe biases in their "excess n"
             * exponent representations.  If the resulting biased IEEE exponent is less than or
             * equal to zero, the converted IEEE S_float must use subnormal form.
             * - Lawrence M. Baker
             * https://pubs.usgs.gov/of/2005/1424/
             */
            exponent -= VAX_EXPONENT_ADJUSTMENT;
            if (exponent > 0)
            {
                return BitConverter.GetBytes(vaxData - VAX_IN_PLACE_EXPONENT_ADJUSTMENT).ConvertToFloat();
            }

            /* In IEEE subnormal form, even though the biased exponent is 0 [e=0], the effective
             * biased exponent is 1.  The mantissa must be shifted right by the number of bits, n,
             * required to adjust the biased exponent from its current value, e, to 1
             * (i.e. e + n = 1, thus n = 1 - e).
             * n is guaranteed to be at least 1 [e<=0], which guarantees that the hidden 1.m bit
             * from the original mantissa will become visible, and the resulting subnormal
             * mantissa will correctly be of the form 0.m.
             * - Lawrence M. Baker
             * https://pubs.usgs.gov/of/2005/1424/
             */
            vaxData = (vaxData & (int)VAX_SIGN_MASK) | ((VAX_HIDDEN_BIT | (vaxData & VAX_MANTISSA_MASK)) >> (1 - exponent));
            return BitConverter.GetBytes(vaxData).ConvertToFloat();
        }

        public static double ReadFDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0d; }
            return dlisStream.ReadBytes(8).ConvertToDouble(false);
        }

        public static FDOUB1 ReadFDOUB1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new FDOUB1
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL()
            };
        }

        public static FDOUB2 ReadFDOUB2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new FDOUB2
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL(),
                B = dlisStream.ReadFDOUBL()
            };
        }

        public static CSINGL ReadCSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new CSINGL
            {
                R = dlisStream.ReadFSINGL(),
                I = dlisStream.ReadFSINGL()
            };
        }

        public static CDOUBL ReadCDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new CDOUBL
            {
                R = dlisStream.ReadFDOUBL(),
                I = dlisStream.ReadFDOUBL()
            };
        }

        public static sbyte ReadSSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(1).ConvertToSbyte();
        }

        public static short ReadSNORM(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(2).ConvertToShort(false);
        }

        public static int ReadSLONG(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(4).ConvertToInt(false);
        }

        public static byte ReadUSHORT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(1).ConvertToByte();
        }

        public static ushort ReadUNORM(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(2).ConvertToUshort(false);
        }

        public static uint ReadULONG(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return dlisStream.ReadBytes(4).ConvertToUInt(false);
        }

        public static uint ReadUVARI1(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }
            return Convert.ToUInt32(dlisStream.ReadBytes(1).ConvertToByte());
        }

        public static uint ReadUVARI2(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }

            var buffer = dlisStream.ReadBytes(2);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].AssignBitUsingMask(0b_1000_0000, false);
            return Convert.ToUInt32(buffer.ConvertToUshort(false));
        }

        public static uint ReadUVARI4(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].AssignBitUsingMask(0b_1100_0000, false);
            return buffer.ConvertToUInt(false);
        }

        public static uint ReadUVARI(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }

            var buffer = dlisStream.ReadBytes(1);
            if (buffer == null) { return 0; }

            dlisStream.Seek(-1, SeekOrigin.Current);
            if (!buffer[0].GetBitUsingMask(0b_1000_0000)) { return dlisStream.ReadUVARI1(); }
            if (!buffer[0].GetBitUsingMask(0b_0100_0000)) { return dlisStream.ReadUVARI2(); }
            return dlisStream.ReadUVARI4();
        }

        public static string ReadIDENT(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var length = dlisStream.ReadByte();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(length);
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public static string ReadASCII(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var length = dlisStream.ReadUVARI();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(Convert.ToInt32(length));
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public static DateTimeKind ToDateTimeKind(this byte b)
        {
            if (b == 1 || b == 2) { return DateTimeKind.Local; }
            return DateTimeKind.Utc;
        }

        public static DateTime ReadDTIME(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return DateTime.MinValue; }

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

        public static OBNAME ReadOBNAME(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new OBNAME
            {
                Origin = dlisStream.ReadUVARI(),
                CopyNumber = dlisStream.ReadUSHORT(),
                Identifier = dlisStream.ReadIDENT()
            };
        }

        public static OBJREF ReadOBJREF(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new OBJREF
            {
                ObjectType = dlisStream.ReadIDENT(),
                Name = dlisStream.ReadOBNAME()
            };
        }

        public static ATTREF ReadATTREF(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            return new ATTREF
            {
                ObjectType = dlisStream.ReadIDENT(),
                Name = dlisStream.ReadOBNAME(),
                Label = dlisStream.ReadIDENT()
            };
        }

        public static bool ReadSTATUS(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return false; }
            return dlisStream.ReadUSHORT() != 0;
        }

        public static string ReadUNITS(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var length = dlisStream.ReadByte();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = dlisStream.ReadBytes(length);
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }
    }
}
