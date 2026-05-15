using AdvancedProgramming.ProblemClasses;
using System.Collections.Generic;
using System;

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
            var path = new FileManger().Path(code, language);
            var codeCompile = new CodeCompile();
            string compileResult = codeCompile.Compile(path, language);


            var results = new List<CodeRunnerTestResult>();

            if (!string.IsNullOrEmpty(compileResult))
            {
                foreach (var tc in testCases)
                {
                    results.Add(new CodeRunnerTestResult
                    {
                        Passed = false,
                        TestCase = tc,
                        ActualOutput = "COMPILE ERROR: " + compileResult,
                        ExpectedOutput = tc.output?.ToString() ?? ""
                    });
                }
                return results;

            }

            var procRun = new ProcessRun();
            foreach (var tc in testCases)
            {
                string actualOutput = procRun.Run(path, language, tc.input);


                string expectedOutput = "";
                if (tc.output != null)
                    expectedOutput = tc.output.ToString();

                string actualTrim = actualOutput.Trim().Replace("\r\n", "\n").Replace("\r", "\n");
                string expectedTrim = expectedOutput.Trim().Replace("\r\n", "\n").Replace("\r", "\n");

                bool passed = actualTrim.Equals(expectedTrim, StringComparison.OrdinalIgnoreCase);

                results.Add(new CodeRunnerTestResult
                {
                    Passed = passed,
                    TestCase = tc,
                    ActualOutput = actualOutput.Trim(),
                    ExpectedOutput = expectedOutput.Trim()

                });



            }

            return results;

        }








    }
}
