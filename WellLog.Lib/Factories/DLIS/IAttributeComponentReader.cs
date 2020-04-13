using System.Collections;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public interface IAttributeComponentReader
    {
        uint ReadAttributeCount(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null);
        string ReadAttributeLabel(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null);
        byte ReadAttributeRepresentationCode(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null);
        string ReadAttributeUnits(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null);
        IEnumerable ReadAttributeValue(Stream dlisStream, ComponentDescriptor descriptor, byte repCode, uint count);
        ComponentBase ReadComponent(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null);
    }
}