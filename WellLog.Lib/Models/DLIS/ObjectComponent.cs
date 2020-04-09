using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class ObjectComponent : ComponentBase
    {
        public OBNAME Name { get; set; }
        public IEnumerable<AttributeComponentBase> Attributes { get; set; }
    }
}
