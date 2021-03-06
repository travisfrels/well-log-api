﻿using System.Collections;
using System.IO;
using System.Linq;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class STATUSReader : IValueReader, ISTATUSReader
    {
        public bool ReadSTATUS(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return false; }
            return s.ReadByte() > 0;
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 1)) { return null; }

            var values = new bool[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadSTATUS(s); }
            return values;
        }
    }
}
