﻿using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class FDOUBLReader : IValueReader, IFDOUBLReader
    {
        public double ReadFDOUBL(Stream s)
        {
            if (s == null || s.BytesRemaining() < 8) { return 0d; }
            return s.ReadBytes(8).ConvertToDouble(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 8)) { return null; }

            var values = new double[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadFDOUBL(s); }
            return values;
        }
    }
}
