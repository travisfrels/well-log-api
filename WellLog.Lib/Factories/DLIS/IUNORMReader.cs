using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IUNORMReader
    {
        ushort ReadUNORM(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}