using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSING1Reader : IValueReader, IFSING1Reader
    {
        private readonly IFSINGLReader _fsinglReader;

        public FSING1Reader(IFSINGLReader fsinglReader)
        {
            _fsinglReader = fsinglReader;
        }

        public FSING1 ReadFSING1(Stream s)
        {
            if (s == null || s.BytesRemaining() < 8) { return null; }
            return new FSING1
            {
                V = _fsinglReader.ReadFSINGL(s),
                A = _fsinglReader.ReadFSINGL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 8)) { return null; }

            var values = new FSING1[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFSING1(s); }
            return values;
        }
    }
}
