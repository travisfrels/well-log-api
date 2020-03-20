using System.Collections.Generic;
using WellLog.Lib.Models;

namespace LAS
{
    public interface ILasLogPrinter
    {
        void PrintLasLog(LasLog lasLog, IEnumerable<ValidationError> validationErrors);
    }
}