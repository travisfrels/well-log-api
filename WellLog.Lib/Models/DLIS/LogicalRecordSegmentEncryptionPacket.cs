using System.Collections.Generic;

namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentEncryptionPacket
    {
        public uint Size { get; set; }
        public uint CompanyCode { get; set; }
        public IEnumerable<byte> EncryptionInfo { get; set; }
    }
}
