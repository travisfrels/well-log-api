using System.Collections;
using System.IO;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IUVARIReader
    {
        uint ReadUVARI(Stream s);
        uint ReadUVARI1(Stream s);
        uint ReadUVARI2(Stream s);
        uint ReadUVARI4(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}