using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace TestStringBuilderCache
{
    class Program
    {
        static void Main(string[] args)
        {
            int iteration = 100000;

            CodeTimer.Time("StringBuilder Spend:", iteration, () =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.ToString();
            });
            
            CodeTimer.Time("StringBuilderCache Spend:", iteration, () =>
            {
                StringBuilder sb = StringBuilderCache.Acquire();
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                sb.Append("Hello World");
                StringBuilderCache.GetStringAndRelease(sb);
            });

            Console.ReadKey();
        }
    }
}
