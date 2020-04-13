using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib;

namespace DLIS
{
    public static class Initializer
    {
        public static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            WellLogModule.RegisterTypes(serviceCollection);

            serviceCollection.AddSingleton(Console.Out);
            serviceCollection.AddScoped<IDlisLogPrinter, DlisLogPrinter>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            WellLogModule.InitFactories(serviceProvider);

            return serviceProvider;
        }
    }
}
