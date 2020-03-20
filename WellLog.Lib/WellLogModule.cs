﻿using Microsoft.Extensions.DependencyInjection;
using WellLog.Lib.Business;
using WellLog.Lib.DataAccess;
using WellLog.Lib.Validators;

namespace WellLog.Lib
{
    public static class WellLogModule
    {
        public static void RegisterTypes(IServiceCollection serviceCollection)
        {

            /* Business */
            serviceCollection.AddScoped<ILasLogBusiness, LasLogBusiness>();
            serviceCollection.AddScoped<ILasSectionBusiness, LasSectionBusiness>();
            serviceCollection.AddScoped<ILasSectionLineBusiness, LasSectionLineBusiness>();

            /* Validators */
            serviceCollection.AddScoped<ILasLogValidator, LasLogValidator>();

            /* Data Access */
            serviceCollection.AddScoped<ILasLogFileDataAccess, LasLogFileDataAccess>();

            /* Services */

            /* Jobs */
        }
    }
}
