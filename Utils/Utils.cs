using System;
using System.Collections.Generic;

namespace LogMonitor.Utils
{
    public static class Utils
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, Func<TKey, TValue , TValue> updateValueFactory)
        {
            if(dictionary.ContainsKey(key))
            {
                dictionary[key] = updateValueFactory(key, dictionary[key]);
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
