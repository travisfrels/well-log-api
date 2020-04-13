using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class DlisLog
    {
        public IEnumerable<ExplicitlyFormattedLogicalRecord> EFLRs { get; set; }
    }
}
