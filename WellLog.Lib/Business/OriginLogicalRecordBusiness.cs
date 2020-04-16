using System;
using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class OriginLogicalRecordBusiness : IOriginLogicalRecordBusiness
    {
        public const string ORIGIN_TYPE = "ORIGIN";
        public const string WELL_REFERENCE_POINT_TYPE = "WELL-REFERENCE-POINT";

        public const string FILE_ID_LABEL = "FILE-ID";
        public const string FILE_SET_NAME_LABEL = "FILE-SET-NAME";
        public const string FILE_SET_NUMBER_LABEL = "FILE-SET-NUMBER";
        public const string FILE_NUMBER_LABEL = "FILE-NUMBER";
        public const string FILE_TYPE_LABEL = "FILE-TYPE";
        public const string PRODUCT_LABEL = "PRODUCT";
        public const string VERSION_LABEL = "VERSION";
        public const string PROGRAMS_LABEL = "PROGRAMS";
        public const string CREATION_TIME_LABEL = "CREATION-TIME";
        public const string ORDER_NUMBER_LABEL = "ORDER-NUMBER";
        public const string DESCENT_NUMBER_LABEL = "DESCENT-NUMBER";
        public const string RUN_NUMBER_LABEL = "RUN-NUMBER";
        public const string WELL_ID_LABEL = "WELL-ID";
        public const string WELL_NAME_LABEL = "WELL-NAME";
        public const string FIELD_NAME_LABEL = "FIELD-NAME";
        public const string PRODUCER_CODE_LABEL = "PRODUCER-CODE";
        public const string PRODUCER_NAME_LABEL = "PRODUCER-NAME";
        public const string COMPANY_LABEL = "COMPANY";
        public const string NAME_SPACE_NAME_LABEL = "NAME-SPACE-NAME";
        public const string NAMEA_SPACE_VERSION_LABEL = "NAME-SPACE-VERSION";

        private readonly IExplicitlyFormattedLogicalRecordBusiness _eflrBusiness;

        public OriginLogicalRecordBusiness(IExplicitlyFormattedLogicalRecordBusiness eflrBusiness)
        {
            _eflrBusiness = eflrBusiness;
        }

        public bool IsOrigin(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null || eflr.Set == null) { return false; }
            return string.Compare(eflr.Set.Type, ORIGIN_TYPE, true) == 0 || string.Compare(eflr.Set.Type, WELL_REFERENCE_POINT_TYPE, true) == 0;
        }

        public IEnumerable<ExplicitlyFormattedLogicalRecord> GetOriginEFLRs(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.Where(x => IsOrigin(x));
        }

        public OriginLogicalRecord ConvertEFLRtoOrigin(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsOrigin(eflr)) { return null; }

            return new OriginLogicalRecord
            {
                FileID = _eflrBusiness.GetFirstStringByLabel(eflr, FILE_ID_LABEL),
                FileSetName = _eflrBusiness.GetFirstStringByLabel(eflr, FILE_SET_NAME_LABEL),
                FileSetNumber = (uint?)_eflrBusiness.GetFirstValueByLabel(eflr, FILE_SET_NUMBER_LABEL),
                FileNumber = (uint?)_eflrBusiness.GetFirstValueByLabel(eflr, FILE_NUMBER_LABEL),
                FileType = _eflrBusiness.GetFirstStringByLabel(eflr, FILE_TYPE_LABEL),
                Product = _eflrBusiness.GetFirstStringByLabel(eflr, PRODUCT_LABEL),
                Version = _eflrBusiness.GetFirstStringByLabel(eflr, VERSION_LABEL),
                Programs = _eflrBusiness.GetFirstStringByLabel(eflr, PROGRAMS_LABEL),
                CreationTime = (DateTime?)_eflrBusiness.GetFirstValueByLabel(eflr, CREATION_TIME_LABEL),
                OrderNumber = _eflrBusiness.GetFirstStringByLabel(eflr, ORDER_NUMBER_LABEL),
                DescentNumber = _eflrBusiness.GetFirstValueByLabel(eflr, DESCENT_NUMBER_LABEL),
                RunNumber = _eflrBusiness.GetFirstValueByLabel(eflr, RUN_NUMBER_LABEL),
                WellName = _eflrBusiness.GetFirstStringByLabel(eflr, WELL_NAME_LABEL),
                FieldName = _eflrBusiness.GetFirstStringByLabel(eflr, FIELD_NAME_LABEL),
                ProducerCode = (ushort?)_eflrBusiness.GetFirstValueByLabel(eflr, PRODUCER_CODE_LABEL),
                ProducerName = _eflrBusiness.GetFirstStringByLabel(eflr, PRODUCER_NAME_LABEL),
                Company = _eflrBusiness.GetFirstStringByLabel(eflr, COMPANY_LABEL),
                NameSpaceName = _eflrBusiness.GetFirstStringByLabel(eflr, NAME_SPACE_NAME_LABEL),
                NameSpaceVersion = (uint?)_eflrBusiness.GetFirstValueByLabel(eflr, NAMEA_SPACE_VERSION_LABEL)
            };
        }
    }
}
