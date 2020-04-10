using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class ExplicitlyFormattedLogicalRecord
    {
        public SetComponent Set { get; set; }
        public IEnumerable<AttributeComponent> Template { get; set; }
        public IEnumerable<ObjectComponent> Objects { get; set; }
    }
}
