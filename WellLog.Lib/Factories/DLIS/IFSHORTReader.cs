using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFSHORTReader
    {
        float ConvertToFloat(ushort fshortData);
        float ReadFSHORT(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}