using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class FileHeaderLogicalRecordBusiness : IFileHeaderLogicalRecordBusiness
    {
        public const string FILE_HEADER_TYPE = "FILE-HEADER";
        public const string SEQUENCE_NUMBER_LABEL = "SEQUENCE-NUMBER";
        public const string ID_LABEL = "ID";

        private readonly IExplicitlyFormattedLogicalRecordBusiness _eflrBusiness;

        public FileHeaderLogicalRecordBusiness(IExplicitlyFormattedLogicalRecordBusiness eflrBusiness)
        {
            _eflrBusiness = eflrBusiness;
        }

        public bool IsFileHeader(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null || eflr.Set == null) { return false; }
            return string.Compare(eflr.Set.Type, FILE_HEADER_TYPE, true) == 0;
        }

        public ExplicitlyFormattedLogicalRecord GetFileHeaderEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.FirstOrDefault(x => IsFileHeader(x));
        }

        public FileHeaderLogicalRecord ConvertEFLRtoFileHeader(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsFileHeader(eflr)) { return null; }

            return new FileHeaderLogicalRecord
            {
                SequenceNumber = _eflrBusiness.GetFirstStringByLabel(eflr, SEQUENCE_NUMBER_LABEL),
                ID = _eflrBusiness.GetFirstStringByLabel(eflr, ID_LABEL)
            };
        }
    }
}
