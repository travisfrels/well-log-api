using System;
using System.IO;
using WellLog.Lib.Business;
using WellLog.Lib.Models;

namespace WellLog.Lib.DataAccess
{
    public class LasLogFileDataAccess : ILasLogFileDataAccess
    {
        private readonly ILasLogBusiness _lasLogBusiness;

        public LasLogFileDataAccess(ILasLogBusiness lasLogBusiness)
        {
            _lasLogBusiness = lasLogBusiness;
        }

        public LasLog Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }
            using var lasFile = File.Open(fileName, FileMode.Open, FileAccess.Read);
            return _lasLogBusiness.ReadStream(lasFile);
        }
    }
}
