using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class AnyInterceptorAttribute : Attribute
    {
        public string InterceptorName { get; set; }

        public AnyInterceptorAttribute(string interceptorName)
        {
            this.InterceptorName = interceptorName;
        }
    }
}