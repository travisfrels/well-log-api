using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSHORTReader : IValueReader
    {
        public static ushort FSHORT_EXP_MASK = 0x000F;
        public static int FSHORT_EXP_SIZE = 4;
        public static ushort FSHORT_FRAC_MASK = 0xFFF0;
        public static int FSHORT_FRAC_SIZE = 11;
        public static uint FSHORT_SIGN_MASK = 0xFFFFF000;

        public float ConvertToFloat(ushort fshortData)
        {
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

        public float ReadFSHORT(Stream s)
        {
            if (s == null || s.BytesRemaining() < 2) { return 0f; }

            var buffer = s.ReadBytes(2);
            if (buffer == null) { return 0f; }

            return ConvertToFloat(buffer.ConvertToUshort(false));
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 2)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadFSHORT(s); }
        }
    }
}
