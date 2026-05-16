using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    // Colors keywords in the solution text box.
    public static class CSharpCodeHighlighter
    {
        private static readonly HashSet<string> Keywords = new HashSet<string>(StringComparer.Ordinal)
        {
            "using", "namespace", "class", "struct", "interface", "enum", "void", "var",
            "int", "long", "double", "float", "decimal", "bool", "char", "string", "object",
            "if", "else", "for", "foreach", "while", "do", "switch", "case", "break",
            "continue", "return", "public", "private", "protected", "internal", "static",
            "readonly", "const", "new", "true", "false", "null", "this", "base", "in", "out",
        };

        public static void Apply(RichTextBox box, string code)
        {
            if (box == null)
                return;

            string normalized = NormalizeCode(code);

            box.ReadOnly = false;
            box.Clear();
            box.Font = new Font("Consolas", 10);
            box.BackColor = AppColors.InsetBack;
            box.ForeColor = AppColors.Text;
            box.BorderStyle = BorderStyle.FixedSingle;
            box.WordWrap = true;
            box.ScrollBars = RichTextBoxScrollBars.None;

            if (string.IsNullOrEmpty(normalized))
            {
                box.ReadOnly = true;
                return;
            }

            Color keywordColor = Color.FromArgb(86, 156, 214);
            Color typeColor = Color.FromArgb(78, 201, 176);
            Color stringColor = Color.FromArgb(206, 145, 120);
            Color commentColor = Color.FromArgb(106, 153, 85);
            Color numberColor = Color.FromArgb(181, 206, 168);

            var tokenPattern = new Regex(
                @"(//[^\r\n]*)|(""(?:\\.|[^""\\])*"")|('(?:\\.|[^'\\])')|(\b\d+(?:\.\d+)?\b)|(\b[A-Za-z_][A-Za-z0-9_]*\b)|(\s+)|(\S)",
                RegexOptions.Compiled);

            foreach (Match match in tokenPattern.Matches(normalized))
            {
                string token = match.Value;
                Color color = AppColors.Text;

                if (token.StartsWith("//", StringComparison.Ordinal))
                    color = commentColor;
                else if (token.StartsWith("\"", StringComparison.Ordinal) || token.StartsWith("'", StringComparison.Ordinal))
                    color = stringColor;
                else if (char.IsDigit(token[0]))
                    color = numberColor;
                else if (Keywords.Contains(token))
                    color = keywordColor;
                else if (token.Length > 0 && char.IsUpper(token[0]))
                    color = typeColor;

                box.SelectionColor = color;
                box.AppendText(token);
            }

            box.SelectionStart = 0;
            box.SelectionLength = 0;
            box.ReadOnly = true;
        }

        public static string NormalizeCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;

            code = code.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            code = code.Replace("\r\n", "\n").Replace("\r", "\n");
            return code.Replace("\n", Environment.NewLine);
        }
    }
}
