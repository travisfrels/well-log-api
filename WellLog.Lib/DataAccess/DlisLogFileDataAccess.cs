using System;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public class DlisLogFileDataAccess : IDlisLogFileDataAccess
    {
        private readonly IStorageSetBusiness _storageSetBusiness;

        public DlisLogFileDataAccess(IStorageSetBusiness storageSetBusiness)
        {
            _storageSetBusiness = storageSetBusiness;
        }

        public StorageSet Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }
            using var dlisFile = File.Open(fileName, FileMode.Open, FileAccess.Read);
            return _storageSetBusiness.ReadStream(dlisFile);
        }
    }
}
