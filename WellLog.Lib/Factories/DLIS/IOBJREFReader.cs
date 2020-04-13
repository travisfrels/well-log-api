using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IOBJREFReader
    {
        OBJREF ReadOBJREF(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}