using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class UNORMReader : IValueReader, IUNORMReader
    {
        public ushort ReadUNORM(Stream s)
        {
            if (s == null || s.BytesRemaining() < 2) { return 0; }
            return s.ReadBytes(2).ConvertToUshort(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 2)) { return null; }

            var values = new ushort[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadUNORM(s); }
            return values;
        }
    }
}
