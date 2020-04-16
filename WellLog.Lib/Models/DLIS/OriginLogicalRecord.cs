using System;

namespace WellLog.Lib.Models.DLIS
{
    public class OriginLogicalRecord
    {
        public string FileID { get; set; }
        public string FileSetName { get; set; }
        public uint? FileSetNumber { get; set; }
        public uint? FileNumber { get; set; }
        public string FileType { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public string Programs { get; set; }
        public DateTime? CreationTime { get; set; }
        public string OrderNumber { get; set; }
        public object DescentNumber { get; set; }
        public object RunNumber { get; set; }
        public string WellName { get; set; }
        public string FieldName { get; set; }
        public ushort? ProducerCode { get; set; }
        public string ProducerName { get; set; }
        public string Company { get; set; }
        public string NameSpaceName { get; set; }
        public uint? NameSpaceVersion { get; set; }
    }
}
