using System.Collections.Generic;
using WellLog.Lib.Enumerations.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public static class ValueReaderFactory
    {
        private static readonly Dictionary<RepresentationCode, IValueReader> _readers = new Dictionary<RepresentationCode, IValueReader>();

        public static void RegisterReaders()
        {
            _readers.Add(RepresentationCode.FSHORT, new FSHORTReader());
            _readers.Add(RepresentationCode.FSINGL, new FSINGLReader());
            _readers.Add(RepresentationCode.FSING1, new FSING1Reader((FSINGLReader)_readers[RepresentationCode.FSINGL]));
            _readers.Add(RepresentationCode.FSING2, new FSING2Reader((FSINGLReader)_readers[RepresentationCode.FSINGL]));
            _readers.Add(RepresentationCode.ISINGL, new ISINGLReader());
            _readers.Add(RepresentationCode.VSINGL, new VSINGLReader());
            _readers.Add(RepresentationCode.FDOUBL, new FDOUBLReader());
            _readers.Add(RepresentationCode.FDOUB1, new FDOUB1Reader((FDOUBLReader)_readers[RepresentationCode.FDOUBL]));
            _readers.Add(RepresentationCode.FDOUB2, new FDOUB2Reader((FDOUBLReader)_readers[RepresentationCode.FDOUBL]));
            _readers.Add(RepresentationCode.CSINGL, new CSINGLReader((FSINGLReader)_readers[RepresentationCode.FSINGL]));
            _readers.Add(RepresentationCode.CDOUBL, new CDOUBLReader((FDOUBLReader)_readers[RepresentationCode.FDOUBL]));
            _readers.Add(RepresentationCode.SSHORT, new SSHORTReader());
            _readers.Add(RepresentationCode.SNORM, new SNORMReader());
            _readers.Add(RepresentationCode.SLONG, new SLONGReader());
            _readers.Add(RepresentationCode.USHORT, new USHORTReader());
            _readers.Add(RepresentationCode.UNORM, new UNORMReader());
            _readers.Add(RepresentationCode.ULONG, new ULONGReader());
            _readers.Add(RepresentationCode.UVARI, new UVARIReader());
            _readers.Add(RepresentationCode.IDENT, new IDENTReader());
            _readers.Add(RepresentationCode.ASCII, new ASCIIReader((UVARIReader)_readers[RepresentationCode.UVARI]));
            _readers.Add(RepresentationCode.DTIME, new DTIMEReader());
            _readers.Add(RepresentationCode.OBNAME, new OBNAMEReader((UVARIReader)_readers[RepresentationCode.UVARI], (USHORTReader)_readers[RepresentationCode.USHORT], (IDENTReader)_readers[RepresentationCode.IDENT]));
            _readers.Add(RepresentationCode.OBJREF, new OBJREFReader((IDENTReader)_readers[RepresentationCode.IDENT], (OBNAMEReader)_readers[RepresentationCode.OBNAME]));
            _readers.Add(RepresentationCode.ATTREF, new ATTREFReader((IDENTReader)_readers[RepresentationCode.IDENT], (OBNAMEReader)_readers[RepresentationCode.OBNAME]));
            _readers.Add(RepresentationCode.STATUS, new STATUSReader());
            _readers.Add(RepresentationCode.UNITS, new UNITSReader());
        }

        public static IValueReader GetReader(RepresentationCode r)
        {
            return _readers[r];
        }
    }
}
