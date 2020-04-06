﻿using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface ILogicalRecordSegmentEncryptionPacketBusiness
    {
        LogicalRecordSegmentEncryptionPacket ReadLogicalRecordSegmentEncryptionPacket(Stream dlisStream);
    }
}