using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentTrailer
    {
        public byte Padding { get; set; }
        public ushort Checksum { get; set; }
        public ushort TrailingLength { get; set; }
    }
}
