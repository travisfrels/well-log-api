using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IComponentBusiness
    {
        Component ReadComponent(Stream dlisStream);
    }
}