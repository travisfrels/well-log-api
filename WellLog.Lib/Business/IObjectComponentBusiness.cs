using System.Collections;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IObjectComponentBusiness
    {
        AttributeComponent GetAttributeByLabel(ObjectComponent obj, string label);
        IEnumerable GetAttributeValueByLabel(ObjectComponent obj, string label);
        object GetFirstAttributeValueByLabel(ObjectComponent obj, string label);
    }
}