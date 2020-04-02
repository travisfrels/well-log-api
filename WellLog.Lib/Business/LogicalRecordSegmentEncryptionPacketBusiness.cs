using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class LogicalRecordSegmentEncryptionPacketBusiness
    {
        public LogicalRecordSegmentEncryptionPacket ReadLogicalRecordSegmentEncryptionPacket(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var sizeBuffer = new byte[2];
            dlisStream.Read(sizeBuffer, 0, 2);

            var size = BitConverter.ToUInt16(sizeBuffer);

            var companyCodeBuffer = new byte[2];
            dlisStream.Read(companyCodeBuffer, 0, 2);

            var encryptionInfoBuffer = new byte[size];
            dlisStream.Read(encryptionInfoBuffer, 0, size);

            return new LogicalRecordSegmentEncryptionPacket
            {
                Size = size,
                CompanyCode = BitConverter.ToUInt16(companyCodeBuffer),
                EncryptionInfo = encryptionInfoBuffer
            };
        }
    }
}
