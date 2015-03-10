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

        //private static string s = "abc";
        //static void Main(string[] args)
        //{
        //    string s2 = "ab";
        //    s2 += "c";

        //    Console.WriteLine(string.IsInterned(s2) ?? null);

        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    for (int i = 0; i < 10000000; i++)
        //    {
        //        createStr(i);
        //    }

        //    GC.Collect();
        //    GC.WaitForFullGCComplete();

        //    Console.WriteLine("Done................");

        //    Console.ReadKey();
        //}

        //private static void createStr(int i)
        //{
        //    string str = string.Intern(i.ToString());
        //    //string str = i.ToString();
        //}

        static void Main(string[] args)
        {
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(3.5m));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(4.5m));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(4.6m));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(3.6m));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(4.7m));
            Console.WriteLine();

            //四舍六入五取偶 
            Console.WriteLine(Math.Round(3.5m));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.5m));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.6m));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(3.6m));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.7m));
            Console.WriteLine();

            //四舍六入五取偶 
            Console.WriteLine(Math.Round(3.5));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.5));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.6));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(3.6));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(4.7));
            Console.WriteLine();

            //四舍六入五取偶
            Console.WriteLine(decimal.Round(0.125m, 2));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(0.135m, 2));
            //四舍六入五取偶 
            Console.WriteLine(decimal.Round(0.126m, 2));
            //四舍五入
            Console.WriteLine(decimal.Round(-3.105m, 2, MidpointRounding.AwayFromZero));
            //四舍五入
            Console.WriteLine(decimal.Round(-0.5m, 0, MidpointRounding.AwayFromZero));
            Console.WriteLine();

            //四舍六入五取偶 
            Console.WriteLine(Math.Round(0.125m, 2));
            //四舍六入五取偶
            Console.WriteLine(Math.Round(0.135m, 2));
            //四舍六入五取偶
            Console.WriteLine(Math.Round(0.126m, 2));
            //四舍五入
            Console.WriteLine(Math.Round(-3.105m, 2, MidpointRounding.AwayFromZero));
            //四舍五入
            Console.WriteLine(Math.Round(-0.5m, 0, MidpointRounding.AwayFromZero));
            Console.WriteLine();
            //四舍六入五取偶  
            Console.WriteLine(Math.Round(0.125, 2));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(0.135, 2));
            //四舍六入五取偶 
            Console.WriteLine(Math.Round(0.126, 2));
            //四舍五入
            Console.WriteLine(Math.Round(-3.105, 2, MidpointRounding.AwayFromZero));
            //四舍五入
            Console.WriteLine(Math.Round(-0.5, 0, MidpointRounding.AwayFromZero));
            Console.WriteLine();
        } 
    }
}