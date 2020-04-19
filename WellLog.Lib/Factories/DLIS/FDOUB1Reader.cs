using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class FDOUB1Reader : IValueReader, IFDOUB1Reader
    {
        private readonly IFDOUBLReader _fdoublReader;

        public FDOUB1Reader(IFDOUBLReader fdoublReader)
        {
            _fdoublReader = fdoublReader;
        }

        public FDOUB1 ReadFDOUB1(Stream s)
        {
            if (s == null || s.BytesRemaining() < 16) { return null; }
            return new FDOUB1
            {
                V = _fdoublReader.ReadFDOUBL(s),
                A = _fdoublReader.ReadFDOUBL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 16)) { return null; }

            var values = new FDOUB1[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFDOUB1(s); }
            return values;
        }
    }
}
