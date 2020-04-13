using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IUNITSReader
    {
        string ReadUNITS(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}