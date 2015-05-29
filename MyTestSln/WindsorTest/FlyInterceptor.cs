using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    public class FlyInterceptor : AnyInterceptor
    {
        public override void RunAfter(object[] p)
        {
            Console.WriteLine("RunAfter");
        }

        public override void RunBegin(object[] p)
        {
            Console.WriteLine("RunBegin");
        }
    }
}