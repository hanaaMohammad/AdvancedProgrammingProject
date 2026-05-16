using System;

namespace AdvancedProgramming.ProblemClasses
{
    public class TestCase
    {
        public string input { get; set; }
        public object output { get; set; }

        public override string ToString()
        {
            return "Input: " + input + "\n Output: " + output;
        }

    
    }
    


}
