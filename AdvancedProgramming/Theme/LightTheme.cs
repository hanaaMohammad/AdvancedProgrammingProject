using System.Drawing;

namespace AdvancedProgramming
{
    public class LightTheme : ITheme
    {
        public Color FormBackColor => Color.FromArgb(245, 240, 250);
        public Color ControlBackColor => Color.FromArgb(235, 230, 240);
        public Color ButtonBackColor => Color.FromArgb(108, 99, 255);
        public Color ButtonHoverBackColor => Color.FromArgb(130, 120, 255);
        public Color AccentColor => Color.FromArgb(108, 99, 255);
        public Color TextColor => Color.FromArgb(30, 20, 50);
        public Color SecondaryTextColor => Color.FromArgb(80, 70, 100);
        public Color BorderColor => Color.FromArgb(180, 170, 190);
        public Color InputBackColor => Color.White;
        public Color SelectionForeColor => Color.Black;
    }
}
