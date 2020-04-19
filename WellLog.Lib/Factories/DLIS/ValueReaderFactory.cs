using Microsoft.Extensions.DependencyInjection;
using System;
using WellLog.Lib.Enumerations.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class ValueReaderFactory : IValueReaderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ValueReaderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValueReader GetReader(RepresentationCode repCode)
        {
            return repCode switch
            {
                RepresentationCode.FSHORT => (IValueReader)_serviceProvider.GetService<IFSHORTReader>(),
                RepresentationCode.FSINGL => (IValueReader)_serviceProvider.GetService<IFSINGLReader>(),
                RepresentationCode.FSING1 => (IValueReader)_serviceProvider.GetService<IFSING1Reader>(),
                RepresentationCode.FSING2 => (IValueReader)_serviceProvider.GetService<IFSING2Reader>(),
                RepresentationCode.ISINGL => (IValueReader)_serviceProvider.GetService<IISINGLReader>(),
                RepresentationCode.VSINGL => (IValueReader)_serviceProvider.GetService<IVSINGLReader>(),
                RepresentationCode.FDOUBL => (IValueReader)_serviceProvider.GetService<IFDOUBLReader>(),
                RepresentationCode.FDOUB1 => (IValueReader)_serviceProvider.GetService<IFDOUB1Reader>(),
                RepresentationCode.FDOUB2 => (IValueReader)_serviceProvider.GetService<IFDOUB2Reader>(),
                RepresentationCode.CSINGL => (IValueReader)_serviceProvider.GetService<ICSINGLReader>(),
                RepresentationCode.CDOUBL => (IValueReader)_serviceProvider.GetService<ICDOUBLReader>(),
                RepresentationCode.SSHORT => (IValueReader)_serviceProvider.GetService<ISSHORTReader>(),
                RepresentationCode.SNORM => (IValueReader)_serviceProvider.GetService<ISNORMReader>(),
                RepresentationCode.SLONG => (IValueReader)_serviceProvider.GetService<ISLONGReader>(),
                RepresentationCode.USHORT => (IValueReader)_serviceProvider.GetService<IUSHORTReader>(),
                RepresentationCode.UNORM => (IValueReader)_serviceProvider.GetService<IUNORMReader>(),
                RepresentationCode.ULONG => (IValueReader)_serviceProvider.GetService<IULONGReader>(),
                RepresentationCode.UVARI => (IValueReader)_serviceProvider.GetService<IUVARIReader>(),
                RepresentationCode.IDENT => (IValueReader)_serviceProvider.GetService<IIDENTReader>(),
                RepresentationCode.ASCII => (IValueReader)_serviceProvider.GetService<IASCIIReader>(),
                RepresentationCode.DTIME => (IValueReader)_serviceProvider.GetService<IDTIMEReader>(),
                RepresentationCode.OBNAME => (IValueReader)_serviceProvider.GetService<IOBNAMEReader>(),
                RepresentationCode.OBJREF => (IValueReader)_serviceProvider.GetService<IOBJREFReader>(),
                RepresentationCode.ATTREF => (IValueReader)_serviceProvider.GetService<IATTREFReader>(),
                RepresentationCode.STATUS => (IValueReader)_serviceProvider.GetService<ISTATUSReader>(),
                RepresentationCode.UNITS => (IValueReader)_serviceProvider.GetService<IUNITSReader>(),
                _ => null,
            };
        }
    }
}
