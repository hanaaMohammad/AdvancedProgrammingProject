using System;
using System.Drawing;

namespace AdvancedProgramming
{
    public static class Theme
    {
        public static string FormatLevel(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
                return "Practice";
            return char.ToUpper(level[0]) + level.Substring(1).ToLower();
        }

        public static Color GetLevelColor(string level)
        {
            switch (level?.Trim().ToLower())
            {
                case "easy": return AppColors.Success;
                case "medium": return AppColors.Warning;
                case "hard": return AppColors.Error;
                default: return AppColors.MutedText;
            }
        }
    }
}
