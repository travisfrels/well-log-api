using System;

namespace WellLog.Lib.Helpers
{
    public static class ByteHelpers
    {
        public static bool GetBit(this byte b, byte mask)
        {
            return (b & mask) > 0;
        }

        public static byte SetBit(this byte b, byte mask, bool value)
        {
            if (value) { return Convert.ToByte(b | mask); }
            return Convert.ToByte(b & ~mask);
        }

        public static bool IsComponentRole(this byte b, byte role)
        {
            return (b >> 5) == (role >> 5);
        }
    }
}
