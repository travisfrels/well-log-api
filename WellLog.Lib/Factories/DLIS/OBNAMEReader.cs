using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class OBNAMEReader : IValueReader, IOBNAMEReader
    {
        private readonly IUVARIReader _uvariReader;
        private readonly IUSHORTReader _ushortReader;
        private readonly IIDENTReader _identReader;

        public OBNAMEReader(IUVARIReader uvariReader, IUSHORTReader ushortReader, IIDENTReader identReader)
        {
            _uvariReader = uvariReader;
            _ushortReader = ushortReader;
            _identReader = identReader;
        }

        public OBNAME ReadOBNAME(Stream s)
        {
            if (s == null || s.BytesRemaining() < 3) { return null; }
            return new OBNAME
            {
                Origin = _uvariReader.ReadUVARI(s),
                CopyNumber = _ushortReader.ReadUSHORT(s),
                Identifier = _identReader.ReadIDENT(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 3)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadOBNAME(s); }
        }
    }
}
