using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class VSINGLReader : IValueReader, IVSINGLReader
    {
        public static int VAX_EXP_MASK = 0x7F800000;
        public static int VAX_EXP_SIZE = 8;
        public static int VAX_FRAC_MASK = 0x007FFFFF;
        public static int VAX_FRAC_SIZE = 23;

        public float ConvertToFloat(uint vaxData)
        {
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

        public float ReadVSINGL(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return 0f; }

            var buffer = s.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return ConvertToFloat((new byte[] { buffer[1], buffer[0], buffer[3], buffer[2] }).ConvertToUInt(false));
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadVSINGL(s); }
        }
    }
}
