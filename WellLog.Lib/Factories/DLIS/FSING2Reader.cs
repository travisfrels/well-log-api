﻿using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSING2Reader : IValueReader, IFSING2Reader
    {
        private readonly IFSINGLReader _fsinglReader;

        public FSING2Reader(IFSINGLReader fsinglReader)
        {
            _fsinglReader = fsinglReader;
        }

        public FSING2 ReadFSING2(Stream s)
        {
            if (s == null || s.BytesRemaining() < 12) { return null; }
            return new FSING2
            {
                V = _fsinglReader.ReadFSINGL(s),
                A = _fsinglReader.ReadFSINGL(s),
                B = _fsinglReader.ReadFSINGL(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 12)) { return null; }

            var values = new FSING2[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFSING2(s); }
            return values;
        }
    }
}
