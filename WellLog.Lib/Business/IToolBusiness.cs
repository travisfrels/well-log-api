using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IToolBusiness
    {
        IEnumerable<Tool> ConvertEFLRtoTools(ExplicitlyFormattedLogicalRecord eflr);
        IEnumerable<ExplicitlyFormattedLogicalRecord> GetToolEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        bool IsTool(ExplicitlyFormattedLogicalRecord eflr);
    }
}