using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordBody
    {
        public IEnumerable<LogicalRecordSegment> Segments { get; set; }
    }
}
