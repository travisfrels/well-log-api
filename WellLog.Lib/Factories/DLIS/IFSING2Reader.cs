using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFSING2Reader
    {
        FSING2 ReadFSING2(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}