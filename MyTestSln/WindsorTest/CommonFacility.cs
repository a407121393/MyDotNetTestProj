using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core;
using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindsorTest
{
    public class CommonFacility : IFacility
    {
        public void Init(IKernel kernel, Castle.Core.Configuration.IConfiguration facilityConfig)
        {
            //每一个组件注册完成都会调用这个事件处理
            kernel.ComponentRegistered += new ComponentDataDelegate(OnComponentRegistered);
        }

        private void OnComponentRegistered(string key, IHandler handler)
        {
            AnyInterceptorAttribute anyInterceptorAttribute = null;

            bool isAnyIterceptor = handler.ComponentModel.Implementation.GetInterfaces().Any(i =>
            {
                return i.GetCustomAttributes(true).Any(a =>
                {
                    if (a is AnyInterceptorAttribute)
                    {
                        anyInterceptorAttribute = a as AnyInterceptorAttribute;
                        return true;
                    }
                    return false;
                });
            });

            if (isAnyIterceptor && anyInterceptorAttribute != null && !string.IsNullOrWhiteSpace(anyInterceptorAttribute.InterceptorName))
            {
                if (handler.ComponentModel.Interceptors.Count <= 0 || !handler.ComponentModel.Interceptors.Any(i => i.ToString().Equals(anyInterceptorAttribute.InterceptorName, StringComparison.OrdinalIgnoreCase)))
                {
                    handler.ComponentModel.Interceptors.Add(InterceptorReference.ForKey(anyInterceptorAttribute.InterceptorName));
                }
            }
        }

        public void Terminate()
        {

        }
    }
}
