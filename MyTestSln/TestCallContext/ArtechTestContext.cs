using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TestCallContext
{
    class ArtechTestContext
    {
        static void Main(string[] args)
        {
            LogicalContextItem<string> userName = new LogicalContextItem<string>("__userName", "Foo");

            CallContext.LogicalSetData(userName.Key, userName);

            Console.WriteLine(((LogicalContextItem<string>)CallContext.LogicalGetData("__userName")).Value == "Foo");

            Console.Read();
        }
    }

    [Serializable]
    public class LogicalContextItem<TValue>
    {
        public string Key { get; private set; }
        public TValue Value { get; set; }


        public LogicalContextItem(string key, TValue value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            this.Key = key;
            this.Value = value;
        }
    }
}
