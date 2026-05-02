using System.Drawing;

namespace AdvancedProgramming
{
    public class LightTheme : ITheme
    {
        public Color FormBackColor => Color.White;
        public Color ControlBackColor => Color.FromArgb(245, 245, 245);
        public Color ButtonBackColor => Color.FromArgb(240, 240, 240);
        public Color ButtonHoverBackColor => Color.FromArgb(225, 225, 225);
        public Color AccentColor => Color.FromArgb(0, 120, 215);
        public Color TextColor => Color.Black;
        public Color SecondaryTextColor => Color.FromArgb(64, 64, 64);
        public Color BorderColor => Color.FromArgb(204, 204, 204);
        public Color InputBackColor => Color.White;
        public Color SelectionForeColor => Color.Black;
    }
}
