// Copyright (c) Travis Frels.  All rights reserved.
// Licensed under the Apache 2.0 License.

using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib.DataAccess;
using WellLog.Lib.Validators;

namespace LAS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("Missing LAS file argument");
                Console.WriteLine("Usage: LAS.exe <las-file-name.las>");
            }

            try
            {
                var serviceProvider = Initializer.GetServiceProvider();

                var lasLogFileDataAccess = serviceProvider.GetService<ILasLogFileDataAccess>();
                var lasLog = lasLogFileDataAccess.Read(args[0]);

                var lasLogValidator = serviceProvider.GetService<ILasLogValidator>();
                var validationErrors = lasLogValidator.ValidateLasLog(lasLog);

                var lasLogPrinter = serviceProvider.GetService<ILasLogPrinter>();
                lasLogPrinter.PrintLasLog(lasLog, validationErrors);

                lasLogFileDataAccess.Write($"{lasLog.WellIdentifier}.LAS", lasLog);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
