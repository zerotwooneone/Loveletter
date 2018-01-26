using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Extensions
{
    public static class Extensions
    {
        public static T RemoveLast<T>(this IList<T> list)
        {
            var removed = list.Last();
            list.RemoveAt(list.Count-1);
            return removed;
        }
    }
}
