using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IULONGReader
    {
        uint ReadULONG(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}