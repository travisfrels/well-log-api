using System;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public class DlisLogFileDataAccess : IDlisLogFileDataAccess
    {
        private readonly IStorageUnitBusiness _storageUnitBusiness;

        public DlisLogFileDataAccess(IStorageUnitBusiness storageUnitBusiness)
        {
            _storageUnitBusiness = storageUnitBusiness;
        }

        public StorageUnit Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }
            using var dlisFile = File.Open(fileName, FileMode.Open, FileAccess.Read);
            return _storageUnitBusiness.ReadStorageUnit(dlisFile);
        }
    }
}
