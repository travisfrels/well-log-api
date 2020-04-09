using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class AttributeComponent<T> : Component
    {
        public string Label { get; set; }
        public uint Count { get; set; }
        public byte RepresentationCode { get; set; }
        public string Units { get; set; }
        public IEnumerable<T> Value { get; set; }
    }
}
