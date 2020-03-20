using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using WellLog.Lib;

namespace LAS
{
    public static class Initializer
    {
        public static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            WellLogModule.RegisterTypes(serviceCollection);

            serviceCollection.AddSingleton(Console.Out);
            serviceCollection.AddScoped<ILasLogPrinter, LasLogPrinter>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
