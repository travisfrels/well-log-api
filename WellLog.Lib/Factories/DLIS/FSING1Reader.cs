using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSING1Reader : IValueReader
    {
        private readonly FSINGLReader _fsinglReader;

        public FSING1Reader(FSINGLReader fsinglReader)
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
            if (s == null || s.BytesRemaining() < (count * 8)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadFSING1(s); }
        }
    }
}
