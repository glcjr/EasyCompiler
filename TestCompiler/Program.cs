using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyCompile;
/*********************************************************************************************************************************
Copyright and Licensing Message

This code is copyright 2018 Gary Cole Jr. 

This code is licensed by Gary Cole to others under the GPLv.3 https://opensource.org/licenses/GPL-3.0 
If you find the code useful or just feel generous a donation is appreciated.

Donate with this link: paypal.me/GColeJr
Please choose Friends and Family

Alternative Licensing Options

If you prefer to license under the LGPL for a project, https://opensource.org/licenses/LGPL-3.0
Single Developers working on their own project can do so with a donation of $20 or more. 
Small and medium companies can do so with a donation of $50 or more. 
Corporations can do so with a donation of $1000 or more.


If you prefer to license under the MS-RL for a project, https://opensource.org/licenses/MS-RL
Single Developers working on their own project can do so with a donation of $40 or more. 
Small and medium companies can do so with a donation of $100 or more.
Corporations can do so with a donation of $2000 or more.


if you prefer to license under the MS-PL for a project, https://opensource.org/licenses/MS-PL
Single Developers working on their own project can do so with a donation of $1000 or more. 
Small and medium companies can do so with a donation of $2000 or more.
Corporations can do so with a donation of $10000 or more.


If you use the code in more than one project, a separate license is required for each project.


Any modifications to this code must retain this message. 
*************************************************************************************************************************************/

namespace TestCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcestring = "using System; namespace HelloWorld    {        class Program        {    static public int Add(int a, int b) {return a + b;}        static public void Main(params string[] Args)            {                string message = \"\"; if (Args.Length > 0) message = Args[0];Console.WriteLine(\"Hello World \" + message);     Console.WriteLine(\"Press a key\"); Console.ReadKey();       }        }    }";
            Compiler compile = new Compiler(CompilerLanguages.csharp, sourcestring);
            compile.SetResultFileName($"{AppDomain.CurrentDomain.BaseDirectory}\\helloworld");
          
            compile.SetToLaunchAfterCompile(new string[] { "Let's get started" });
            compile.Compile();
            // find all namespaces from built in method
            if (compile.FindAllNameSpaces("Program", out List<string> namespaces))
            {
                Console.WriteLine("Namespaces that contain the class program: ");
                foreach (var s in namespaces)
                    Console.WriteLine(s);
            }
            // find all methods of a class from built in method
            if (compile.FindAllMethods("Program", out List<String> methodnames))
            {
                Console.WriteLine("All methods in the class program:");
                foreach (var m in methodnames)
                    Console.WriteLine(m);
            }
            if (compile.Success)
                Console.WriteLine($"Compiled successfully to {compile.GetName()}");
            else
                Console.WriteLine($"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}");

          // To Retrieve and Run the Assembly from outside the compiler and in the main program:

          Assembly Compiled = compile.GetCompiledAssembly();

            Type type = Compiled.GetTypes()[0];
            object obj = Activator.CreateInstance(type);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Also can launch from here.");

            // InvokeMember also returns an Object so if the method returns anything I believe you can access it by Object returnstuff = type.InvokeMember... 
            

            type.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, null);

            Console.WriteLine();
            Console.WriteLine();

            // Can Also use CompileRun to run a Method with return value in the Assembly
            compile.SetToLaunchAfterCompile("Add", new object[] { 3,4});
            compile.SetToMemoryOutputOnly(); // because the previously created exe causes an error if its compiled again
            object Value = compile.CompileRun(); 
            Type t = Value.GetType();
            if (t.Equals(typeof(CompilerErrorCollection)))
                foreach (CompilerError e in (CompilerErrorCollection)Value)
                    Console.WriteLine(e.ErrorText);
            else
                Console.WriteLine(Value);


            //Can also see what methods are available by doing this:
            MethodInfo[] methods = type.GetMethods();
            foreach (var meth in methods)
                Console.WriteLine(meth.Name);


        }
    }
}
