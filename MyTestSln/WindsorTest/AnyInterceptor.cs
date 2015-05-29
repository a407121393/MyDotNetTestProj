using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace WindsorTest
{
    public abstract class AnyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            RunBegin(invocation.Arguments);

            invocation.Proceed();

            RunAfter(invocation.Arguments);
        }

        public abstract void RunAfter(object[] p);

        public abstract void RunBegin(object[] p);
    }
}