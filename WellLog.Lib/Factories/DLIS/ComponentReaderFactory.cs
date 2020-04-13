using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public static class ComponentReaderFactory
    {
        private static readonly Dictionary<byte, IComponentReader> _readers = new Dictionary<byte, IComponentReader>();

        public static void RegisterReaders(IServiceProvider serviceProvider)
        {
            var attributeReader = (IComponentReader)serviceProvider.GetService<IAttributeComponentReader>();
            var objectReader = (IComponentReader)serviceProvider.GetService<IObjectComponentReader>();
            var setReader = (IComponentReader)serviceProvider.GetService<ISetComponentReader>();

            _readers.Add(ComponentDescriptor.ABSENT_ATTRIBUTE_ROLE, attributeReader);
            _readers.Add(ComponentDescriptor.ATTRIBUTE_ROLE, attributeReader);
            _readers.Add(ComponentDescriptor.INVARIANT_ATTRIBUTE_ROLE, attributeReader);
            _readers.Add(ComponentDescriptor.OBJECT_ROLE, objectReader);
            _readers.Add(ComponentDescriptor.REDUNDANT_SET_ROLE, setReader);
            _readers.Add(ComponentDescriptor.REPLACEMENT_SET_ROLE, setReader);
            _readers.Add(ComponentDescriptor.SET_ROLE, setReader);
        }

        public static IComponentReader GetReader(ComponentDescriptor descriptor)
        {
            if (descriptor == null) { return null; }
            return _readers[descriptor.Role];
        }
    }
}
