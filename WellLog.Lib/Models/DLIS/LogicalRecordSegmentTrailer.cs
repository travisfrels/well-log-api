namespace WellLog.Lib.Models.DLIS
{
    public class LogicalRecordSegmentTrailer
    {
        public uint Checksum { get; set; }
        public uint LogicalRecordSegmentLength { get; set; }
    }
}
