using System.Collections.Generic;

namespace AdvancedProgramming.ProblemClasses
{
    public class Problem
    {
        public string title { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public List<TestCase> TestCase { get; set; }
        public Example Example { get; set; }
        public string Constraints { get; set; }
        public string level { get; set; }
        public string solution { get; set; }
    }
}
