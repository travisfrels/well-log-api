using System.IO;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentEncryptionPacketBusiness : ILogicalRecordSegmentEncryptionPacketBusiness
    {
        private readonly IUNORMReader _unormReader;

        public LogicalRecordSegmentEncryptionPacketBusiness(IUNORMReader unormReader)
        {
            _unormReader = unormReader;
        }

        public LogicalRecordSegmentEncryptionPacket ReadLogicalRecordSegmentEncryptionPacket(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            if (dlisStream.BytesRemaining() < 4) { return null; }

            var size = _unormReader.ReadUNORM(dlisStream);
            if (size < 4) { return null; }
            if (dlisStream.BytesRemaining() < size - 4) { return null; }

            return new LogicalRecordSegmentEncryptionPacket
            {
                Size = size,
                CompanyCode = _unormReader.ReadUNORM(dlisStream),
                EncryptionInfo = dlisStream.ReadBytes(size - 4)
            };
        }
    }
}
