using System;
using System.Collections.Generic;
using System.Linq;

namespace Halloween.Extensions
{
    public static class IEnumerableExtensions
    {
        private static Random randomizer = new Random((int)DateTimeOffset.UtcNow.UtcTicks);

        public static T GetRandomElement<T>(this IEnumerable<T> value)
        {
            return value.ElementAt(randomizer.Next(value.Count()));
        }
    }
}
