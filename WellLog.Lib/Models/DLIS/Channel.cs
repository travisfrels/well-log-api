namespace WellLog.Lib.Models.DLIS
{
    public class Channel
    {
        public string LongName { get; set; }
        public string Properties { get; set; }
        public byte? RepresentationCode { get; set; }
        public string Units { get; set; }
        public uint? Dimension { get; set; }
        public OBNAME Axis { get; set; }
        public uint? ElementLimit { get; set; }
        public OBJREF Source { get; set; }
    }
}
