using AdvancedProgramming.ProblemClasses;
using System.Collections.Generic;
using System;
using AdvancedProgramming.CodeRun;

namespace AdvancedProgramming.Service
{
    public class CodeRunnerTestResult
    {
        public bool Passed { get; set; }
        public TestCase TestCase { get; set; }
        public string ActualOutput { get; set; }
        public string ExpectedOutput { get; set; }




    }

    public class CodeRunner
    {


        public List<CodeRunnerTestResult> RunTestCases(string code, string language, List<TestCase> testCases)
        {
            CodeExecutor executor;
            if (language == "C#")
                executor = new csharpExecutor();
            else
                executor = new JavaExcuteor();

            var results = new List<CodeRunnerTestResult>();


            foreach (var T in testCases)
            {
                string actualOutput = executor.ExecuteCode(code, T.input);


                string expectedOutput = T.output?.ToString() ?? "";

                string actualTrim =
                        actualOutput.Trim()
                        .Replace("\r\n", "\n")
                        .Replace("\r", "\n");

                string expectedTrim =
                    expectedOutput.Trim()
                    .Replace("\r\n", "\n")
                    .Replace("\r", "\n");

                bool passed = actualTrim.Equals(expectedTrim, StringComparison.OrdinalIgnoreCase);




                actualTrim.Equals(
                       expectedTrim,
                       StringComparison.OrdinalIgnoreCase);

                results.Add(new CodeRunnerTestResult
                {
                    Passed = passed,
                    TestCase = T,
                    ActualOutput = actualOutput.Trim(),
                    ExpectedOutput = expectedOutput.Trim()
                });



            }
return results;










        }
    }
}