using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Service
{
    public class CodeRunner
    {
       public string Run(string code, string language)
        {
            var path=new FileManger().Path(code,language);

            var codeCompile = new CodeCompile();

            string compileResult = codeCompile.Copmile(path, "c++");

            if (!string.IsNullOrEmpty(compileResult))
            {
                return compileResult;

            }
            var run = new CodeRunner();
            return run.Run(path, language);

        }




    }
}
