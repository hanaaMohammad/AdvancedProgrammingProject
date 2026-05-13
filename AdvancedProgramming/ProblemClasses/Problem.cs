using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace AdvancedProgramming.ProblemClasses
{
    public class Problem
    {

        string title;
        string input;
        string output;
        string type;
        string description;
        List<TestCase> TestCase;
        List<Example> Examplelist;
        string Constraints;
        string level;

        public string Title1 { get => title; set => title = value; }
        public string Type { get => type; set => type = value; }
        public string Description { get => description; set => description = value; }
        public List<TestCase> TestCases { get => TestCase; set => TestCase = value; }
        public List<Example> Example1 { get => Examplelist; set => Examplelist = value; }
        public string Constraints1 { get => Constraints; set => Constraints = value; }
        public string Level { get => level; set => level = value; }
        public string Input { get => input; set => input = value; }
        public string Output { get => output; set => output = value; }
    }
}
