using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public interface IDlisLogFileDataAccess
    {
        IEnumerable<ExplicitlyFormattedLogicalRecord> Read(string fileName);
    }
}