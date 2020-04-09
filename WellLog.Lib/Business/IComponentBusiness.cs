using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IComponentBusiness
    {
        IEnumerable<Component> ReadFileHeaderLogicalRecord(Stream dlisStream);
        Component ReadComponent(Stream dlisStream);
    }
}