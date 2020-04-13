using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSINGLReader : IValueReader, IFSINGLReader
    {
        public float ReadFSINGL(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return 0f; }
            return s.ReadBytes(4).ConvertToFloat(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { return null; }

            var values = new float[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFSINGL(s); }
            return values;
        }
    }
}
