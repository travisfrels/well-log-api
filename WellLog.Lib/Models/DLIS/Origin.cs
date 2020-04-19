using System;

namespace WellLog.Lib.Models.DLIS
{
    public class Origin
    {
        public string FileID { get; set; }
        public string FileSetName { get; set; }
        public uint FileSetNumber { get; set; }
        public string FileType { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public string Programs { get; set; }
        public DateTime CreationTime { get; set; }
        public string OrderNumber { get; set; }
        public object DescentNumber { get; set; }
        public object RunNumber { get; set; }
        public string WellName { get; set; }
        public string FieldName { get; set; }
        public uint ProducerCode { get; set; }
        public uint ProducerName { get; set; }
        public string CompanyName { get; set; }
        public string NamespaceName { get; set; }
        public uint NamespaceVersion { get; set; }
    }
}
