using System.Collections;
using System.IO;
using System.Linq;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class STATUSReader : IValueReader
    {
        public bool ReadSTATUS(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return false; }
            return s.ReadByte() > 0;
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadSTATUS(s); }
        }
    }
}
