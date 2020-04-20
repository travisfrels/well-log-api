using System.Collections;
using System.Collections.Generic;

namespace WellLog.Lib.Helpers
{
    public static class IEnumerableHelpers
    {
        public static object First(this IEnumerable i)
        {
            if (i == null) { return null; }

            var e = i.GetEnumerator();
            if (e == null) { return null; }

            e.Reset();
            e.MoveNext();
            return e.Current;
        }

        public static int Count(this IEnumerable i)
        {
            if (i == null) { return 0; }

            var count = 0;
            foreach (var x in i) { count++; }
            return count;
        }

        public static IEnumerable<string> SerializeValues(this IEnumerable i)
        {
            if (i == null) { yield break; }
            foreach(var x in i)
            {
                if (x == null) { yield return string.Empty; }
                yield return x.ToString();
            }
        }
    }
}
