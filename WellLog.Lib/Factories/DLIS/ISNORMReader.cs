using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface ISNORMReader
    {
        short ReadSNORM(Stream dlisStream);
        IEnumerable ReadValues(Stream s, uint count);
    }
}