using WellLog.Lib.Helpers;

namespace WellLog.Lib.Models.DLIS
{
    public class ComponentDescriptor
    {
        public const byte ABSENT_ATTRIBUTE_ROLE = 0b_0000_0000;
        public const byte ATTRIBUTE_ROLE = 0b_0010_0000;
        public const byte INVARIANT_ATTRIBUTE_ROLE = 0b_0100_0000;
        public const byte OBJECT_ROLE = 0b_0110_0000;
        public const byte REDUNDANT_SET_ROLE = 0b_1010_0000;
        public const byte REPLACEMENT_SET_ROLE = 0b_1100_0000;
        public const byte SET_ROLE = 0b_1110_0000;

        public const byte SET_TYPE_MASK = 0b_0001_0000;
        public const byte SET_NAME_MASK = 0b_0000_1000;

        public const byte OBJ_NAME_MASK = 0b_0001_0000;

        public const byte ATTR_LABEL_MASK = 0b_0001_0000;
        public const byte ATTR_COUNT_MASK = 0b_0001_0000;
        public const byte ATTR_REPRESENTATION_CODE_MASK = 0b_0001_0000;
        public const byte ATTR_UNITS_MASK = 0b_0001_0000;
        public const byte ATTR_VALUE_MASK = 0b_0001_0000;

        private readonly byte _descriptor;

        public bool IsAbsentAttribute => _descriptor.IsComponentRole(ABSENT_ATTRIBUTE_ROLE);
        public bool IsAttribute => _descriptor.IsComponentRole(ATTRIBUTE_ROLE);
        public bool IsInvariantAttribute => _descriptor.IsComponentRole(INVARIANT_ATTRIBUTE_ROLE);
        public bool IsObject => _descriptor.IsComponentRole(OBJECT_ROLE);
        public bool IsRedundantSet => _descriptor.IsComponentRole(REDUNDANT_SET_ROLE);
        public bool IsReplacementSet => _descriptor.IsComponentRole(REPLACEMENT_SET_ROLE);
        public bool IsSet => _descriptor.IsComponentRole(SET_ROLE);

        public bool SetHasType => (IsSet || IsRedundantSet || IsReplacementSet) ? _descriptor.GetBitUsingMask(SET_TYPE_MASK) : false;
        public bool SetHasName => (IsSet || IsRedundantSet || IsReplacementSet) ? _descriptor.GetBitUsingMask(SET_NAME_MASK) : false;
        public bool ObjectHasName => IsObject ? _descriptor.GetBitUsingMask(OBJ_NAME_MASK) : false;
        public bool AttributeHasLabel => (IsAttribute || IsInvariantAttribute) ? _descriptor.GetBitUsingMask(ATTR_LABEL_MASK) : false;
        public bool AttributeHasCount => (IsAttribute || IsInvariantAttribute) ? _descriptor.GetBitUsingMask(ATTR_COUNT_MASK) : false;
        public bool AttributeHasRepresentationCode => (IsAttribute || IsInvariantAttribute) ? _descriptor.GetBitUsingMask(ATTR_REPRESENTATION_CODE_MASK) : false;
        public bool AttributeHasUnits => (IsAttribute || IsInvariantAttribute) ? _descriptor.GetBitUsingMask(ATTR_UNITS_MASK) : false;
        public bool AttributeHasValue => (IsAttribute || IsInvariantAttribute) ? _descriptor.GetBitUsingMask(ATTR_VALUE_MASK) : false;

        public ComponentDescriptor(byte descriptor)
        {
            _descriptor = descriptor;
        }
    }
}
