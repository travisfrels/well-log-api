using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Business;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.DataAccess
{
    public class DlisLogFileDataAccess : IDlisLogFileDataAccess
    {
        private readonly IStorageUnitBusiness _storageUnitBusiness;
        private readonly IExplicitlyFormattedLogicalRecordBusiness _eflrBusiness;
        private readonly IFileHeaderLogicalRecordBusiness _fhlrBusiness;
        private readonly IOriginLogicalRecordBusiness _olrBusiness;
        private readonly IParameterBusiness _paramBusiness;
        private readonly IChannelBusiness _channelBusiness;
        private readonly IToolBusiness _toolBusiness;

        public DlisLogFileDataAccess
        (
            IStorageUnitBusiness storageUnitBusiness,
            IExplicitlyFormattedLogicalRecordBusiness eflrBusiness,
            IFileHeaderLogicalRecordBusiness fhlrBusiness,
            IOriginLogicalRecordBusiness olrBusiness,
            IParameterBusiness paramBusiness,
            IChannelBusiness channelBusiness,
            IToolBusiness toolBusiness
        )
        {
            _storageUnitBusiness = storageUnitBusiness;
            _eflrBusiness = eflrBusiness;
            _fhlrBusiness = fhlrBusiness;
            _olrBusiness = olrBusiness;
            _paramBusiness = paramBusiness;
            _channelBusiness = channelBusiness;
            _toolBusiness = toolBusiness;
        }

        public DlisLog Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException(nameof(fileName)); }

            StorageUnit storageUnit;
            using (var dlisFile = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                storageUnit = _storageUnitBusiness.ReadStorageUnit(dlisFile);
            }

            var eflrData = storageUnit
                .VisibleRecords
                .SelectMany(x => x.Segments.Where(x => x.Header.LogicalRecordStructure))
                .SelectMany(x => x.Body)
                .ToArray();

            var eflrRecords = new List<ExplicitlyFormattedLogicalRecord>();
            using (var eflrStream = new MemoryStream(eflrData))
            {
                while (!eflrStream.IsAtEndOfStream())
                {
                    var eflr = _eflrBusiness.ReadExplicitlyFormattedLogicalRecord(eflrStream);
                    if (eflr != null) { eflrRecords.Add(eflr); }
                }
            }

            return new DlisLog
            {
                Label = storageUnit.Label,
                EFLRs = eflrRecords.ToArray(),
                FileHeader = _fhlrBusiness.ConvertEFLRtoFileHeader(_fhlrBusiness.GetFileHeaderEFLR(eflrRecords)),
                Origins = _olrBusiness.GetOriginEFLRs(eflrRecords).Select(x => _olrBusiness.ConvertEFLRtoOrigin(x)),
                Parameters = _paramBusiness.GetParameterEFLR(eflrRecords).SelectMany(x => _paramBusiness.ConvertEFLRtoParameters(x)),
                Channels = _channelBusiness.GetChannelEFLR(eflrRecords).SelectMany(x => _channelBusiness.ConvertEFLRtoChannels(x)),
                Tools = _toolBusiness.GetToolEFLR(eflrRecords).SelectMany(x => _toolBusiness.ConvertEFLRtoTools(x))
            };
        }
    }
}
