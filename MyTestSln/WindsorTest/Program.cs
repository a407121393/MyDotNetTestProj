using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IoCContainer.Instance.Resolve<IFly>().Fly();

            Console.Read();
        }
    }
}