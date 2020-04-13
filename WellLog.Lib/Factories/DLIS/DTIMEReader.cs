using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class DTIMEReader : IValueReader, IDTIMEReader
    {
        public DateTimeKind ToDateTimeKind(byte b)
        {
            if (b == 1 || b == 2) { return DateTimeKind.Local; }
            return DateTimeKind.Utc;
        }

        public DateTime ReadDTIME(Stream s)
        {
            if (s == null || s.BytesRemaining() < 8) { return DateTime.MinValue; }

            var buffer = s.ReadBytes(8);
            if (buffer == null) { return DateTime.MinValue; }

            var year = 1900 + buffer[0];
            var tz = buffer[1].ClearBitUsingMask(0b_0000_1111).ShiftRight(4);
            var month = buffer[1].ClearBitUsingMask(0b_1111_0000);
            var day = buffer[2];
            var hour = buffer[3];
            var minute = buffer[4];
            var second = buffer[5];
            var millisecond = buffer[6..8].ConvertToUshort(false);

            return new DateTime(year, month, day, hour, minute, second, millisecond, ToDateTimeKind(tz));
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 8)) { return null; }

            var values = new DateTime[count];
            for (uint i = 0; i < count; i++) { values[i] = ReadDTIME(s); }
            return values;
        }
    }
}
