using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class UVARIReader : IValueReader, IUVARIReader
    {
        public uint ReadUVARI1(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return 0; }
            return Convert.ToUInt32(s.ReadBytes(1).ConvertToByte());
        }

        public uint ReadUVARI2(Stream s)
        {
            if (s == null || s.BytesRemaining() < 2) { return 0; }

            var buffer = s.ReadBytes(2);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].ClearBitUsingMask(0b_1000_0000);
            return Convert.ToUInt32(buffer.ConvertToUshort(false));
        }

        public uint ReadUVARI4(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return 0; }

            var buffer = s.ReadBytes(4);
            if (buffer == null) { return 0; }

            buffer[0] = buffer[0].ClearBitUsingMask(0b_1100_0000);
            return buffer.ConvertToUInt(false);
        }

        public uint ReadUVARI(Stream s)
        {
            if (s == null || s.BytesRemaining() < 1) { return 0; }

            var buffer = s.ReadBytes(1);
            if (buffer == null) { return 0; }

            s.Seek(-1, SeekOrigin.Current);
            if (!buffer[0].GetBitUsingMask(0b_1000_0000)) { return ReadUVARI1(s); }
            if (!buffer[0].GetBitUsingMask(0b_0100_0000)) { return ReadUVARI2(s); }
            return ReadUVARI4(s);
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < count) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadUVARI(s); }
        }
    }
}
