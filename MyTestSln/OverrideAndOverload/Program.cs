using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverrideAndOverload
{
    class Program
    {
        static void Main(string[] args)
        {
            A a=new A();
            a.Test();

            B b=new B();
            b.Test();
            a = b;
            a.Test();

            Console.ReadKey();
        }
    }

    class A
    {
        public virtual void Test()
        {
            Console.WriteLine("A Base CW.");
        }
    }

    class B:A
    {
        public new void Test()
        {
            Console.WriteLine("B Base CW.");
        }
    }
}
