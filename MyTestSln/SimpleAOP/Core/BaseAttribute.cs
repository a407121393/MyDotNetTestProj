using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAOP.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseAttribute : Attribute
    {
        public abstract Type CallHandlerType
        {
            get;
            protected set;
        }
    }
}
