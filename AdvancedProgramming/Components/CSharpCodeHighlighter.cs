using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
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

        private static readonly HashSet<string> Types = new HashSet<string>(StringComparer.Ordinal)
        {
            "Console", "Program", "String", "Int32", "Boolean", "Math",
        };

        public static void Apply(RichTextBox box, string code)
        {
            if (box == null)
                return;

            string normalized = NormalizeCode(code);

            box.ReadOnly = false;
            box.Clear();
            box.Font = DesignTokens.Typography.Code;
            box.BackColor = Theme.Current.InputBackColor;
            box.ForeColor = Theme.Current.TextColor;
            box.BorderStyle = BorderStyle.FixedSingle;
            box.WordWrap = true;
            box.ScrollBars = RichTextBoxScrollBars.None;
            box.DetectUrls = false;
            box.ShortcutsEnabled = true;

            if (string.IsNullOrEmpty(normalized))
            {
                box.ReadOnly = true;
                return;
            }

            Color defaultColor = Theme.Current.TextColor;
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
                Color color = defaultColor;

                if (token.StartsWith("//", StringComparison.Ordinal))
                    color = commentColor;
                else if (token.StartsWith("\"", StringComparison.Ordinal) || token.StartsWith("'", StringComparison.Ordinal))
                    color = stringColor;
                else if (Regex.IsMatch(token, @"^\d"))
                    color = numberColor;
                else if (Keywords.Contains(token))
                    color = keywordColor;
                else if (Types.Contains(token) || (token.Length > 0 && char.IsUpper(token[0])))
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
