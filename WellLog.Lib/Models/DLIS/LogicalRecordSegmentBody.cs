using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentBody
    {
        public IEnumerable<byte> Data { get; set; }
    }
}
