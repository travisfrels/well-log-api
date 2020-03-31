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
    }
}
