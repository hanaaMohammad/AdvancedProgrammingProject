using System.Drawing;

namespace AdvancedProgramming
{
    public class DarkTheme : ITheme
    {
        public Color FormBackColor => Color.FromArgb(30, 30, 30);
        public Color ControlBackColor => Color.FromArgb(37, 37, 38);
        public Color ButtonBackColor => Color.FromArgb(51, 51, 51);
        public Color ButtonHoverBackColor => Color.FromArgb(61, 61, 61);
        public Color AccentColor => Color.FromArgb(0, 122, 204);
        public Color TextColor => Color.White;
        public Color SecondaryTextColor => Color.FromArgb(204, 204, 204);
        public Color BorderColor => Color.FromArgb(62, 62, 66);
        public Color InputBackColor => Color.FromArgb(30, 30, 30);
        public Color SelectionForeColor => Color.White;
    }
}
