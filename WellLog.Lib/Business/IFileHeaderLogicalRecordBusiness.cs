﻿using System.Collections.Generic;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public interface IFileHeaderLogicalRecordBusiness
    {
        bool IsFileHeader(ExplicitlyFormattedLogicalRecord eflr);
        ExplicitlyFormattedLogicalRecord GetFileHeaderEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs);
        FileHeaderLogicalRecord ConvertEFLRtoFileHeader(ExplicitlyFormattedLogicalRecord eflr);
    }
}