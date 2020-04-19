using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class ComponentReaderFactory : IComponentReaderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ComponentReaderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IComponentReader GetReader(ComponentDescriptor descriptor)
        {
            if (descriptor == null) { return null; }

            return descriptor.Role switch
            {
                ComponentDescriptor.ABSENT_ATTRIBUTE_ROLE => (IComponentReader)_serviceProvider.GetService<IAttributeComponentReader>(),
                ComponentDescriptor.ATTRIBUTE_ROLE => (IComponentReader)_serviceProvider.GetService<IAttributeComponentReader>(),
                ComponentDescriptor.INVARIANT_ATTRIBUTE_ROLE => (IComponentReader)_serviceProvider.GetService<IAttributeComponentReader>(),
                ComponentDescriptor.OBJECT_ROLE => (IComponentReader)_serviceProvider.GetService<IObjectComponentReader>(),
                ComponentDescriptor.REDUNDANT_SET_ROLE => (IComponentReader)_serviceProvider.GetService<ISetComponentReader>(),
                ComponentDescriptor.REPLACEMENT_SET_ROLE => (IComponentReader)_serviceProvider.GetService<ISetComponentReader>(),
                ComponentDescriptor.SET_ROLE => (IComponentReader)_serviceProvider.GetService<ISetComponentReader>(),
                _ => null,
            };
        }
    }
}
