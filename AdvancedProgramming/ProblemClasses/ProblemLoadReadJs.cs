using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Script.Serialization;

namespace AdvancedProgramming.ProblemClasses
{
    public class ProblemLoadReadJs
    {
        private List<Problem> problemsList;

        public ProblemLoadReadJs()
        {
            string textJson = File.ReadAllText(@"ProblemClasses\ProblemJs.json");
            problemsList = new JavaScriptSerializer().Deserialize<List<Problem>>(textJson) ?? new List<Problem>();
        }

        public Problem getProblemByName(string title)
        {
            return problemsList.FirstOrDefault(p => p.title == title);
        }

        public List<Problem> GetAllProblems()
        {
            return problemsList;
        }
    }
}
