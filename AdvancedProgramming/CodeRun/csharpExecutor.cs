using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdvancedProgramming.CodeRun
{
    public class CSharpExecutor
    {
        public string ExecuteCode(string code, string input)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            string assemblyName = Path.GetRandomFileName();

            var references = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location));

            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
                return string.Join("\n", result.Diagnostics);

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(ms.ToArray());
            var entryPoint = assembly.EntryPoint;
            var output = new StringWriter();
            Console.SetOut(output);
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            entryPoint.Invoke(null,
                entryPoint.GetParameters().Length == 0
                ? null
                : new object[] { new string[0] });

            return output.ToString();
        }
    }
}
