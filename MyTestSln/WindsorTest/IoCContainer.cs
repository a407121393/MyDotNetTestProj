using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    public class IoCContainer
    {
        public static readonly IWindsorContainer Instance = new WindsorContainer("Windsor.xml");
    }
}