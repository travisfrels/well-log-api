using System;
using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IDTIMEReader
    {
        DateTimeKind ToDateTimeKind(byte b);
        DateTime ReadDTIME(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}