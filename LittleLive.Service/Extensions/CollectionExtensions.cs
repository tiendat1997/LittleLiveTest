using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleLive.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> TakePercent<T>(this IEnumerable<T> source, double percent)
        {
            int count = (int)(source.Count() * percent / 100);
            return source.Take(count);
        }
    }
}
