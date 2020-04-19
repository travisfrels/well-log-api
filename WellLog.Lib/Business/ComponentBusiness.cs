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
        private readonly IComponentReaderFactory _componentReaderFactory;

        public ComponentBusiness(IUSHORTReader ushortReader, IComponentReaderFactory componentReaderFactory)
        {
            _ushortReader = ushortReader;
            _componentReaderFactory = componentReaderFactory;
        }

        public ComponentBase ReadComponent(Stream dlisStream, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var startPosition = dlisStream.Position;

            var descriptor = new ComponentDescriptor(_ushortReader.ReadUSHORT(dlisStream));
            var componentReader = _componentReaderFactory.GetReader(descriptor);
            if (componentReader == null) { throw new Exception($"no component reader found for role {descriptor.Role}"); }

            var component = componentReader.ReadComponent(dlisStream, descriptor, template);
            component.StartPosition = startPosition;

            return component;
        }
    }
}
