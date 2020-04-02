using System;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentHeaderBusiness : ILogicalRecordSegmentHeaderBusiness
    {
        public LogicalRecordSegmentHeader ReadLogicalRecordSegmentHeader(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var logicalRecordSegmentLength = new byte[2];
            dlisStream.Read(logicalRecordSegmentLength, 0, 2);

            var logicalRecordSegmentAttributes = Convert.ToByte(dlisStream.ReadByte());
            var logicalRecordType = Convert.ToByte(dlisStream.ReadByte());

            return new LogicalRecordSegmentHeader
            {
                LogicalRecordSegmentLength = BitConverter.ToUInt16(logicalRecordSegmentLength),
                LogicalRecordSegmentAttributes = logicalRecordSegmentAttributes,
                LogicalRecordType = logicalRecordType
            };
        }
    }
}
