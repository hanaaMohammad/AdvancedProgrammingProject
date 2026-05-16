using System;

namespace AdvancedProgramming.ProblemClasses
{
    public static class ProblemCatalog
    {
        public const string StarterProblemTitle =
            "Program to Print Full Pyramid Pattern (Star Pattern)";

        public const string ComingSoonSuffix = " (Coming Soon)";

        public static bool IsAvailable(string title)
        {
            return true;
        }

        public static string GetListLabel(string title)
        {
            return IsAvailable(title) ? title : title + ComingSoonSuffix;
        }

        public static string ParseListLabel(string listLabel)
        {
            if (string.IsNullOrEmpty(listLabel))
                return listLabel;

            if (listLabel.EndsWith(ComingSoonSuffix, StringComparison.Ordinal))
                return listLabel.Substring(0, listLabel.Length - ComingSoonSuffix.Length);

            return listLabel;
        }
    }
}
