using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class ULONGReader : IValueReader, IULONGReader
    {
        public uint ReadULONG(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return 0; }
            return s.ReadBytes(4).ConvertToUInt(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { return null; }

            var values = new uint[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadULONG(s); }
            return values;
        }
    }
}
