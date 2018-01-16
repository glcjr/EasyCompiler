using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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


namespace EasyCompile
{
    /*
  * EasyCompile gives a simpler access for using the Compiler that comes in the Net Framework. 
  * It can be used to compile source as a string or as files from within your application.
  * 
  * It is (I believe) Net Standard 2.0 compliant and needs the Net Standard 2.0 compliant System.Codedom library from Microsoft.
  * The System.Codedom is found here: https://www.nuget.org/packages/System.CodeDom/4.4.0
  * The Nugent Package Installer Console Command: Install-Package System.CodeDom -Version 4.4.0
  * 
  * Simple use:
      * Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
      * if (compile.Compile())
      *      Console.WriteLine($"Successfully compiled and saved at {compile.GetName()}");
      *  else
      *      Console.WriteLine($"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}");
  * 
  * Another way to do the same:
      * Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
      * compile.Compile();
      * if (compile.Success)
      *      Console.WriteLine($"Successfully compiled and saved at {compile.GetName()}");
      *  else
      *      Console.WriteLine($"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}");
  *      
  * If you want to compile a debug build you would do the following:
      * Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
      * compile.IncludeDebugInfo();
      * compile.Compile();
  * 
  * If you want to compile as a dll you would do the following:
      * Compiler compile = new Compiler(CompilerLanguages.csharp, stringwithcssource);
      * compile.SetToOutputDLL();
      * compile.Compile();
      * 
  *
  * If you want to compile a source file instead of a string or with strings, you would do the following:
      * Compiler compile = new Compiler(CompilerLanguages.csharp);
      * compile.AddSourceFile("Pathtofile.cs");
      * compile.Compile();
  * 
  * If you want to compile to a a particular file name, you would do the following:
        * Compiler compile = new Compiler(CompilerLanguages.csharp);
        * compile.SetResultFileName("Pathtoresultfile");
        * compile.Compile();
  * 
  * If your code requires an assembly, you need to add it to the compiler. If you need the System.RunTime.dll, since it is in the GAC you would do the following
        * Compiler compile = new Compiler(CompilerLanguages.csharp);
        * compile.AddAssemblyLocation("System.RunTime.dll");
        * compile.Compile();
  * You could also do the following which adds some of the more common net framework dlls:
        * Compiler compile = new Compiler(CompilerLanguages.csharp);
        * compile.AddUsefulWindowsDesktopAssemblies();
        * compile.Compile();
  * 
  * 
  * */

    public enum CompilerLanguages { csharp, visualbasic };
    public class Compiler
    {
        private CodeDomProvider CodeProvider;
        private List<string> Assemblies = new List<string>();
        private List<string> Embed = new List<string>();
        private List<string> SourceStrings = new List<string>();
        private List<string> SourceFiles = new List<string>();
        private CompilerResults Results;
        private bool CompileToExecutable = true;
        private string FileName = "";
        private bool CompileToMemory = false;
        private bool IncludeDebug = false;
        private int WarningLevel = 3;
        private bool TreatWarningAsError = false;
        private bool Optimize = true;
        private bool LaunchAfterCompile = false;
        private string MethodToLaunch;
        
        public int ErrorCount
        {
            get
            {
                try
                {
                    return Results.Errors.Count;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public bool Success
        {
            get
            {
                try
                {
                    return Results.Errors.Count == 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        // This constructor should be used if you're going to be using only source files rather than strings that contain the source. Although you could use empty lists
        // and strings with the other constructors.
        public Compiler(CompilerLanguages language)
        {
            if (language == CompilerLanguages.csharp)
                CodeProvider = CodeDomProvider.CreateProvider("CSharp");
            else if (language == CompilerLanguages.visualbasic)
                CodeProvider = CodeDomProvider.CreateProvider("VisualBasic");
        }
        // This constructor can be used when you have several strings with source code in them.
        public Compiler(CompilerLanguages language, List<string> sourcestrings) : this(language)
        {
            SourceStrings = sourcestrings;
        }
        // This constructor can be used when you have one string that contains source code that you want to compile.
        public Compiler(CompilerLanguages language, string sourcestring) : this(language)
        {
            SourceStrings.Add(sourcestring);
        }
        // This constructor can be used when you have a list of strings with source code and you need to add assemblies (dlls) that are referenced.
        // If the Assemblies are not in the GAC, full paths need to be used. Or they need to be findable in some way.
        public Compiler(CompilerLanguages language, List<string> sourcestrings, List<string> assemblies) : this(language, sourcestrings)
        {
            Assemblies = assemblies;
        }
        // This constructor can be used when you have a list of strings with source code and you need to add assemblies (dlls) and you want to embed needed dlls.
        // The assemblies need the full the path if they are not located in the GAC. 
        public Compiler(CompilerLanguages language, List<string> sourcestrings, List<string> assemblies, List<string> embed) : this(language, sourcestrings, assemblies)
        {
            Embed = embed;
        }
        // This allows you to set the warning level of the compiler. The default is level three.
        public void SetWarning(int warn)
        {
            WarningLevel = warn;
        }
        // If you want the compiled code to contain debug information, you should call this method. Default is false.
        public void IncludeDebugInfo()
        {
            IncludeDebug = true;
        }
        // If you want warnings to end the compile as error this should be called. Default is false.
        public void TreatWarningsAsError()
        {
            TreatWarningAsError = true;
        }
        // This will be the name of the file that is produced by the compiler. Default is to create a temporary name.
        public void SetResultFileName(string file)
        {
            FileName = file;
        }
        //This method allows you to add a source string to the list of source strings that will be compiled.
        public void AddSourceString(string source)
        {
            if (SourceStrings.Contains(source))
                SourceStrings.Remove(source);
            SourceStrings.Add(source);
        }
        //This method allows you to add multiple source files to the source files list that will be compiled.
        public void AddSourceFiles(params string[] files)
        {
            foreach (var file in files)
                AddSourceFile(file);
        }
        //This methods adds a single source file to the source file list that will be compiled when the compile methods is called.
        //In order to successfully add a file, it must exist.
        public bool AddSourceFile(string file)
        {
            if (File.Exists(file))
            {
                if (SourceFiles.Contains(file))
                    SourceFiles.Remove(file);
                SourceFiles.Add(file);
                return true;
            }
            else
                return false;
        }
        // This method combines the source strings and source files lists together so the compile method can compile everything together.
        private string[] GetAllSource()
        {
            List<string> Source = new List<string>();
            foreach (string f in SourceFiles)
                Source.Add(File.ReadAllText(f));
            foreach (string s in SourceStrings)
                Source.Add(s);
            return Source.ToArray();
        }
        //This method allows mutliple dlls to be added to the assemblies list that are referenced in the source code that is being compiled.
        public void AddAssemblyLocations(params string[] assemblies)
        {
            foreach (var assembly in assemblies)
                AddAssemblyLocation(assembly);
        }
        //This method allows a single dll to be added to the assemblies list that is referenced in the source code that is being compiled.
        //The method makes sure the assemblies are unique in the list.
        public void AddAssemblyLocation(string assembly)
        {
            if (Assemblies.Contains(assembly))
                Assemblies.Remove(assembly);
            Assemblies.Add(assembly);
        }
        //This method adds mutliple dlls to be embedded in the compiled dll.
        public void AddEmbedLocations(params string[] embeds)
        {
            foreach (var e in embeds)
                AddEmbedLocation(e);
        }
        //This adds several of the more common desktop .net framework dlls to the assemblies list so that its simpler to add them.
        //You can also add these individually if there are too many included that are unneeded.
        public void AddUsefulWindowsDesktopAssemblies()
        {
            AddAssemblyLocations("System.Windows.dll", "System.RunTime.dll", "System.IO.dll", "System.Reflection.dll", "System.IO.Compression.dll", "System.dll",
                "System.Xaml.dll", "System.Xml.dll", "System.Core.dll", "System.Data.dll", "mscorlib.dll", "System.Drawing.dll");
        }
        //This method adds embedded dlls to the embed list.
        //It makes sure the list is unqiue
        //It also makes sure the file exists before adding them.
        public bool AddEmbedLocation(string embed)
        {
            if (File.Exists(embed))
            {
                if (Embed.Contains(embed))
                    Embed.Remove(embed);
                Embed.Add(embed);
                return true;
            }
            else
                return false;
        }
        //A method to remove unneeded assmblys from the list
        public bool RemoveAssemblyLocation(string assembly)
        {
            if (Assemblies.Contains(assembly))
            {
                Assemblies.Remove(assembly);
                return true;
            }
            else
                return false;
        }
        // A method to remove unneeded embeded locations from the list
        public bool RemoveEmbedLocation(string embed)
        {
            if (Embed.Contains(embed))
            {
                Embed.Remove(embed);
                return true;
            }
            else
                return false;
        }
        // Removes a source string from the list of source strings.
        // Probably a difficult method to use to find a match.
        public bool RemoveSourceString(string sourcestring)
        {
            if (SourceStrings.Contains(sourcestring))
            {
                SourceStrings.Remove(sourcestring);
                return true;
            }
            else
                return false;
        }
        // Removes a source file from the list of source files that are to be compiled.
        public bool RemoveSourceFile(string sourcefile)
        {
            if (SourceFiles.Contains(sourcefile))
            {
                SourceFiles.Remove(sourcefile);
                return true;
            }
            else
                return false;
        }
        // a method to cause the compiler to produce an executable.
        public void SetToOutputEXE()
        {
            CompileToExecutable = true;
            CompileToMemory = false;
        }
        // a method to cause the compiler to produce a dll.
        public void SetToOutputDLL()
        {
            CompileToExecutable = false;
            CompileToMemory = false;
        }
        // a Method to cause the compiler to only compile in memory. Good for testing the results of the source code if you don't want to run it.
        public void SetToMemoryOutputOnly()
        {
            CompileToMemory = true;
        }
        //Allows the option to launch the compiled script after compiling
        public void SetToLaunchAfterCompile(string method)
        {
            LaunchAfterCompile = true;
            MethodToLaunch = method;
        }
        // Prepares the output file name for the compiler. It either creates a name or uses the passed in name. And it makes sure the file has the proper exetension.
        private string RetriveName()
        {
            if (FileName == "")
                FileName = Path.GetTempFileName();
            if (CompileToExecutable)
                FileName = Path.ChangeExtension(FileName, ".exe");
            else
                FileName = Path.ChangeExtension(FileName, ".dll");
            return FileName;
        }
        // Returns the file name that was used to create the output file.
        public string GetName()
        {
            return FileName;
        }
        // This method prepares the compiler options and then compiles the code
        // It returns true if the compile is successful without errors
        // It returns false if the compile results in errors.
        public bool Compile()
        {
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = CompileToExecutable;
            if (Assemblies.Count > 0)
                parameters.ReferencedAssemblies.AddRange(Assemblies.ToArray());
            if (Embed.Count > 0)
                parameters.LinkedResources.AddRange(Embed.ToArray());
            parameters.IncludeDebugInformation = IncludeDebug;
            parameters.TreatWarningsAsErrors = TreatWarningAsError;
            parameters.WarningLevel = WarningLevel;
            if (Optimize)
                parameters.CompilerOptions += "/optimize";
            if (CompileToMemory)
                parameters.GenerateInMemory = true;
            else
                parameters.OutputAssembly = RetriveName();
            Results = CodeProvider.CompileAssemblyFromSource(parameters, GetAllSource());            
            if (Results.Errors.Count > 0)
                return false;
            else
            {
                if (LaunchAfterCompile)
                {
                    Assembly Compiled = Results.CompiledAssembly;
                    Type type = Compiled.GetTypes()[0];
                    object obj = Activator.CreateInstance(type);

                    type.InvokeMember(MethodToLaunch, BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, null);
                }
                return true;
            }
        }
        // This method returns the compiler errors as a COmpiler error collection
        public CompilerErrorCollection GetErrors()
        {
            return Results.Errors;
        }
        //Method to allow retrieval of the compiled Assembly
        public Assembly GetCompiledAssembly()
        {
            return Results.CompiledAssembly;
        }
        //This creates a list of errors the compiler found to be displayed to the user.
        public string GetErrorsAsString()
        {
            string resultofcompile = "";
            if (Results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in Results.Errors)
                {
                    resultofcompile += $"Line number {CompErr.Line}, Error Number: {CompErr.ErrorNumber}, '{CompErr.ErrorText};{Environment.NewLine}";
                }
            }
            return resultofcompile;
        }

    }
}
