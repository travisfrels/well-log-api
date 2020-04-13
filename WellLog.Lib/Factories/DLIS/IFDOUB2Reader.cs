using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFDOUB2Reader
    {
        FDOUB2 ReadFDOUB2(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}