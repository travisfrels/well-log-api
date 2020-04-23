using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Helpers;
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

        private readonly IObjectComponentBusiness _objectComponentBusiness;

        public ParameterBusiness(IObjectComponentBusiness objectComponentBusiness)
        {
            _objectComponentBusiness = objectComponentBusiness;
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

        public IEnumerable<Parameter> ConvertEFLRtoParameters(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsParameter(eflr)) { return null; }

            var objects = eflr.Objects.ToArray();
            var numParameters = objects.Length;
            var parameters = new Parameter[numParameters];
            for (var i = 0; i < numParameters; i++)
            {
                var obj = objects[i];
                parameters[i] = new Parameter
                {
                    LongName = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, LONG_NAME_LABEL),
                    Dimension = (uint?)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, DIMENSION_LABEL),
                    Zones = (OBNAME)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, ZONES_LABEL),
                    Values = _objectComponentBusiness.GetAttributeValueByLabel(obj, VALUES_LABEL)
                };
            }

            //var nonNullValues = parameters.Where(x => x.Values.Count() > 1).ToArray();
            return parameters;
        }
    }
}
