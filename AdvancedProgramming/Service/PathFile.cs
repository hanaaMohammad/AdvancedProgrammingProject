using AdvancedProgramming.ProblemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdvancedProgramming.Service
{
    public class RunCode
    {
       
        private string code;
        private string language;
        private Problem problem;
      

        public RunCode(Problem problemChoice)
        {
            this.problemChoice = problemChoice;
        }

        public RunCode(string language, string code, Problem problem)

        {
          
            this.language = language;
            this.problem = problem;
            this.code = code;
            CreatSourceCode();

        }
        private void CreatSourceCode()
        {
           

            if (language == "c++")
            {
             File.CreateFile(codeRun+ ".cpp", code);



            }
            else
            {
               File.CreateFile(CodeRun + ".cs", code);

            }


        }
    }
}