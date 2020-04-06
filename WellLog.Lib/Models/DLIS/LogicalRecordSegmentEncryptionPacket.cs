using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentEncryptionPacket
    {
        public ushort Size { get; set; }
        public ushort CompanyCode { get; set; }
        public IEnumerable<byte> EncryptionInfo { get; set; }
    }
}
