namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegment
    {
        public LogicalRecordSegmentHeader Header { get; set; }
        public LogicalRecordSegmentEncryptionPacket EncryptionPacket { get; set; }
        public LogicalRecordSegmentBody Body { get; set; }
        public LogicalRecordSegmentTrailer Trailer { get; set; }
    }
}
