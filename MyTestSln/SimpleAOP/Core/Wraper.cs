using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SimpleAOP.Core
{
    public class Wraper
    {
        private static IDictionary<string, object> _Cache = new Dictionary<string, object>();

        public static T Wrap<T>(T sourceObj)
        {
            T returnObj;
            string key = sourceObj.GetType().ToString() + "_" + typeof(T).ToString();
            object aopObject;
            if (!_Cache.TryGetValue(key, out aopObject))
            {
                Type returnType = typeof(T);
                Type sourceType = sourceObj.GetType();
                AppDomain currentAppDomain = AppDomain.CurrentDomain;
                AssemblyName assyName = new AssemblyName(sourceType.Name + "_Aop_Assmely");
                AssemblyBuilder assyBuilder = currentAppDomain.DefineDynamicAssembly(assyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder modBuilder = assyBuilder.DefineDynamicModule(sourceType.Name + "_Aop_Module");

                String newTypeName = sourceType.Name + "_Aop";
                TypeAttributes newTypeAttribute = TypeAttributes.Class | TypeAttributes.Public;
                Type newTypeParent;
                Type[] newTypeInterfaces;
                MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual;
                if (returnType.IsInterface)
                {
                    newTypeParent = null;
                    newTypeInterfaces = new Type[] { returnType };
                }
                else
                {
                    newTypeParent = returnType;
                    newTypeInterfaces = new Type[0];
                }

                TypeBuilder typeBuilder = modBuilder.DefineType(newTypeName, newTypeAttribute, newTypeParent, newTypeInterfaces);

                //创建字段
                FieldBuilder fbuilder = typeBuilder.DefineField("_SourceObj", typeof(T), FieldAttributes.Public);

                #region 创建构造函数
                //ConstructorBuilder conBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(T) });
                //ILGenerator conIlGen = conBuilder.GetILGenerator();
                //conIlGen.Emit(OpCodes.Ldarg_0);
                //conIlGen.Emit(OpCodes.Ldarg_1);
                //conIlGen.Emit(OpCodes.Stfld, fbuilder);
                //conIlGen.Emit(OpCodes.Ret);
                #endregion

                MethodInfo[] targetMethods = returnType.GetMethods();
                foreach (MethodInfo targetMethod in targetMethods)
                {
                    if ((!targetMethod.IsAbstract) && (!targetMethod.IsVirtual))
                    {
                        continue;
                    }

                    #region 得到方法的各个参数的类型
                    ParameterInfo[] arrParamInfo = targetMethod.GetParameters();
                    int paramCount = arrParamInfo.Length;
                    Type[] paramType = new Type[paramCount];
                    for (int i = 0; i < paramCount; i++)
                        paramType[i] = arrParamInfo[i].ParameterType;
                    #endregion

                    #region 传入方法签名，得到方法生成器
                    MethodBuilder methodBuilder = typeBuilder.DefineMethod(targetMethod.Name, methodAttributes, targetMethod.ReturnType, paramType);
                    MethodInfo getTypeMI = typeof(object).GetMethod("GetType", new Type[] { });
                    MethodInfo getMethodMI = typeof(Type).GetMethod("GetMethod", new Type[] { typeof(string), typeof(Type[]) });

                    #endregion

                    #region 定义局部变量
                    //得到IL生成器
                    ILGenerator ilGen = methodBuilder.GetILGenerator();
                    ilGen.DeclareLocal(typeof(MethodInfo), true); //loc0
                    ilGen.DeclareLocal(typeof(object[]), true);//loc1
                    ilGen.DeclareLocal(typeof(MethodContext), true);//loc2
                    ilGen.DeclareLocal(typeof(Type[]), true);//loc3
                    ilGen.DeclareLocal(typeof(Type), true);//loc4
                    #endregion

                    object[] aopAtts = sourceType.GetMethod(targetMethod.Name, paramType).GetCustomAttributes(typeof(BaseAttribute), true);
                    if (aopAtts != null && aopAtts.Length > 0)//检测是否标注了AOP属性
                    {
                        BaseAttribute apoAtt = (BaseAttribute)aopAtts[0];

                        #region 获取sourceObj中的当前MethodInfo并存入loc0中
                        ilGen.Emit(OpCodes.Nop);
                        ilGen.Emit(OpCodes.Ldarg_0);
                        ilGen.Emit(OpCodes.Ldfld, fbuilder);
                        ilGen.EmitCall(OpCodes.Call, getTypeMI, null);
                        ilGen.Emit(OpCodes.Stloc, 4);

                        #region 获取参数类型数组
                        ilGen.Emit(OpCodes.Ldc_I4, paramCount);
                        ilGen.Emit(OpCodes.Newarr, typeof(Type));
                        ilGen.Emit(OpCodes.Stloc_3);
                        ilGen.Emit(OpCodes.Ldloc_3);
                        for (int i = 0; i < paramCount; i++)
                        {
                            ilGen.Emit(OpCodes.Ldc_I4, i);
                            ilGen.Emit(OpCodes.Ldarg, (short)(i + 1));//arg0表示this,真正的方法参数从索引1开始
                            ilGen.Emit(OpCodes.Box, paramType[i]);//不装箱对一些类型的参数会出错
                            ilGen.EmitCall(OpCodes.Call, getTypeMI, null);
                            ilGen.Emit(OpCodes.Stelem_Ref);
                        }
                        #endregion

                        ilGen.Emit(OpCodes.Ldloc, 4);
                        ilGen.Emit(OpCodes.Ldstr, targetMethod.Name);
                        ilGen.Emit(OpCodes.Ldloc_3);
                        ilGen.EmitCall(OpCodes.Call, getMethodMI, null);
                        ilGen.Emit(OpCodes.Stloc_0);

                        #endregion

                        #region 获取参数并将其存入object[]类型变量loc1中
                        ilGen.Emit(OpCodes.Ldc_I4, paramCount);
                        ilGen.Emit(OpCodes.Newarr, typeof(object));
                        ilGen.Emit(OpCodes.Stloc_1);
                        ilGen.Emit(OpCodes.Ldloc_1);
                        for (int i = 0; i < paramCount; i++)
                        {
                            ilGen.Emit(OpCodes.Ldc_I4, i);
                            ilGen.Emit(OpCodes.Ldarg, (short)(i + 1));//arg0表示this,真正的方法参数从索引1开始
                            ilGen.Emit(OpCodes.Box, paramType[i]);//不装箱对一些类型的参数会出错
                            ilGen.Emit(OpCodes.Stelem_Ref);
                        }
                        #endregion

                        #region 创建MethodContext对象
                        ConstructorInfo methodContextCI = typeof(MethodContext).GetConstructor(new Type[] { });
                        ilGen.Emit(OpCodes.Newobj, methodContextCI);
                        ilGen.Emit(OpCodes.Stloc_2);
                        ilGen.Emit(OpCodes.Ldloc_2);
                        #endregion

                        #region 给MethodInfo付值
                        ilGen.Emit(OpCodes.Ldloc_0);//MethodInfo
                        ilGen.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_MethodInfo", new Type[] { typeof(MethodInfo) }), null);
                        #endregion

                        #region 给Executor付值
                        ilGen.Emit(OpCodes.Ldloc_2);
                        ilGen.Emit(OpCodes.Ldarg_0);
                        ilGen.Emit(OpCodes.Ldfld, fbuilder);
                        ilGen.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Executor", new Type[] { typeof(object) }), null);
                        #endregion

                        #region 给ParametersValue付值
                        ilGen.Emit(OpCodes.Ldloc_2);
                        ilGen.Emit(OpCodes.Ldloc_1);
                        ilGen.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_ParametersValue", new Type[] { typeof(object[]) }), null);
                        #endregion

                        #region 创建CallHandler对象并调用方法
                        Type handlerType = apoAtt.CallHandlerType;
                        ConstructorInfo ci = handlerType.GetConstructor(new Type[] { });
                        ilGen.Emit(OpCodes.Newobj, ci);
                        MethodInfo aopMethod = handlerType.GetMethod("Invoke", new Type[] { typeof(MethodContext) });
                        ilGen.Emit(OpCodes.Ldloc_2);
                        ilGen.EmitCall(OpCodes.Call, aopMethod, null);
                        #endregion
                    }
                    else//直接执行原方法
                    {
                        #region 将参数压入栈 并执行原方法
                        ilGen.Emit(OpCodes.Ldarg_0);
                        ilGen.Emit(OpCodes.Ldfld, fbuilder);
                        for (int i = 0; i < paramCount; i++)
                        {
                            ilGen.Emit(OpCodes.Ldarg, (short)(i + 1));//arg0表示this,真正的方法参数从索引1开始
                        }
                        ilGen.EmitCall(OpCodes.Call, targetMethod, null);
                        #endregion
                    }

                    ilGen.Emit(OpCodes.Ret);
                }
                returnObj = (T)Activator.CreateInstance(typeBuilder.CreateType());
                if (returnObj != null)
                    _Cache[key] = returnObj;
                assyBuilder.Save("adcdefg.dll");
            }
            else
            {
                returnObj = (T)aopObject;
            }
            if (returnObj != null)
            {
                FieldInfo fi = returnObj.GetType().GetField("_SourceObj");
                fi.SetValue(returnObj, sourceObj);
            }
            return returnObj;
        }
    }
}
