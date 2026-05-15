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
            try
            {
                string textJson = File.ReadAllText(@"ProblemClasses\ProblemJs.json");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                problemsList = serializer.Deserialize<List<Problem>>(textJson) ?? new List<Problem>();
            }
            catch
            {
                problemsList = new List<Problem>();
            }
        }

        public Problem getProblemByName(string title)
        {
            return problemsList.FirstOrDefault(p => p.title == title);
        }
    }
}
