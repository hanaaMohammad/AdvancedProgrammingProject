using AdvancedProgramming.ProblemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Service
{
    public class RunCode
    {
        private string name;
        private string code;
        private string language;
        private Problem problem;
        private Problem problemChoice;

        public RunCode(Problem problemChoice)
        {
            this.problemChoice = problemChoice;
        }

        public RunCode(string name, string language, string code, Problem problem)
           
        {
            this.name = name;
            this.language = language;
            this.problem = problem;
            this.code = code;

        }
    }
}
