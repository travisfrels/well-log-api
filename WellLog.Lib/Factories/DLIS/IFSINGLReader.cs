using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IFSINGLReader
    {
        float ReadFSINGL(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}