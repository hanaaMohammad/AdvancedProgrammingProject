using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace AdvancedProgramming.ProblemClasses
{
    public static class ProblemLoadReadJs
    {
        private static List<Problem> problems;

        private static List<Problem> Problems =>
            problems ?? (problems = Load());

        private static List<Problem> Load()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProblemClasses", "ProblemJs.json");
            if (!File.Exists(path))
                path = @"ProblemClasses\ProblemJs.json";

            string json = File.ReadAllText(path);
            return new JavaScriptSerializer().Deserialize<List<Problem>>(json) ?? new List<Problem>();
        }

        public static Problem GetByName(string title) =>
            Problems.FirstOrDefault(p => p.title == title);

        public static List<Problem> GetAll() => Problems;
    }
}
