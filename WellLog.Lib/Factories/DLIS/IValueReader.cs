using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IValueReader
    {
        IEnumerable ReadValues(Stream s, uint count);
    }
}
