using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestMultiplePropertySort
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 简单测试数据
            var list = new List<MyClass>()
            {
                new MyClass()
                {
                    P1="h3",
                    P2=1,
                    P3=DateTime.Now
                },
                new MyClass()
                {
                    P1="h2",
                    P2=3,
                    P3=DateTime.Now.AddHours(-1)
                },
                new MyClass()
                {
                    P1="h1",
                    P2=2,
                    P3=DateTime.Now.AddHours(1)
                },
                new MyClass()
                {
                    P1="h3",
                    P2=1,
                    P3=DateTime.Now
                },
                new MyClass()
                {
                    P1="h1",
                    P2=1,
                    P3=DateTime.Now
                },
                new MyClass()
                {
                    P1="h2",
                    P2=2,
                    P3=DateTime.Now.AddHours(1)
                },
            };
            #endregion

            //调用多字段排序
            SortMutiplePropertyHelper<MyClass>.SortMutipleProperty(list);

            //可以复用
            SortMutiplePropertyHelper<MySecondClass>.SortMutipleProperty(new List<MySecondClass>());

            //输出排序结果
            list.ForEach(m => Trace.WriteLine(m.ToString()));
        }
    }

    public class MyClass
    {
        [SortOrder(0)]
        public string P1 { get; set; }

        [SortOrder(1)]
        public int P2 { get; set; }

        [SortOrder(2)]
        public DateTime P3 { get; set; }

        public override string ToString()
        {
            return P1.ToString() + "," + P2.ToString() + "," + P3.ToString();
        }
    }

    public class MySecondClass
    {
        
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SortOrderAttribute : Attribute
    {
        public int Order { get; set; }
        public SortOrderAttribute(int order)
        {
            this.Order = order;
        }
    }

    public class SortMutiplePropertyHelper<T> where T : class ,new()
    {
        /// <summary>
        /// 保存属性和顺序的字典
        /// </summary>
        public static readonly Dictionary<PropertyInfo, SortOrderAttribute> attrDic = new Dictionary<PropertyInfo, SortOrderAttribute>();

        static SortMutiplePropertyHelper()
        {
            //初始化order字段
            Type t = typeof(T);
            foreach (var prop in t.GetProperties())
            {
                foreach (var sortOrderAttribute in prop.GetCustomAttributes(typeof(SortOrderAttribute), false))
                {
                    if (sortOrderAttribute is SortOrderAttribute)
                    {
                        attrDic.Add(prop, sortOrderAttribute as SortOrderAttribute);
                        break;
                    }
                }
            }
        }

        public static void SortMutipleProperty(List<T> list)
        {
            list.Sort((t1, t2) =>
            {
                int result = 0;

                foreach (var attr in  attrDic.OrderBy(key => key.Value.Order))
                {
                    //这里简单的把属性转成字符串对比，比较靠谱的做法应当是针对不同的类型去做不同的比较。
                    string v1 = attr.Key.GetValue(t1).ToString();
                    string v2 = attr.Key.GetValue(t2).ToString();
                    result = v1.CompareTo(v2);
                    if (result != 0)
                    {
                        break;
                    }
                }

                return result;
            });
        }
    }
}