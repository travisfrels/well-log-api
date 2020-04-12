using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class OBJREFReader : IValueReader
    {
        private readonly IDENTReader _identReader;
        private readonly OBNAMEReader _obnameReader;

        public OBJREFReader(IDENTReader identReader, OBNAMEReader obnameReader)
        {
            _identReader = identReader;
            _obnameReader = obnameReader;
        }

        public OBJREF ReadOBJREF(Stream s)
        {
            if (s == null || s.BytesRemaining() < 4) { return null; }
            return new OBJREF
            {
                ObjectType = _identReader.ReadIDENT(s),
                Name = _obnameReader.ReadOBNAME(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 4)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadOBJREF(s); }
        }
    }
}
