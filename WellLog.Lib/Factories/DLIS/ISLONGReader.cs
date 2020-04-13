using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface ISLONGReader
    {
        int ReadSLONG(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}