using System.IO;
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

            return new StorageUnit
            {
                Label = _storageUnitLabelBusiness.ReadStorageUnitLabel(dlisStream),
                Segments = new LogicalRecordSegment[] { _logicalRecordSegmentBusiness.ReadLogicalRecordSegment(dlisStream) }
            };
        }
    }
}
