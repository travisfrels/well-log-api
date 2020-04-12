using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class UNORMReader : IValueReader
    {
        public IEnumerable ReadValues(Stream s, uint count)
        {
            foreach (var v in s.ReadUNORM(count)) { yield return v; }
        }
    }
}
