using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;
using WellLog.Lib.Helpers;

namespace WellLog.Lib.DataAccess
{
    public class DlisLogFileDataAccess : IDlisLogFileDataAccess
    {
        private readonly IStorageUnitBusiness _storageUnitBusiness;
        private readonly IExplicitlyFormattedLogicalRecordBusiness _explicitlyFormattedLogicalRecordBusiness;

        public DlisLogFileDataAccess(IStorageUnitBusiness storageUnitBusiness, IExplicitlyFormattedLogicalRecordBusiness explicitlyFormattedLogicalRecordBusiness)
        {
            _storageUnitBusiness = storageUnitBusiness;
            _explicitlyFormattedLogicalRecordBusiness = explicitlyFormattedLogicalRecordBusiness;
        }

        public IEnumerable<ExplicitlyFormattedLogicalRecord> Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }
            StorageUnit storageUnit;
            using (var dlisFile = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                storageUnit = _storageUnitBusiness.ReadStorageUnit(dlisFile);
            }

            var eflrData = storageUnit
                .VisibleRecords
                .SelectMany(x => x.Segments.Where(x => x.Header.LogicalRecordStructure))
                .SelectMany(x => x.Body)
                .ToArray();

            var eflrRecords = new List<ExplicitlyFormattedLogicalRecord>();
            using (var eflrStream = new MemoryStream(eflrData))
            {
                while(!eflrStream.IsAtEndOfStream())
                {
                    var eflr = _explicitlyFormattedLogicalRecordBusiness.ReadExplicitlyFormattedLogicalRecord(eflrStream);
                    if (eflr != null) { eflrRecords.Add(eflr); }
                }
            }

            return eflrRecords.ToArray();
        }
    }
}
