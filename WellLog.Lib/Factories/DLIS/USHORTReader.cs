using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class USHORTReader : IValueReader, IUSHORTReader
    {
        public byte ReadUSHORT(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return 0; }
            return s.ReadBytes(1).ConvertToByte();
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < count) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadUSHORT(s); }
        }
    }
}
