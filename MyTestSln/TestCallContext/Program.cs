using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TestCallContext
{
    class Program
    {
        static void Main(string[] args)
        {
            TestIThread ttttt = new TestIThread();
            CallContext.SetData("key2", ttttt);

            CallContext.SetData("key", "SetData1111111");

            CallContext.LogicalSetData("key1", "LogicalSetData1111111111");

            Thread trd = new Thread(new ThreadStart(() =>
            {
                Console.WriteLine(CallContext.GetData("key"));

                Console.WriteLine(CallContext.LogicalGetData("key1"));


                Console.WriteLine(CallContext.GetData("key2"));
            }));

            trd.Start();


            AppDomain app = AppDomain.CreateDomain("xxxxxx");
            app.DoCallBack(GetCallContext);
            AppDomain.Unload(app);

            Console.Read();
        }

        private static void GetCallContext()
        {
            Console.WriteLine(CallContext.GetData("key") + "-----AppDomain---xxxxxx");

            Console.WriteLine(CallContext.LogicalGetData("key1") + "-----AppDomain---xxxxxx");


            Console.WriteLine(CallContext.GetData("key2") + "-----AppDomain---xxxxxx");

            CallContext.FreeNamedDataSlot("key2");
            Console.WriteLine(CallContext.GetData("key2") + "-----AppDomain---xxxxxx");

            //HttpContext.Current.Request = null;
        }
    }

    [Serializable]
    public class TestIThread : ILogicalThreadAffinative
    {
        public override string ToString()
        {
            return "TestIThread-------------ILogicalThreadAffinative";
        }
    }
}
