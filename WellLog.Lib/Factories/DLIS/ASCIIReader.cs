﻿using System;
using System.Collections;
using System.IO;
using System.Text;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class ASCIIReader : IValueReader, IASCIIReader
    {
        private readonly IUVARIReader _uvariReader;

        public ASCIIReader(IUVARIReader uvariReader)
        {
            _uvariReader = uvariReader;
        }

        public string ReadASCII(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return null; }

            var length = _uvariReader.ReadUVARI(s);
            if (length < 0) { return null; }
            if (length == 0) { return string.Empty; }

            var buffer = s.ReadBytes(Convert.ToInt32(length));
            if (buffer == null) { return null; }

            return Encoding.ASCII.GetString(buffer);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < count) { return null; }

            var values = new string[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadASCII(s); }
            return values;
        }
    }
}
