using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class ComponentHelpers
    {
        public static bool IsFileHeaderLogicalRecord(this ComponentBase component)
        {
            if (component == null) { return false; }
            if (component.Descriptor == null) { return false; }
            if (!component.Descriptor.IsSet) { return false; }

            var setComponent = (SetComponent)component;
            if (string.Compare(setComponent.Type, "FILE-HEADER", true) == 0) { return true; }
            return false;
        }

        public static bool IsOriginLogicalRecord(this ComponentBase component)
        {
            if (component == null) { return false; }
            if (component.Descriptor == null) { return false; }
            if (!component.Descriptor.IsSet) { return false; }

            var setComponent = (SetComponent)component;
            if (string.Compare(setComponent.Type, "ORIGIN", true) == 0 || string.Compare(setComponent.Type, "WELL-REFERENCE-POINT", true) == 0) { return true; }
            return false;
        }
    }
}
