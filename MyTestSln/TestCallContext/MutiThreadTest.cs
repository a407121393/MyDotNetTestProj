using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace TestCallContext
{
    class MutiThreadTest
    {
        static void Main(string[] args)
        {
            CallContext.LogicalSetData("key", "1");

            Thread trd = new Thread(new ThreadStart(() =>
            {

                Console.WriteLine("first thread : " + CallContext.LogicalGetData("key"));

                Thread.Sleep(1000);

                Console.WriteLine("first thread : " + CallContext.LogicalGetData("key"));

                Thread.Sleep(1000);
                CallContext.LogicalSetData("key", "3");
                Console.WriteLine("first thread : " + CallContext.LogicalGetData("key"));

            }));
            trd.Start();
            Thread.Sleep(100);

            CallContext.LogicalSetData("key", "2");


            Thread trd1 = new Thread(new ThreadStart(() =>
            {

                Console.WriteLine("second thread : " + CallContext.LogicalGetData("key"));

                Thread.Sleep(5000);

                Console.WriteLine("second thread : " + CallContext.LogicalGetData("key"));

            }));
            trd1.Start();

            Thread.Sleep(10000);

            Console.WriteLine("main thread : " + CallContext.LogicalGetData("key"));


            Console.Read();
        }
    }
}
