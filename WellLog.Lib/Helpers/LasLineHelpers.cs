namespace WellLog.Lib.Helpers
{
    public static class LasLineHelpers
    {
        public static bool IsLasComment(this string lasLine)
        {
            if (string.IsNullOrEmpty(lasLine)) { return false; }
            return lasLine.TrimStart().StartsWith('#');
        }

        public static bool IsLasSectionHeader(this string lasLine)
        {
            if (string.IsNullOrEmpty(lasLine)) { return false; }
            return lasLine.TrimStart().StartsWith('~');
        }
    }
}
