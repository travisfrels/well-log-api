using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public abstract class AttributeComponentBase : ComponentBase
    {
        public string Label { get; set; }
        public uint Count { get; set; }
        public byte RepresentationCode { get; set; }
        public string Units { get; set; }
    }

    public class AttributeComponent<T> : AttributeComponentBase
    {
        public IEnumerable<T> Value { get; set; }
    }
}
