using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class ObjectComponentReader : IComponentReader, IObjectComponentReader
    {
        private readonly IOBNAMEReader _obnameReader;

        public ObjectComponentReader(IOBNAMEReader obnameReader)
        {
            _obnameReader = obnameReader;
        }

        public ComponentBase ReadComponent(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            var objComponent = new ObjectComponent { Descriptor = descriptor };
            if (descriptor.DoesObjectHaveName) { objComponent.Name = _obnameReader.ReadOBNAME(s); }
            return objComponent;
        }
    }
}
