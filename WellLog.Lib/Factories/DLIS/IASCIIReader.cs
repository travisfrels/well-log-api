using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IASCIIReader
    {
        string ReadASCII(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}