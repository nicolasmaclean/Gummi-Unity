using System;
using System.Collections.Generic;

namespace Gummi
{
    public static class GenericExtensions
    {
        public static bool IsNull<T>(this T val)
        {
            var type = typeof(T);

            bool isNullable = type.IsClass || Nullable.GetUnderlyingType(type) != null; 
            return isNullable && EqualityComparer<T>.Default.Equals(val, default(T));
        }
    }
}