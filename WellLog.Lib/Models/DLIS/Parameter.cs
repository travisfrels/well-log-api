using System.Collections;

namespace WellLog.Lib.Models.DLIS
{
    public class Parameter
    {
        public OBNAME LongName { get; set; }
        public uint Dimension { get; set; }
        public OBNAME Axis { get; set; }
        public OBNAME Zones { get; set; }
        public IEnumerable Values { get; set; }
    }
}
