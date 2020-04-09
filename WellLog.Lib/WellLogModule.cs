using Microsoft.Extensions.DependencyInjection;
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
            serviceCollection.AddScoped<IAsciiLogDataBusiness, AsciiLogDataBusiness>();
            serviceCollection.AddScoped<IWellInformationBusiness, WellInformationBusiness>();

            serviceCollection.AddScoped<IStorageUnitLabelBusiness, StorageUnitLabelBusiness>();
            serviceCollection.AddScoped<IStorageUnitBusiness, StorageUnitBusiness>();
            serviceCollection.AddScoped<IVisibleRecordBusiness, VisibleRecordBusiness>();
            serviceCollection.AddScoped<ILogicalRecordSegmentHeaderBusiness, LogicalRecordSegmentHeaderBusiness>();
            serviceCollection.AddScoped<ILogicalRecordSegmentEncryptionPacketBusiness, LogicalRecordSegmentEncryptionPacketBusiness>();
            serviceCollection.AddScoped<ILogicalRecordSegmentTrailerBusiness, LogicalRecordSegmentTrailerBusiness>();
            serviceCollection.AddScoped<ILogicalRecordSegmentBusiness, LogicalRecordSegmentBusiness>();
            serviceCollection.AddScoped<IComponentBusiness, ComponentBusiness>();

            /* Validators */
            serviceCollection.AddScoped<ILasLogValidator, LasLogValidator>();

            /* Data Access */
            serviceCollection.AddScoped<ILasLogFileDataAccess, LasLogFileDataAccess>();
            serviceCollection.AddScoped<IDlisLogFileDataAccess, DlisLogFileDataAccess>();
        }
    }
}
