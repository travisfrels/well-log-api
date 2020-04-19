using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IParameterBusiness
    {
        bool IsParameter(ExplicitlyFormattedLogicalRecord eflr);
        IEnumerable<ExplicitlyFormattedLogicalRecord> GetParameterEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        Parameter ConvertEFLRtoParameter(ExplicitlyFormattedLogicalRecord eflr);
    }
}