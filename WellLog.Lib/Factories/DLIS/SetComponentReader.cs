using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class SetComponentReader : IComponentReader, ISetComponentReader
    {
        private readonly IIDENTReader _identReader;

        public SetComponentReader(IIDENTReader identReader)
        {
            _identReader = identReader;
        }

        public ComponentBase ReadComponent(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            var setComponent = new SetComponent { Descriptor = descriptor };
            if (descriptor.DoesSetHaveType) { setComponent.Type = _identReader.ReadIDENT(s); }
            if (descriptor.DoesSetHaveName) { setComponent.Name = _identReader.ReadIDENT(s); }
            return setComponent;
        }
    }
}
