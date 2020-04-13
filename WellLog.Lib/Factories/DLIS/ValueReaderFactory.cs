using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WellLog.Lib.Enumerations.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public static class ValueReaderFactory
    {
        private static readonly Dictionary<RepresentationCode, IValueReader> _readers = new Dictionary<RepresentationCode, IValueReader>();

        public static void RegisterReaders(IServiceProvider serviceProvider)
        {
            _readers.Add(RepresentationCode.FSHORT, (IValueReader)serviceProvider.GetService<IFSHORTReader>());
            _readers.Add(RepresentationCode.FSINGL, (IValueReader)serviceProvider.GetService<IFSINGLReader>());
            _readers.Add(RepresentationCode.FSING1, (IValueReader)serviceProvider.GetService<IFSING1Reader>());
            _readers.Add(RepresentationCode.FSING2, (IValueReader)serviceProvider.GetService<IFSING2Reader>());
            _readers.Add(RepresentationCode.ISINGL, (IValueReader)serviceProvider.GetService<IISINGLReader>());
            _readers.Add(RepresentationCode.VSINGL, (IValueReader)serviceProvider.GetService<IVSINGLReader>());
            _readers.Add(RepresentationCode.FDOUBL, (IValueReader)serviceProvider.GetService<IFDOUBLReader>());
            _readers.Add(RepresentationCode.FDOUB1, (IValueReader)serviceProvider.GetService<IFDOUB1Reader>());
            _readers.Add(RepresentationCode.FDOUB2, (IValueReader)serviceProvider.GetService<IFDOUB2Reader>());
            _readers.Add(RepresentationCode.CSINGL, (IValueReader)serviceProvider.GetService<ICSINGLReader>());
            _readers.Add(RepresentationCode.CDOUBL, (IValueReader)serviceProvider.GetService<ICDOUBLReader>());
            _readers.Add(RepresentationCode.SSHORT, (IValueReader)serviceProvider.GetService<ISSHORTReader>());
            _readers.Add(RepresentationCode.SNORM, (IValueReader)serviceProvider.GetService<ISNORMReader>());
            _readers.Add(RepresentationCode.SLONG, (IValueReader)serviceProvider.GetService<ISLONGReader>());
            _readers.Add(RepresentationCode.USHORT, (IValueReader)serviceProvider.GetService<IUSHORTReader>());
            _readers.Add(RepresentationCode.UNORM, (IValueReader)serviceProvider.GetService<IUNORMReader>());
            _readers.Add(RepresentationCode.ULONG, (IValueReader)serviceProvider.GetService<IULONGReader>());
            _readers.Add(RepresentationCode.UVARI, (IValueReader)serviceProvider.GetService<IUVARIReader>());
            _readers.Add(RepresentationCode.IDENT, (IValueReader)serviceProvider.GetService<IIDENTReader>());
            _readers.Add(RepresentationCode.ASCII, (IValueReader)serviceProvider.GetService<IASCIIReader>());
            _readers.Add(RepresentationCode.DTIME, (IValueReader)serviceProvider.GetService<IDTIMEReader>());
            _readers.Add(RepresentationCode.OBNAME, (IValueReader)serviceProvider.GetService<IOBNAMEReader>());
            _readers.Add(RepresentationCode.OBJREF, (IValueReader)serviceProvider.GetService<IOBJREFReader>());
            _readers.Add(RepresentationCode.ATTREF, (IValueReader)serviceProvider.GetService<IATTREFReader>());
            _readers.Add(RepresentationCode.STATUS, (IValueReader)serviceProvider.GetService<ISTATUSReader>());
            _readers.Add(RepresentationCode.UNITS, (IValueReader)serviceProvider.GetService<IUNITSReader>());
        }

        public static IValueReader GetReader(RepresentationCode r)
        {
            return _readers[r];
        }
    }
}
