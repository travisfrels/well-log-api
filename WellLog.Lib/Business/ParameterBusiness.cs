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

        public IEnumerable<Parameter> ConvertEFLRtoParameter(ExplicitlyFormattedLogicalRecord eflr)
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
                    LongName = (string)obj.Attributes.Where(x => string.Compare(x.Label, LONG_NAME_LABEL, true) == 0).First().Value.First(),
                    Dimension = (uint?)obj.Attributes.Where(x => string.Compare(x.Label, DIMENSION_LABEL, true) == 0).First().Value.First(),
                    Zones = (OBNAME)obj.Attributes.Where(x => string.Compare(x.Label, ZONES_LABEL, true) == 0).First().Value.First(),
                    Values = obj.Attributes.Where(x => string.Compare(x.Label, VALUES_LABEL, true) == 0).First().Value
                };
            }

            //var nonNullValues = parameters.Where(x => x.Values.Count() > 1).ToArray();
            return parameters;
        }
    }
}
