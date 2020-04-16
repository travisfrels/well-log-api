using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IOriginLogicalRecordBusiness
    {
        OriginLogicalRecord ConvertEFLRtoOrigin(ExplicitlyFormattedLogicalRecord eflr);
        IEnumerable<ExplicitlyFormattedLogicalRecord> GetOriginEFLRs(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        bool IsOrigin(ExplicitlyFormattedLogicalRecord eflr);
    }
}