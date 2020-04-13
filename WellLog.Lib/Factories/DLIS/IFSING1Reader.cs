using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFSING1Reader
    {
        FSING1 ReadFSING1(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}