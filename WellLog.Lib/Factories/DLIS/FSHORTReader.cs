using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class FSHORTReader : IValueReader
    {
        public IEnumerable ReadValues(Stream s, uint count)
        {
            foreach (var v in s.ReadFSHORT(count)) { yield return v; }
        }
    }
}
