using System;
using System.IO;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ComponentBusiness : IComponentBusiness
    {
        private readonly IUSHORTReader _ushortReader;

        public ComponentBusiness(IUSHORTReader ushortReader)
        {
            _ushortReader = ushortReader;
        }

        public ComponentBase ReadComponent(Stream dlisStream, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var startPosition = dlisStream.Position;

            var descriptor = new ComponentDescriptor(_ushortReader.ReadUSHORT(dlisStream));
            var componentReader = ComponentReaderFactory.GetReader(descriptor);
            if (componentReader == null) { throw new Exception($"no component reader found for role {descriptor.Role}"); }

            var component = componentReader.ReadComponent(dlisStream, descriptor, template);
            component.StartPosition = startPosition;

            return component;
        }
    }
}
