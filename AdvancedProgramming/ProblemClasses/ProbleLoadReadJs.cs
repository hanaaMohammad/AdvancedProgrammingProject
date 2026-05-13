using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace AdvancedProgramming.ProblemClasses
{
    public class ProbleLoadReadJs
    {
        private List<Problem> problemsList;
        public ProbleLoadReadJs()
        {
            string TextJson=File.ReadAllText("Problems.json");
           JavaScriptSerializer serializer = new JavaScriptSerializer();
           
            
            problemsList = serializer.Deserialize<List<Problem>>(TextJson);


        }

        public Problem getProblemByName(string title)
        {
          return  problemsList.FirstOrDefault(p =>p.Title1 == title);
        }
























    }
}
