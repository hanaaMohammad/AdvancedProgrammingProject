using AdvancedProgramming.ProblemClasses;
using System;
using System.Collections.Generic;
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
    }

    public class CodeRunner
    {
        public List<CodeRunnerTestResult> RunTestCases(string code, List<TestCase> testCases)
        {
            var executor = new CodeRun.CSharpExecutor();
            var results = new List<CodeRunnerTestResult>();

            foreach (var testCase in testCases)
            {
                string actual;
                try
                {
                    actual = executor.ExecuteCode(code, testCase.input);
                }
                catch (Exception ex)
                {
                    actual = "ERROR: " + ex.Message;
                }

                string expected = testCase.output?.ToString() ?? "";
                bool passed = Normalize(actual).Equals(Normalize(expected), StringComparison.OrdinalIgnoreCase);

                results.Add(new CodeRunnerTestResult
                {
                    Passed = passed,
                    TestCase = testCase,
                    ActualOutput = actual.Trim(),
                    ExpectedOutput = expected.Trim()
                });
            }

            return results;
        }

        private static string Normalize(string value) =>
            (value ?? "").Trim().Replace("\r\n", "\n").Replace("\r", "\n");
    }
}
