using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestString
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string s1 = "abc";
        //    Console.WriteLine(string.IsInterned(s1) ?? null);

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    string s1 = "ab";
        //    s1 += "c";
        //    Console.WriteLine(string.IsInterned(s1) ?? null);

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    string s1 = "abc";
        //    string s2 = "ab";
        //    s2 += "c";

        //    string s3 = "ab";
        //    Console.WriteLine(string.IsInterned(s1) ?? null);
        //    Console.WriteLine(string.IsInterned(s2) ?? null);
        //    Console.WriteLine(string.IsInterned(s3) ?? null);

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    string s1 = "abc";
        //    string s2 = "ab";

        //    string s3 = s2 + "c";

        //    Console.WriteLine(string.IsInterned(s1) ?? null);
        //    Console.WriteLine(string.IsInterned(s2) ?? null);
        //    Console.WriteLine(string.IsInterned(s3) ?? null);

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    string s2 = "ab";
        //    s2 += "c";
            
        //    Console.WriteLine(string.IsInterned(s2) ?? null);

        //    string s1 = "abc";

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    string s2 = "ab";
        //    s2 += "c";

        //    Console.WriteLine(string.IsInterned(s2) ?? null);

        //    string s1 = getstr();

        //    Console.ReadKey();
        //}

        //private static string getstr()
        //{
        //    return "abc";
        //}

        //private const string s = "abc";
        //static void Main(string[] args)
        //{
        //    string s2 = "ab";
        //    s2 += "c";

        //    Console.WriteLine(string.IsInterned(s2) ?? null);

        //    Console.ReadKey();
        //}

        private static string s = "abc";
        static void Main(string[] args)
        {
            string s2 = "ab";
            s2 += "c";

            Console.WriteLine(string.IsInterned(s2) ?? null);

            Console.ReadKey();
        }
    }
}
