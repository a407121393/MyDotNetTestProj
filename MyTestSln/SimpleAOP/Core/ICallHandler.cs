using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAOP.Core
{
    public interface ICallHandler
    {
        object Invoke(MethodContext method);
    }
}
