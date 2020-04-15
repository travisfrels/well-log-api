using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public class DlisLogFileDataAccess : IDlisLogFileDataAccess
    {
        private readonly IStorageUnitBusiness _storageUnitBusiness;
        private readonly IExplicitlyFormattedLogicalRecordBusiness _explicitlyFormattedLogicalRecordBusiness;
        private readonly IFileHeaderLogicalRecordBusiness _fileHeaderLogicalRecordBusiness;

        public DlisLogFileDataAccess(IStorageUnitBusiness storageUnitBusiness, IExplicitlyFormattedLogicalRecordBusiness explicitlyFormattedLogicalRecordBusiness, IFileHeaderLogicalRecordBusiness fileHeaderLogicalRecordBusiness)
        {
            _storageUnitBusiness = storageUnitBusiness;
            _explicitlyFormattedLogicalRecordBusiness = explicitlyFormattedLogicalRecordBusiness;
            _fileHeaderLogicalRecordBusiness = fileHeaderLogicalRecordBusiness;
        }

        public DlisLog Read(string fileName)
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
                while (!eflrStream.IsAtEndOfStream())
                {
                    var eflr = _explicitlyFormattedLogicalRecordBusiness.ReadExplicitlyFormattedLogicalRecord(eflrStream);
                    if (eflr != null) { eflrRecords.Add(eflr); }
                }
            }

            return new DlisLog
            {
                Label = storageUnit.Label,
                EFLRs = eflrRecords.ToArray(),
                FileHeader = _fileHeaderLogicalRecordBusiness.ConvertEFLRtoFileHeader(_fileHeaderLogicalRecordBusiness.GetFileHeaderEFLR(eflrRecords))
            };
        }
    }
}
