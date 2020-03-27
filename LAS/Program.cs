using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WellLog.Lib;
using WellLog.Lib.DataAccess;
using WellLog.Lib.Validators;

namespace LAS
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceCollection = new ServiceCollection();
            WellLogModule.RegisterTypes(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var lasLogFileDataAccess = serviceProvider.GetService<ILasLogFileDataAccess>();
            var lasLog = lasLogFileDataAccess.Read("sample2.las");
            foreach (var section in lasLog.Sections)
            {
                Console.WriteLine($"{section.SectionType}");

                if (section.MnemonicsLines != null)
                {
                    foreach (var mnemonicLine in section.MnemonicsLines)
                    {
                        Console.WriteLine($"\tMnemonic: {mnemonicLine.Mnemonic}; Units: {mnemonicLine.Units}; Data: {mnemonicLine.Data}; Description: {mnemonicLine.Description}");
                    }
                }

                if (section.AsciiLogDataLines != null)
                {
                    foreach (var asciiLine in section.AsciiLogDataLines.Take(25))
                    {
                        Console.WriteLine($"\t{string.Join('\t', asciiLine.Values)}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("----------");
            Console.WriteLine();

            var lasLogValidator = serviceProvider.GetService<ILasLogValidator>();
            var validationErrors = lasLogValidator.ValidateLasLog(lasLog);
            if (validationErrors.Any())
            {
                foreach (var verr in validationErrors)
                {
                    Console.WriteLine(verr.Message);
                }
            }
            else
            {
                Console.WriteLine("No validation errors.");
            }

            lasLogFileDataAccess.Write($"{lasLog.WellIdentifier}.LAS", lasLog);
        }
    }
}
