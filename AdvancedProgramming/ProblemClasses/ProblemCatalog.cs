using System;

namespace AdvancedProgramming.ProblemClasses
{
    public static class ProblemCatalog
    {
        public static bool IsAvailable(Problem problem) =>
            problem != null &&
            string.Equals(problem.level?.Trim(), "easy", StringComparison.OrdinalIgnoreCase);

        public static bool IsAvailable(string title) =>
            IsAvailable(ProblemLoadReadJs.GetByName(title));
    }
}
