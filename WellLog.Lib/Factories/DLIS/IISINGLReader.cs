using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IISINGLReader
    {
        float ConvertToFloat(uint ibmData);
        float ReadISINGL(Stream dlisStream);
        IEnumerable ReadValues(Stream s, uint count);
    }
}