using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface ISTATUSReader
    {
        bool ReadSTATUS(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}