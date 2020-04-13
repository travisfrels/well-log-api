using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class SSHORTReader : IValueReader, ISSHORTReader
    {
        public sbyte ReadSSHORT(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return 0; }
            return s.ReadBytes(1).ConvertToSbyte();
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 1)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadSSHORT(s); }
        }
    }
}
