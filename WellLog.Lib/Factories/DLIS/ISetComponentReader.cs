using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface ISetComponentReader
    {
        ComponentBase ReadComponent(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null);
    }
}