using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVirt
{
    class Program
    {
        static void Main(string[] args)
        {
            A c1 = new C();
            c1.Foo();

            C c2 = new C();
            c2.Foo();

            Console.ReadLine();
        }
    }

    class A
    {
        public virtual void Foo()
        {
            Console.WriteLine("Call on A.Foo()");
        }
    }

    class B : A
    {
        public override void Foo()
        {
            Console.WriteLine("Call on B.Foo() ");
        }
    }

    class C : B
    {
        public new void Foo()
        {
            Console.WriteLine("Call on C.Foo()");
        }
    }
}
