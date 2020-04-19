using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ParameterBusiness : IParameterBusiness
    {
        public const string PARAMETER_TYPE = "PARAMETER";
        public const string LONG_NAME_LABEL = "LONG-NAME";
        public const string DIMENSION_LABEL = "DIMENSION";
        public const string AXIS_LABEL = "AXIS";
        public const string ZONES_LABEL = "ZONES";
        public const string VALUES_LABEL = "VALUES";

        private readonly IExplicitlyFormattedLogicalRecordBusiness _eflrBusiness;

        public ParameterBusiness(IExplicitlyFormattedLogicalRecordBusiness eflrBusiness)
        {
            _eflrBusiness = eflrBusiness;
        }

        public bool IsParameter(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null || eflr.Set == null) { return false; }
            return string.Compare(eflr.Set.Type, PARAMETER_TYPE, true) == 0;
        }

        public IEnumerable<ExplicitlyFormattedLogicalRecord> GetParameterEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.Where(x => IsParameter(x));
        }

        public Parameter ConvertEFLRtoParameter(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsParameter(eflr)) { return null; }

            return new Parameter
            {
                LongName = (OBNAME)_eflrBusiness.GetFirstValueByLabel(eflr, LONG_NAME_LABEL),
                Dimension = (uint)_eflrBusiness.GetFirstValueByLabel(eflr, LONG_NAME_LABEL),
                Axis = (OBNAME)_eflrBusiness.GetFirstValueByLabel(eflr, LONG_NAME_LABEL),
                Zones = (OBNAME)_eflrBusiness.GetFirstValueByLabel(eflr, LONG_NAME_LABEL)/*,
                Values = _eflrBusiness.GetValueByLabel(eflr, LONG_NAME_LABEL)*/
            };
        }
    }
}
