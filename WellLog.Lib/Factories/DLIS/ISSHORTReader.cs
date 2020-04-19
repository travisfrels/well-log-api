using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface ISSHORTReader
    {
        sbyte ReadSSHORT(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}