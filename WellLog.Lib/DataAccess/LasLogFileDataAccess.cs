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

        public void Write(string fileName, LasLog lasLog)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }
            if (lasLog == null) { throw new ArgumentNullException(nameof(lasLog)); }
            using var lasFile = File.Open(fileName, FileMode.Create, FileAccess.Write);
            _lasLogBusiness.WriteStream(lasFile, lasLog);
        }
    }
}
