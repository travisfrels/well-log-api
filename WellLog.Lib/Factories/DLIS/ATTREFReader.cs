using System.Collections;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class ATTREFReader : IValueReader, IATTREFReader
    {
        private readonly IIDENTReader _identReader;
        private readonly IOBNAMEReader _obnameReader;

        public ATTREFReader(IIDENTReader identReader, IOBNAMEReader obnameReader)
        {
            _identReader = identReader;
            _obnameReader = obnameReader;
        }

        public ATTREF ReadATTREF(Stream s)
        {
            if (s == null || s.BytesRemaining() < 5) { return null; }
            return new ATTREF
            {
                ObjectType = _identReader.ReadIDENT(s),
                Name = _obnameReader.ReadOBNAME(s),
                Label = _identReader.ReadIDENT(s)
            };
        }

        public IEnumerable ReadValues(Stream s, uint count)
        {
            if (s == null || s.BytesRemaining() < (count * 5)) { yield break; }
            for (uint i = 0; i < count; i++) { yield return ReadATTREF(s); }
        }
    }
}
