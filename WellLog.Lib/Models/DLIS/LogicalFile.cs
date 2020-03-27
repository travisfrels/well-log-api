using System;
using System.Collections.Generic;
using System.Text;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalFile
    {
        public FileHeaderLogicalRecord Header { get; set; }
        public IEnumerable<LogicalRecordBody> Records { get; set; }
    }
}
