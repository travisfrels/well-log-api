using System.Collections.Generic;
using WellLog.Lib.Models;

namespace WellLog.Lib.Validators
{
    public interface ILasLogValidator
    {
        IEnumerable<ValidationError> ValidateLasLog(params LasLog[] lasLogs);
    }
}