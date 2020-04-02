using System;
using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class DlisStreamHelpers
    {
        public static byte[] ReadBytes(this Stream dlisStream, int numBytes)
        {
            if (dlisStream == null) { return null; }

            var buffer = new byte[numBytes];
            var bytesRead = dlisStream.Read(buffer, 0, numBytes);

            if (bytesRead < numBytes) { return null; }
            return buffer;
        }

        public static float ReadFSHORT(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0f; }

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
            if (dlisStream == null) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return BitConverter.ToSingle(buffer);
        }

        public static FSING1 ReadFSING1(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new FSING1
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL()
            };
        }

        public static FSING2 ReadFSING2(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new FSING2
            {
                V = dlisStream.ReadFSINGL(),
                A = dlisStream.ReadFSINGL(),
                B = dlisStream.ReadFSINGL()
            };
        }

        public static float ReadISINGL(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0f; }

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
            if (dlisStream == null) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return BitConverter.ToSingle(new byte[4] { buffer[1], buffer[0], buffer[2], buffer[3] });
        }

        public static double ReadFDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0d; }

            var buffer = dlisStream.ReadBytes(8);
            if (buffer == null) { return 0d; }

            return BitConverter.ToDouble(buffer);
        }

        public static FDOUB1 ReadFDOUB1(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new FDOUB1
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL()
            };
        }

        public static FDOUB2 ReadFDOUB2(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new FDOUB2
            {
                V = dlisStream.ReadFDOUBL(),
                A = dlisStream.ReadFDOUBL(),
                B = dlisStream.ReadFDOUBL()
            };
        }

        public static CSINGL ReadCSINGL(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new CSINGL
            {
                R = dlisStream.ReadFSINGL(),
                I = dlisStream.ReadFSINGL()
            };
        }

        public static CDOUBL ReadCDOUBL(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new CDOUBL
            {
                R = dlisStream.ReadFDOUBL(),
                I = dlisStream.ReadFDOUBL()
            };
        }

        public static sbyte ReadSSHORT(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return Convert.ToSByte(dlisStream.ReadByte());
        }

        public static short ReadSNORM(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return BitConverter.ToInt16(dlisStream.ReadBytes(2));
        }

        public static int ReadSLONG(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return BitConverter.ToInt32(dlisStream.ReadBytes(4));
        }

        public static byte ReadUSHORT(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return Convert.ToByte(dlisStream.ReadByte());
        }

        public static ushort ReadUNORM(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return BitConverter.ToUInt16(dlisStream.ReadBytes(2));
        }

        public static uint ReadULONG(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }
            return BitConverter.ToUInt32(dlisStream.ReadBytes(4));
        }

        public static uint ReadUVARI(this Stream dlisStream)
        {
            if (dlisStream == null) { return 0; }

            var b = Convert.ToByte(dlisStream.ReadByte());
            if (b >> 7 == 0) { return Convert.ToUInt32(b); }
            if (b >> 6 == 2) { return BitConverter.ToUInt32(new byte[2] { b.ClearBitUsingMask(0b_1000_0000), Convert.ToByte(dlisStream.ReadByte()) }); }
            if (b >> 6 == 3)
            {
                var buffer = dlisStream.ReadBytes(3);
                return BitConverter.ToUInt32(new byte[4] { b.ClearBitUsingMask(0b_1100_0000), buffer[0], buffer[1], buffer[2] });
            }

            return BitConverter.ToUInt32(dlisStream.ReadBytes(4));
        }

        public static string ReadIDENT(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var length = dlisStream.ReadByte();
            if (length < 0) { return null; }

            return Encoding.ASCII.GetString(dlisStream.ReadBytes(length));
        }

        public static string ReadASCII(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var length = Convert.ToInt32(dlisStream.ReadUVARI());
            if (length < 0) { return null; }

            return Encoding.ASCII.GetString(dlisStream.ReadBytes(length));
        }

        public static DateTime ReadDTIME(this Stream dlisStream)
        {
            if (dlisStream == null) { return DateTime.MinValue; }

            var buffer = dlisStream.ReadBytes(8);
            var year = 1900 + buffer[0];
            var month = buffer[1].ClearBitUsingMask(0b_1111_0000);
            var day = buffer[2];
            var hour = buffer[3];
            var minute = buffer[4];
            var second = buffer[5];
            var millisecond = BitConverter.ToUInt16(new byte[2] { buffer[6], buffer[7] });

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public static OBNAME ReadOBNAME(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new OBNAME
            {
                O = dlisStream.ReadUVARI(),
                C = dlisStream.ReadUSHORT(),
                I = dlisStream.ReadIDENT()
            };
        }

        public static OBJREF ReadOBJREF(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new OBJREF
            {
                T = dlisStream.ReadIDENT(),
                N = dlisStream.ReadOBNAME()
            };
        }

        public static ATTREF ReadATTREF(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            return new ATTREF
            {
                T = dlisStream.ReadIDENT(),
                N = dlisStream.ReadOBNAME(),
                L = dlisStream.ReadIDENT()
            };
        }

        public static bool ReadSTATUS(this Stream dlisStream)
        {
            if (dlisStream == null) { return false; }
            return dlisStream.ReadUSHORT() != 0;
        }

        public static string ReadUNITS(this Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var length = Convert.ToInt32(dlisStream.ReadUSHORT());
            if (length < 0) { return null; }

            return Encoding.ASCII.GetString(dlisStream.ReadBytes(length));
        }
    }
}
