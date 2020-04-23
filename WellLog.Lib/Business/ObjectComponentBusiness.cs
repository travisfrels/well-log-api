using System.Collections;
using System.Linq;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ObjectComponentBusiness : IObjectComponentBusiness
    {
        public AttributeComponent GetAttributeByLabel(ObjectComponent obj, string label)
        {
            if (obj == null || obj.Attributes == null) { return null; }
            return obj.Attributes.Where(x => string.Compare(x.Label, label, true) == 0).FirstOrDefault();
        }

        public IEnumerable GetAttributeValueByLabel(ObjectComponent obj, string label)
        {
            var attribute = GetAttributeByLabel(obj, label);
            if (attribute == null) { return null; }
            return attribute.Value;
        }

        public object GetFirstAttributeValueByLabel(ObjectComponent obj, string label)
        {
            var value = GetAttributeValueByLabel(obj, label);
            if (value == null) { return null; }
            return value.First();
        }
    }
}
