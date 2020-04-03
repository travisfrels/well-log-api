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

            var floatBuffer = new byte[4]
            {
                buffer[1].ClearBitUsingMask(0b_1111_0000),
                buffer[0],
                buffer[1].ClearBitUsingMask(0b_0000_1111),
                0
            };
            return BitConverter.ToSingle(floatBuffer.ShiftRight());
        }

        public static float ReadFSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return BitConverter.ToSingle(buffer);
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

        public static float ReadISINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            var sign = buffer[0].GetBitUsingMask(0b_1000_0000);
            buffer[0] = buffer[0].ClearBitUsingMask(0b_1000_0000);
            buffer = buffer.ShiftRight();
            buffer[0] = buffer[0].AssignBitUsingMask(0b_1000_0000, sign);

            return BitConverter.ToSingle(buffer);
        }

        public static float ReadVSINGL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return BitConverter.ToSingle(new byte[4] { buffer[1], buffer[0], buffer[2], buffer[3] });
        }

        public static double ReadFDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0d; }

            var buffer = dlisStream.ReadBytes(8);
            if (buffer == null) { return 0d; }

            return BitConverter.ToDouble(buffer);
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
