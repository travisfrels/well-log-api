using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IUSHORTReader
    {
        byte ReadUSHORT(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}