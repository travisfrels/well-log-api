using System.IO;
using System.Text;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class StorageUnitLabelBusiness : IStorageUnitLabelBusiness
    {
        public StorageUnitLabel ReadStorageUnitLabel(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            var label = Encoding.ASCII.GetString(dlisStream.ReadBytes(80));
            return new StorageUnitLabel
            {
                StorageUnitSequenceNumber = label[0..4],
                DlisVersion = label[4..9],
                StorageUnitStructure = label[9..15],
                MaximumRecordLength = label[15..20],
                StorageSetIdentifier = label[20..80]
            };
        }
    }
}
