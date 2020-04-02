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

        public static bool IsComponentRole(this byte b, byte role)
        {
            return (b >> 5) == (role >> 5);
        }

        public static byte[] ShiftLeft(this byte[] bytes)
        {
            if (bytes == null) { return null; }

            var buffer = new byte[bytes.Length];

            var carryFlag = false;
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                var b = bytes[i];
                buffer[i] = Convert.ToByte(b << 1).AssignBitUsingMask(0b_0000_0001, carryFlag);
                carryFlag = b.GetBitUsingMask(0b_1000_0000);
            }

            return buffer;
        }

        public static byte[] ShiftRight(this byte[] bytes)
        {
            if (bytes == null) { return null; }

            var buffer = new byte[bytes.Length];

            var carryFlag = false;
            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                buffer[i] = Convert.ToByte(b >> 1).AssignBitUsingMask(0b_1000_0000, carryFlag);
                carryFlag = b.GetBitUsingMask(0b_0000_0001);
            }

            return buffer;
        }
    }
}
