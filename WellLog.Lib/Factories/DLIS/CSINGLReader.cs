using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class CSINGLReader : IValueReader, ICSINGLReader
    {
        private readonly IFSINGLReader _fsinglReader;

        public CSINGLReader(IFSINGLReader fsinglReader)
        {
            _fsinglReader = fsinglReader;
        }

        public CSINGL ReadCSINGL(Stream s)
        {
            if (s == null || s.BytesRemaining() < 8) { return null; }
            return new CSINGL
            {
                R = _fsinglReader.ReadFSINGL(s),
                I = _fsinglReader.ReadFSINGL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 8)) { return null; }

            var values = new CSINGL[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadCSINGL(s); }
            return values;
        }
    }
}
