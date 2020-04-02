using System;
using System.IO;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class StorageSetBusiness : IStorageSetBusiness
    {
        private readonly IStorageUnitBusiness _storageUnitBusiness;

        public StorageSetBusiness(IStorageUnitBusiness storageUnitBusiness)
        {
            _storageUnitBusiness = storageUnitBusiness;
        }

        public StorageSet ReadStream(Stream dlisStream)
        {
            if (dlisStream == null) { throw new ArgumentNullException(nameof(dlisStream)); }

            /* ignore beginning of tape and tape marks */
            dlisStream.Seek(12, SeekOrigin.Begin);

            return new StorageSet
            {
                StorageUnits = new StorageUnit[]
                {
                    _storageUnitBusiness.ReadStorageUnit(dlisStream)
                }
            };
        }
    }
}
