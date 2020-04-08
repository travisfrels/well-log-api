using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class LogicalRecordSegmentHeaderHelpers
    {
        public static bool HasTrailer(this LogicalRecordSegmentHeader header)
        {
            if (header == null) { return false; }
            return header.TrailingLength || header.Checksum || header.Padding;
        }
        public static int TrailerSize(this LogicalRecordSegmentHeader header, LogicalRecordSegmentTrailer trailer)
        {
            if (header == null) { return 0; }
            if (trailer == null) { return 0; }

            var size = 0;
            if (header.TrailingLength) { size += 2; }
            if (header.Checksum) { size += 2; }
            if (header.Padding) { size += trailer.PadCount; }

            return size;
        }
    }
}
