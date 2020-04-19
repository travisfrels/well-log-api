using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IIDENTReader
    {
        string ReadIDENT(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}