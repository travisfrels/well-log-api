// Copyright (c) Travis Frels.  All rights reserved.
// Licensed under the Apache 2.0 License.

using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib.DataAccess;

namespace DLIS
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args == null || args.Length < 1)
            //{
            //    Console.WriteLine("Missing DLIS file argument");
            //    Console.WriteLine("Usage: DLIS.exe <dlis-file-name.dlis>");
            //}

            try
            {
                var serviceProvider = Initializer.GetServiceProvider();

                var dlisLogFileDataAccess = serviceProvider.GetService<IDlisLogFileDataAccess>();
                var dlisLog = dlisLogFileDataAccess.Read("sample2.DLIS");

                //var dlisLogPrinter = serviceProvider.GetService<IDlisLogPrinter>();
                //dlisLogPrinter.PrintLasLog(dlisLog);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
