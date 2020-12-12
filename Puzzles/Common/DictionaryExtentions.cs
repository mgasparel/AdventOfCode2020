using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Common
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue> factory)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, factory(key));
            }

            return dictionary[key];
        }
    }
}
