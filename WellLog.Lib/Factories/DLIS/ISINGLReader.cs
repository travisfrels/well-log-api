using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class ISINGLReader : IValueReader
    {
        public static int IBM_EXP_MASK = 0x7F000000;
        public static int IBM_EXP_SIZE = 7;
        public static int IBM_FRAC_MASK = 0x00FFFFFF;
        public static int IBM_FRAC_SIZE = 24;

        public float ConvertToFloat(uint ibmData)
        {
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

        public float ReadISINGL(Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 4) { return 0f; }

            var buffer = dlisStream.ReadBytes(4);
            if (buffer == null) { return 0f; }

            return ConvertToFloat(buffer.ConvertToUInt(false));
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadISINGL(s); }
        }
    }
}
