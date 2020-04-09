using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class ComponentHelpers
    {
        public static bool IsFileHeaderLogicalRecord(this Component component)
        {
            if (component == null) { return false; }
            if (component.Descriptor == null) { return false; }
            if (!component.Descriptor.IsSet) { return false; }

            var setComponent = (SetComponent)component;
            if (string.Compare(setComponent.Type, "FILE-HEADER", true) == 0) { return true; }
            return false;
        }
    }
}
