using System.Collections;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IExplicitlyFormattedLogicalRecordBusiness
    {
        ExplicitlyFormattedLogicalRecord ReadExplicitlyFormattedLogicalRecord(Stream dlisStream);
        IEnumerable<AttributeComponent> GetAttributesByLabel(ExplicitlyFormattedLogicalRecord eflr, string label);
        object GetFirstValueByLabel(ExplicitlyFormattedLogicalRecord eflr, string label);
        string GetFirstStringByLabel(ExplicitlyFormattedLogicalRecord eflr, string label);
        IEnumerable GetValueByLabel(ExplicitlyFormattedLogicalRecord eflr, string label);
    }
}
