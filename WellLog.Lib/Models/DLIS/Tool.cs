using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class Tool
    {
        public string Description { get; set; }
        public string TrademarkName { get; set; }
        public string GenericName { get; set; }
        public string Parts { get; set; }
        public string Status { get; set; }
        public IEnumerable<OBNAME> Channels { get; set; }
        public IEnumerable<OBNAME> Parameters { get; set; }
    }
}
