using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class StringBuilderCache
    {
        [ThreadStatic]
        private static StringBuilder CachedInstance;
        private const int MAX_BUILDER_SIZE = 360;

        public static StringBuilder Acquire(int capacity = 16)
        {
            if (capacity <= 360)
            {
                StringBuilder stringBuilder = StringBuilderCache.CachedInstance;
                if (stringBuilder != null && capacity <= stringBuilder.Capacity)
                {
                    StringBuilderCache.CachedInstance = (StringBuilder)null;
                    stringBuilder.Clear();
                    return stringBuilder;
                }
            }
            return new StringBuilder(capacity);
        }

        public static void Release(StringBuilder sb)
        {
            if (sb.Capacity > 360)
                return;
            StringBuilderCache.CachedInstance = sb;
        }

        public static string GetStringAndRelease(StringBuilder sb)
        {
            string str = ((object)sb).ToString();
            StringBuilderCache.Release(sb);
            return str;
        }
    }
}
