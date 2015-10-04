using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Halloween.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Ref. https://trikks.wordpress.com/2012/12/31/mapping-dictionary-to-typed-object-using-c/
        /// </summary>
        public static object ToObject(this IDictionary<string, string> dict, Type type)
        {
            var t = Activator.CreateInstance(type);
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // Find which property type (int, string, double? etc) the CURRENT property is...
                Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

                // Fix nullables...
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                // ...and change the type
                object newA = Convert.ChangeType(item.Value, newT);
                t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
            }
            return t;
        }
    }
}
