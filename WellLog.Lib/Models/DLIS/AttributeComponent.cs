using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class AttributeComponent : Component
    {
        public string Label { get; set; }
        public ulong Count { get; set; }
        public ushort RepresentationCode { get; set; }
        public string Units { get; set; }
        public IEnumerable<byte> Value { get; set; }
    }
}
