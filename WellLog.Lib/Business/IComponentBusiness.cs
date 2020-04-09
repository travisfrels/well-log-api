using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IComponentBusiness
    {
        ComponentBase ReadComponent(Stream dlisStream, RepresentationCode valueRepCode);
    }
}