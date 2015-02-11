using System;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


public class MyDynamicCompiler
{
    public static void Main()
    {
        ///需要编译的字符串
        string MyCodeString = @"
        public class MyTest
        {
            public static string GetTestString()
            {
                string MyStr = ""This is a Dynamic Compiler Demo!"";
                return MyStr;
            }
        }";

        CompilerParameters compilerParams = new CompilerParameters();
        ///编译器选项设置
        compilerParams.CompilerOptions = "/target:library /optimize";

        ///编译时在内存输出
        compilerParams.GenerateInMemory = true;

        ///生成调试信息
        compilerParams.IncludeDebugInformation = false;

        ///添加相关的引用
        compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
        compilerParams.ReferencedAssemblies.Add("System.dll");

        CSharpCodeProvider compiler = new CSharpCodeProvider();

        ///编译
        CompilerResults results = compiler.CompileAssemblyFromSource(compilerParams, MyCodeString);

        ///创建程序集
        Assembly asm = results.CompiledAssembly;
        
        ///获取编译后的类型
        object objMyTestClass = asm.CreateInstance("MyTest");
        Type MyTestClassType = objMyTestClass.GetType();
        
        ///输出结果
        Console.WriteLine(MyTestClassType.GetMethod("GetTestString").Invoke(objMyTestClass, null));

        Console.ReadLine();

    }
}