using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IVSINGLReader
    {
        float ConvertToFloat(uint vaxData);
        IEnumerable ReadValues(Stream s, uint count);
        float ReadVSINGL(Stream s);
    }
}