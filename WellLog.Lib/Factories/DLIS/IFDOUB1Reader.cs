using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFDOUB1Reader
    {
        FDOUB1 ReadFDOUB1(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}