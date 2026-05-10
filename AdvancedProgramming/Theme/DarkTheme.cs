using System.Drawing;

namespace AdvancedProgramming
{
    public class DarkTheme : ITheme
    {
        public Color FormBackColor => Color.FromArgb(10, 10, 20);
        public Color ControlBackColor => Color.FromArgb(20, 20, 35);
        public Color ButtonBackColor => Color.FromArgb(50, 40, 80);
        public Color ButtonHoverBackColor => Color.FromArgb(80, 60, 120);
        public Color AccentColor => Color.FromArgb(108, 99, 255);
        public Color TextColor => Color.White;
        public Color SecondaryTextColor => Color.FromArgb(180, 170, 200);
        public Color BorderColor => Color.FromArgb(60, 50, 90);
        public Color InputBackColor => Color.FromArgb(15, 15, 30);
        public Color SelectionForeColor => Color.White;
    }
}
