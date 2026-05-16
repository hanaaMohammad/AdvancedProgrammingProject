using AdvancedProgramming.ProblemClasses;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AdvancedProgramming.Service
{
    public class CodeRunnerTestResult
    {
        public bool Passed { get; set; }
        public TestCase TestCase { get; set; }
        public string ActualOutput { get; set; }
        public string ExpectedOutput { get; set; }
    }

    public class CodeRunnerTestResultList
    {
        public List<CodeRunnerTestResult> Results { get; set; }
        public bool AllPassed => Results != null && Results.Count > 0 && Results.All(r => r.Passed);
    }

    public class CodeRunner
    {
        public List<CodeRunnerTestResult> RunTestCases(string code, List<TestCase> testCases)
        {
            var executor = new CodeRun.CSharpExecutor();
            var results = new List<CodeRunnerTestResult>();

            foreach (var testCase in testCases)
            {
                string actualOutput;
                try
                {
                    actualOutput = executor.ExecuteCode(code, testCase.input);
                }
                catch (Exception ex)
                {
                    actualOutput = "ERROR: " + ex.Message;
                }

                string expectedOutput = testCase.output?.ToString() ?? "";
                string actualTrim = actualOutput.Trim()
                    .Replace("\r\n", "\n")
                    .Replace("\r", "\n");
                string expectedTrim = expectedOutput.Trim()
                    .Replace("\r\n", "\n")
                    .Replace("\r", "\n");

                bool passed = actualTrim.Equals(expectedTrim, StringComparison.OrdinalIgnoreCase);

                results.Add(new CodeRunnerTestResult
                {
                    Passed = passed,
                    TestCase = testCase,
                    ActualOutput = actualOutput.Trim(),
                    ExpectedOutput = expectedOutput.Trim()
                });
            }
            return results;
        }
    }
}
