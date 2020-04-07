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
        private readonly ILogicalRecordSegmentBusiness _logicalRecordSegmentBusiness;

        public StorageUnitBusiness(IStorageUnitLabelBusiness storageUnitLabelBusiness, ILogicalRecordSegmentBusiness logicalRecordSegmentBusiness)
        {
            _storageUnitLabelBusiness = storageUnitLabelBusiness;
            _logicalRecordSegmentBusiness = logicalRecordSegmentBusiness;
        }

        public StorageUnit ReadStorageUnit(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }

            var label = _storageUnitLabelBusiness.ReadStorageUnitLabel(dlisStream);
            var visibleRecords = new List<VisibleRecord>();

            while (!dlisStream.IsAtEndOfStream())
            {
                var vr = new VisibleRecord
                {
                    Length = dlisStream.ReadUNORM(),
                    FormatVersionField1 = dlisStream.ReadUSHORT(),
                    FormatVersionField2 = dlisStream.ReadUSHORT()
                };


                if (vr.FormatVersionField1 != 255 || vr.FormatVersionField2 != 1) { throw new Exception("invalid visible record format"); }

                vr.Data = dlisStream.ReadBytes(vr.Length - 4);
                if (vr.Data == null) { throw new Exception("invalid visible record length"); }

                visibleRecords.Add(vr);
            }

            return new StorageUnit
            {
                Label = label,
                VisibleRecords = visibleRecords.ToArray()
            };
        }
    }
}
