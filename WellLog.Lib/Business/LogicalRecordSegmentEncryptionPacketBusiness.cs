using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentEncryptionPacketBusiness : ILogicalRecordSegmentEncryptionPacketBusiness
    {
        public LogicalRecordSegmentEncryptionPacket ReadLogicalRecordSegmentEncryptionPacket(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.BytesRemaining() < 4) { return null; }

            var size = dlisStream.ReadUNORM();
            if (size < 4) { return null; }
            if (dlisStream.BytesRemaining() < size - 4) { return null; }

            return new LogicalRecordSegmentEncryptionPacket
            {
                Size = size,
                CompanyCode = dlisStream.ReadUNORM(),
                EncryptionInfo = dlisStream.ReadBytes(size - 4)
            };
        }
    }
}
