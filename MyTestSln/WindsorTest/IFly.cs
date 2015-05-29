using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    [AnyInterceptor("flyInterceptor")]
    public interface IFly
    {
        void Fly();
    }
}