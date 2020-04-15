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
                StorageUnitSequenceNumber = label[0..4].Trim(),
                DlisVersion = label[4..9].Trim(),
                StorageUnitStructure = label[9..15].Trim(),
                MaximumRecordLength = label[15..20].Trim(),
                StorageSetIdentifier = label[20..80].Trim()
            };
        }
    }
}
