﻿using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class SLONGReader : IValueReader, ISLONGReader
    {
        public int ReadSLONG(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return 0; }
            return s.ReadBytes(4).ConvertToInt(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { return null; }

            var values = new int[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadSLONG(s); }
            return values;
        }
    }
}
