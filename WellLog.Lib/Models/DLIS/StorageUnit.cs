using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class StorageUnit
    {
        public StorageUnitLabel Label { get; set; }
        public IEnumerable<LogicalRecordSegment> Segments { get; set; }
    }
}
