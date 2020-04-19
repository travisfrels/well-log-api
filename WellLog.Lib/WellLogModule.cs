using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib.Business;
using WellLog.Lib.DataAccess;
using WellLog.Lib.Factories.DLIS;
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
            serviceCollection.AddScoped<IExplicitlyFormattedLogicalRecordBusiness, ExplicitlyFormattedLogicalRecordBusiness>();
            serviceCollection.AddScoped<IFileHeaderLogicalRecordBusiness, FileHeaderLogicalRecordBusiness>();
            serviceCollection.AddScoped<IOriginLogicalRecordBusiness, OriginLogicalRecordBusiness>();
            serviceCollection.AddScoped<IParameterBusiness, ParameterBusiness>();

            /* Validators */
            serviceCollection.AddScoped<ILasLogValidator, LasLogValidator>();

            /* Data Access */
            serviceCollection.AddScoped<ILasLogFileDataAccess, LasLogFileDataAccess>();
            serviceCollection.AddScoped<IDlisLogFileDataAccess, DlisLogFileDataAccess>();

            /* Readers */
            serviceCollection.AddScoped<IFSHORTReader, FSHORTReader>();
            serviceCollection.AddScoped<IFSINGLReader, FSINGLReader>();
            serviceCollection.AddScoped<IFSING1Reader, FSING1Reader>();
            serviceCollection.AddScoped<IFSING2Reader, FSING2Reader>();
            serviceCollection.AddScoped<IISINGLReader, ISINGLReader>();
            serviceCollection.AddScoped<IVSINGLReader, VSINGLReader>();
            serviceCollection.AddScoped<IFDOUBLReader, FDOUBLReader>();
            serviceCollection.AddScoped<IFDOUB1Reader, FDOUB1Reader>();
            serviceCollection.AddScoped<IFDOUB2Reader, FDOUB2Reader>();
            serviceCollection.AddScoped<ICSINGLReader, CSINGLReader>();
            serviceCollection.AddScoped<ICDOUBLReader, CDOUBLReader>();
            serviceCollection.AddScoped<ISSHORTReader, SSHORTReader>();
            serviceCollection.AddScoped<ISNORMReader, SNORMReader>();
            serviceCollection.AddScoped<ISLONGReader, SLONGReader>();
            serviceCollection.AddScoped<IUSHORTReader, USHORTReader>();
            serviceCollection.AddScoped<IUNORMReader, UNORMReader>();
            serviceCollection.AddScoped<IULONGReader, ULONGReader>();
            serviceCollection.AddScoped<IUVARIReader, UVARIReader>();
            serviceCollection.AddScoped<IIDENTReader, IDENTReader>();
            serviceCollection.AddScoped<IASCIIReader, ASCIIReader>();
            serviceCollection.AddScoped<IDTIMEReader, DTIMEReader>();
            serviceCollection.AddScoped<IOBNAMEReader, OBNAMEReader>();
            serviceCollection.AddScoped<IOBJREFReader, OBJREFReader>();
            serviceCollection.AddScoped<IATTREFReader, ATTREFReader>();
            serviceCollection.AddScoped<ISTATUSReader, STATUSReader>();
            serviceCollection.AddScoped<IUNITSReader, UNITSReader>();
            serviceCollection.AddScoped<IValueReaderFactory, ValueReaderFactory>();

            serviceCollection.AddScoped<ISetComponentReader, SetComponentReader>();
            serviceCollection.AddScoped<IObjectComponentReader, ObjectComponentReader>();
            serviceCollection.AddScoped<IAttributeComponentReader, AttributeComponentReader>();
            serviceCollection.AddScoped<IComponentReaderFactory, ComponentReaderFactory>();
        }
    }
}
