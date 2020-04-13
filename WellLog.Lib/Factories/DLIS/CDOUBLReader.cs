using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class CDOUBLReader : IValueReader, ICDOUBLReader
    {
        private readonly IFDOUBLReader _fdoublReader;

        public CDOUBLReader(IFDOUBLReader fdoublReader)
        {
            _fdoublReader = fdoublReader;
        }

        public CDOUBL ReadCDOUBL(Stream s)
        {
            if (s == null || s.BytesRemaining() < 16) { return null; }
            return new CDOUBL
            {
                R = _fdoublReader.ReadFDOUBL(s),
                I = _fdoublReader.ReadFDOUBL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 16)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadCDOUBL(s); }
        }
    }
}
