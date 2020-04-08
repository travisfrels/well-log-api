using System;

namespace WellLog.Lib.Helpers
{
    public static class ByteHelpers
    {
        public static bool GetBitUsingMask(this byte b, byte mask)
        {
            return (b & mask) > 0;
        }

        public static byte SetBitUsingMask(this byte b, byte mask)
        {
            return Convert.ToByte(b | mask);
        }

        public static byte ClearBitUsingMask(this byte b, byte mask)
        {
            return Convert.ToByte(b & ~mask);
        }

        public static byte AssignBitUsingMask(this byte b, byte mask, bool value)
        {
            return value ? b.SetBitUsingMask(mask) : b.ClearBitUsingMask(mask);
        }

        public static bool HasDlisComponentRole(this byte b, byte role)
        {
            return (b >> 5) == (role >> 5);
        }

        public static byte ShiftLeft(this byte b, byte p)
        {
            return BitConverter.GetBytes(b << p)[0];
        }

        public static byte ShiftRight(this byte b, byte p)
        {
            return BitConverter.GetBytes(b >> p)[0];
        }

        public static byte[] Reversed(this byte[] buffer)
        {
            if (buffer == null) { return null; }
            if (buffer.Length < 2) { return buffer; }

            var newBuffer = new byte[buffer.Length];
            buffer.CopyTo(newBuffer, 0);
            Array.Reverse(newBuffer);
            return newBuffer;
        }

        public static byte ConvertToByte(this byte[] buffer)
        {
            if (buffer == null) { return 0; }
            if (buffer.Length < 1) { return 0; }
            return buffer[0];
        }

        public static sbyte ConvertToSbyte(this byte[] buffer)
        {
            if (buffer == null) { return 0; }
            if (buffer.Length < 1) { return 0; }
            return (sbyte)buffer[0];
        }

        public static ushort ConvertToUshort(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0; }
            if (buffer.Length < 2) { return 0; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToUInt16(buffer[0..2].Reversed()); }
            return BitConverter.ToUInt16(buffer[0..2]);
        }

        public static short ConvertToShort(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0; }
            if (buffer.Length < 2) { return 0; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToInt16(buffer[0..2].Reversed()); }
            return BitConverter.ToInt16(buffer[0..2]);
        }

        public static uint ConvertToUInt(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0u; }
            if (buffer.Length < 4) { return 0u; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToUInt32(buffer[0..4].Reversed()); }
            return BitConverter.ToUInt16(buffer[0..4]);
        }

        public static int ConvertToInt(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0; }
            if (buffer.Length < 4) { return 0; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToInt32(buffer[0..4].Reversed()); }
            return BitConverter.ToInt32(buffer[0..4]);
        }

        public static float ConvertToFloat(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0f; }
            if (buffer.Length < 4) { return 0f; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToSingle(buffer[0..4].Reversed()); }
            return BitConverter.ToSingle(buffer[0..4]);
        }

        public static double ConvertToDouble(this byte[] buffer, bool isLittleEndian = true)
        {
            if (buffer == null) { return 0d; }
            if (buffer.Length < 8) { return 0d; }
            if (BitConverter.IsLittleEndian != isLittleEndian) { return BitConverter.ToDouble(buffer[0..8].Reversed()); }
            return BitConverter.ToDouble(buffer[0..8]);
        }
    }
}
