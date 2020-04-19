using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IATTREFReader
    {
        ATTREF ReadATTREF(Stream s);
        IEnumerable ReadValues(Stream s, uint count);
    }
}