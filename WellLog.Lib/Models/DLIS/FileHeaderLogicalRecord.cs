namespace WellLog.Lib.Models.DLIS
{
    /* A File Header Logical Record is an Explicitly Formatted Logical Record (EFLR) with Type FHLR.  Each EFLR contains one and only one Set. */
    public class FileHeaderLogicalRecord
    {
        public LogicalRecordSegment Segment { get; set; }
    }
}
