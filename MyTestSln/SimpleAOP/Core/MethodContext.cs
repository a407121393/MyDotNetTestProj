using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleAOP.Core
{
    /// <summary>
    /// 原方法的上下文
    /// </summary>
    public class MethodContext
    {
        /// <summary>
        /// 原方法的MethodInfo
        /// </summary>
        public MethodInfo MethodInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 执行对象
        /// </summary>
        public object Executor
        {
            get;
            set;
        }
        /// <summary>
        /// 调用方法中的参数值
        /// </summary>
        public object[] ParametersValue
        {
            get;
            set;
        }

        /// <summary>
        /// 调用原方法
        /// </summary>
        /// <returns>返回原方法返回值</returns>
        public object Invoke()
        {
            return MethodInfo.Invoke(Executor, ParametersValue);
        }
    }
}