using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFDOUBLReader
    {
        double ReadFDOUBL(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}