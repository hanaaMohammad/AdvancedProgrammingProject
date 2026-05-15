using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming.Service
{
    public class RunCode        
    {

        private string code;
        private string language;
        private Problem problem;


        public RunCode(Problem problemChoice)
        {
            this.problem = problemChoice;
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
                File.WriteAllText("codeRun.cpp", code);
            }
            else
                File.WriteAllText("CodeRun.cs", code);

        }


    }
}

