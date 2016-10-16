using System;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAsyncTest
{
    public partial class Default : System.Web.UI.Page
    {
        private string Key = "TestContextKey";

        private TestEntity entity = new TestEntity()
        {
            Age = 18,
            Name = "小薇"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Items[Key] = entity;

            var result = GetResult().Result;

            WriteLine("ManagedThreadId:" + Thread.CurrentThread.ManagedThreadId);
            WriteLine("HttpContext entity:" + (HttpContext.Current == null ? string.Empty : HttpContext.Current.Items[Key]));
            WriteLine("");
        }

        private async Task<string> GetResult()
        {
            HttpClient client = new HttpClient(new LogHandler());

            WriteLine("ManagedThreadId:" + Thread.CurrentThread.ManagedThreadId);
            WriteLine("HttpContext entity:" + (HttpContext.Current == null ? string.Empty : HttpContext.Current.Items[Key]));
            WriteLine("");
            
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://www.baidu.com")).ConfigureAwait(false);

            WriteLine("ManagedThreadId:" + Thread.CurrentThread.ManagedThreadId);
            WriteLine("HttpContext entity:" + (HttpContext.Current == null ? string.Empty : HttpContext.Current.Items[Key]));
            WriteLine("");

            return await response.Content.ReadAsStringAsync();
        }

        private void WriteLine(string msg)
        {
            Response.Write(msg + "<br />");
        }
    }

    public class TestEntity
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name + Age.ToString();
        }
    }

    public class LogHandler : MessageProcessingHandler
    {
        private string Key = "TestContextKey";

        public LogHandler()
        {
            this.InnerHandler = new HttpClientHandler();
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var entity = new TestEntity()
            {
                Age = 1811,
                Name = "小薇11"
            };
            HttpContext.Current.Response.Write("ProcessRequest entity is :'" + HttpContext.Current.Items[Key] + "'<br />");
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.Response.Write("log request is in<br />");
            //    HttpContext.Current.Items[Key] = entity;
            //}

            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var entity = new TestEntity()
            {
                Age = 1811,
                Name = "小薇11"
            };

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Write("log response is in<br />");
                HttpContext.Current.Items[Key] = entity;
            }
            CallContext.SetData(Key, entity);
            CallContext.LogicalSetData(Key, entity);

            return response;
        }
    }
}