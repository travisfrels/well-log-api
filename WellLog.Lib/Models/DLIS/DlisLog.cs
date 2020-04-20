using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class DlisLog
    {
        public StorageUnitLabel Label { get; set; }
        public IEnumerable<ExplicitlyFormattedLogicalRecord> EFLRs { get; set; }
        public FileHeaderLogicalRecord FileHeader { get; set; }
        public IEnumerable<OriginLogicalRecord> Origins { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<Channel> Channels { get; set; }
    }
}
