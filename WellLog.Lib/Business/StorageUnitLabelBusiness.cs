using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class StorageUnitLabelBusiness : IStorageUnitLabelBusiness
    {
        public StorageUnitLabel ReadStorageUnitLabel(Stream dlisStream)
        {
            if (dlisStream == null) { return null; }
            var buffer = new byte[80];
            dlisStream.Read(buffer, 0, 80);
            return new StorageUnitLabel(Encoding.ASCII.GetString(buffer));
        }
    }
}
