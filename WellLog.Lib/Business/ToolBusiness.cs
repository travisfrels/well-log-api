using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ToolBusiness : IToolBusiness
    {
        public const string TOOL_TYPE = "TOOL";
        public const string DESCRIPTION_LABEL = "DESCRIPTION";
        public const string TRADEMARK_NAME_LABEL = "TRADEMARK-NAME";
        public const string GENERIC_NAME_LABEL = "GENERIC-NAME";
        public const string PARTS_LABEL = "PARTS";
        public const string STATUS_LABEL = "STATUS";
        public const string CHANNELS_LABEL = "CHANNELS";
        public const string PARAMETERS_LABEL = "PARAMETERS";

        private readonly IObjectComponentBusiness _objectComponentBusiness;

        public ToolBusiness(IObjectComponentBusiness objectComponentBusiness)
        {
            _objectComponentBusiness = objectComponentBusiness;
        }

        public bool IsTool(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null || eflr.Set == null) { return false; }
            return string.Compare(eflr.Set.Type, TOOL_TYPE, true) == 0;
        }

        public IEnumerable<ExplicitlyFormattedLogicalRecord> GetToolEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.Where(x => IsTool(x));
        }

        public IEnumerable<Tool> ConvertEFLRtoTools(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsTool(eflr)) { return null; }

            var objects = eflr.Objects.ToArray();
            var numParameters = objects.Length;
            var tools = new Tool[numParameters];
            for (var i = 0; i < numParameters; i++)
            {
                var obj = objects[i];
                tools[i] = new Tool
                {
                    Description = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, DESCRIPTION_LABEL),
                    TrademarkName = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, TRADEMARK_NAME_LABEL),
                    GenericName = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, GENERIC_NAME_LABEL),
                    Parts = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, PARTS_LABEL),
                    Status = (string)_objectComponentBusiness.GetFirstAttributeValueByLabel(obj, STATUS_LABEL),
                    Channels = (OBNAME[])_objectComponentBusiness.GetAttributeValueByLabel(obj, CHANNELS_LABEL),
                    Parameters = (OBNAME[])_objectComponentBusiness.GetAttributeValueByLabel(obj, PARAMETERS_LABEL)
                };
            }

            //var nonNullValues = parameters.Where(x => x.Values.Count() > 1).ToArray();
            return tools;
        }
    }
}
