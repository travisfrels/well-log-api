using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.Factories.DLIS
{
    public class IDENTReader : IValueReader
    {
        public IEnumerable ReadValues(Stream s, uint count)
        {
            foreach (var v in s.ReadIDENT(count)) { yield return v; }
        }
    }
}
