using System.Collections;
using System.IO;
using System.Text;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class UNITSReader : IValueReader, IUNITSReader
    {
        public string ReadUNITS(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return null; }

            var length = s.ReadByte();
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = s.ReadBytes(length);
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 1)) { return null; }

            var values = new string[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadUNITS(s); }
            return values;
        }
    }
}
