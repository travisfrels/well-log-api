using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class VisibleRecord
    {
        public ushort Length { get; set; }
        public byte FormatVersionField1 { get; set; }
        public byte FormatVersionField2 { get; set; }
        public IEnumerable<byte> Data { get; set; }
    }
}
