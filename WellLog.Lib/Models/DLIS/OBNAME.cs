using System;
using System.Collections.Generic;
using System.Text;

namespace WellLog.Lib.Models.DLIS
{
    public class OBNAME
    {
        public uint Origin { get; set; }
        public byte CopyNumber { get; set; }
        public string Identifier { get; set; }
    }
}
