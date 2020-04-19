using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class FDOUB2Reader : IValueReader, IFDOUB2Reader
    {
        private readonly IFDOUBLReader _fdoublReader;

        public FDOUB2Reader(IFDOUBLReader fdoublReader)
        {
            _fdoublReader = fdoublReader;
        }

        public FDOUB2 ReadFDOUB2(Stream s)
        {
            if (s == null || s.BytesRemaining() < 24) { return null; }
            return new FDOUB2
            {
                V = _fdoublReader.ReadFDOUBL(s),
                A = _fdoublReader.ReadFDOUBL(s),
                B = _fdoublReader.ReadFDOUBL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 24)) { return null; }

            var values = new FDOUB2[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFDOUB2(s); }
            return values;
        }
    }
}
