using System.Collections.Generic;
using System.Linq;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ChannelBusiness : IChannelBusiness
    {
        public const string CHANNEL_TYPE = "CHANNEL";
        public const string LONG_NAME_LABEL = "LONG-NAME";
        public const string PROPERTIES_LABEL = "PROPERTIES";
        public const string REPRESENTATION_CODE_LABEL = "REPRESENTATION-CODE";
        public const string UNITS_LABEL = "UNITS";
        public const string DIMENSION_LABEL = "DIMENSION";
        public const string AXIS_LABEL = "AXIS";
        public const string ELEMENT_LIMIT_LABEL = "ELEMENT-LIMIT";
        public const string SOURCE_LABEL = "SOURCE";

        public bool IsChannel(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null || eflr.Set == null) { return false; }
            return string.Compare(eflr.Set.Type, CHANNEL_TYPE, true) == 0;
        }

        public IEnumerable<ExplicitlyFormattedLogicalRecord> GetParameterEFLR(IEnumerable<ExplicitlyFormattedLogicalRecord> eflrs)
        {
            if (eflrs == null) { return null; }
            return eflrs.Where(x => IsChannel(x));
        }

        public IEnumerable<Channel> ConvertEFLRtoChannels(ExplicitlyFormattedLogicalRecord eflr)
        {
            if (eflr == null) { return null; }
            if (eflr.Set == null || eflr.Template == null || eflr.Objects == null) { return null; }

            if (!IsChannel(eflr)) { return null; }

            var objects = eflr.Objects.ToArray();
            var numParameters = objects.Length;
            var channels = new Channel[numParameters];
            for (var i = 0; i < numParameters; i++)
            {
                var obj = objects[i];
                channels[i] = new Channel
                {
                    LongName = (string)obj.Attributes.Where(x => string.Compare(x.Label, LONG_NAME_LABEL, true) == 0).First().Value.First(),
                    Properties = (string)obj.Attributes.Where(x => string.Compare(x.Label, PROPERTIES_LABEL, true) == 0).First().Value.First(),
                    RepresentationCode = (byte?)obj.Attributes.Where(x => string.Compare(x.Label, REPRESENTATION_CODE_LABEL, true) == 0).First().Value.First(),
                    Units = (string)obj.Attributes.Where(x => string.Compare(x.Label, UNITS_LABEL, true) == 0).First().Value.First(),
                    Dimension = (uint?)obj.Attributes.Where(x => string.Compare(x.Label, DIMENSION_LABEL, true) == 0).First().Value.First(),
                    ElementLimit = (uint?)obj.Attributes.Where(x => string.Compare(x.Label, ELEMENT_LIMIT_LABEL, true) == 0).First().Value.First(),
                    Source = (OBJREF)obj.Attributes.Where(x => string.Compare(x.Label, SOURCE_LABEL, true) == 0).First().Value
                };
            }

            //var nonNullValues = parameters.Where(x => x.Values.Count() > 1).ToArray();
            return channels;
        }
    }
}
