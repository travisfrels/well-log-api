using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IExplicitlyFormattedLogicalRecordBusiness
    {
        ExplicitlyFormattedLogicalRecord ReadExplicitlyFormattedLogicalRecord(Stream dlisStream);
        IEnumerable<AttributeComponent> GetAttributesByLabel(ExplicitlyFormattedLogicalRecord eflr, string label);
    }
}