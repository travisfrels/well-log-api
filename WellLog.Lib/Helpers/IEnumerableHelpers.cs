using System.Collections;

namespace WellLog.Lib.Helpers
{
    public static class IEnumerableHelpers
    {
        public static object First(this IEnumerable i)
        {
            if (i == null) { return null; }
            var e = i.GetEnumerator();
            e.Reset();
            e.MoveNext();
            return e.Current;
        }
    }
}
