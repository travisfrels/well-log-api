﻿using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class SNORMReader : IValueReader, ISNORMReader
    {
        public short ReadSNORM(Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.BytesRemaining() < 2) { return 0; }
            return dlisStream.ReadBytes(2).ConvertToShort(false);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 2)) { return null; }

            var values = new short[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadSNORM(s); }
            return values;
        }
    }
}
