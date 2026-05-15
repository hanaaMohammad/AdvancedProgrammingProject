using System;

namespace AdvancedProgramming.ProblemClasses
{
    public static class ProblemCatalog
    {
        public const string StarterProblemTitle =
            "Program to Print Full Pyramid Pattern (Star Pattern)";

        public const string ComingSoonSuffix = " (Coming Soon)";

        public const string DefaultCSharpTemplate =
            "using System;\r\n\r\n" +
            "class Program\r\n" +
            "{\r\n" +
            "    static void Main(string[] args)\r\n" +
            "    {\r\n" +
            "        \r\n" +
            "    }\r\n" +
            "}";

        public static bool IsAvailable(string title)
        {
            return string.Equals(title, StarterProblemTitle, StringComparison.Ordinal);
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
