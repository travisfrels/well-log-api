using System;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class StorageUnitBusiness : IStorageUnitBusiness
    {
        private readonly IStorageUnitLabelBusiness _storageUnitLabelBusiness;
        private readonly IVisibleRecordBusiness _visibleRecordBusiness;

        public StorageUnitBusiness(IStorageUnitLabelBusiness storageUnitLabelBusiness, IVisibleRecordBusiness visibleRecordBusiness)
        {
            _storageUnitLabelBusiness = storageUnitLabelBusiness;
            _visibleRecordBusiness = visibleRecordBusiness;
        }

        public StorageUnit ReadStorageUnit(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var label = _storageUnitLabelBusiness.ReadStorageUnitLabel(dlisStream);
            var visibleRecords = new List<VisibleRecord>();
            while (!dlisStream.IsAtEndOfStream())
            {
                visibleRecords.Add(_visibleRecordBusiness.ReadVisibleRecord(dlisStream));
            }

            return new StorageUnit
            {
                Label = label,
                VisibleRecords = visibleRecords.ToArray()
            };
        }
    }
}
