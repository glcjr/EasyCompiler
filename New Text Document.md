 
   EasyCompile gives a simpler access for using the Compiler that comes in the Net Framework. 
   It can be used to compile source as a string or as files from within your application.
   
   It is (I believe) Net Standard 2.0 compliant and needs the Net Standard 2.0 compliant System.Codedom library from Microsoft.
   The System.Codedom is found here: https://www.nuget.org/packages/System.CodeDom/4.4.0
   The Nugent Package Installer Console Command: Install-Package System.CodeDom -Version 4.4.0
   
   Simple use:
       Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
       if (compile.Compile())
            Console.WriteLine($"Successfully compiled and saved at {compile.GetName()}");
        else
            Console.WriteLine($"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}");
   
   Another way to do the same:
       Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
       compile.Compile();
       if (compile.Success)
            Console.WriteLine($"Successfully compiled and saved at {compile.GetName()}");
        else
            Console.WriteLine($"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}");
        
   If you want to compile a debug build you would do the following:
       Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
       compile.IncludeDebugInfo();
       compile.Compile();
   
   If you want to compile as a dll you would do the following:
       Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
       compile.SetToOutputDLL();
       compile.Compile();
       
  
   If you want to compile a source file instead of a string or with strings, you would do the following:
       Compiler compile = new Compiler(CompilerLanguages.csharp);
       compile.AddSourceFile("Pathtofile.cs");
       compile.Compile();
   
   If you want to compile to a a particular file name, you would do the following:
         Compiler compile = new Compiler(CompilerLanguages.csharp);
         compile.SetResultFileName("Pathtoresultfile");
         compile.Compile();
   
   If your code requires an assembly, you need to add it to the compiler. If you need the System.RunTime.dll, since it is in the GAC you would do the following
         Compiler compile = new Compiler(CompilerLanguages.csharp);
         compile.AddAssemblyLocation("System.RunTime.dll");
         compile.Compile();
   You could also do the following which adds some of the more common net framework dlls:
         Compiler compile = new Compiler(CompilerLanguages.csharp);
         compile.AddUsefulWindowsDesktopAssemblies();
         compile.Compile();
   
   