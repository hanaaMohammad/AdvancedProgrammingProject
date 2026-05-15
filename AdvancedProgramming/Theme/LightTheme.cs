using System.Drawing;

namespace AdvancedProgramming
{
    public class LightTheme : ITheme
    {
        public Color FormBackColor => Color.FromArgb(246, 244, 250);
        public Color SurfaceColor => Color.FromArgb(240, 238, 245);
        public Color CardColor => Color.FromArgb(255, 255, 255);
        public Color ControlBackColor => Color.FromArgb(233, 230, 240);
        public Color ButtonBackColor => Color.FromArgb(108, 99, 255);
        public Color ButtonHoverBackColor => Color.FromArgb(130, 122, 255);
        public Color ButtonSecondaryBackColor => Color.FromArgb(255, 255, 255);
        public Color ButtonSecondaryHoverBackColor => Color.FromArgb(240, 237, 250);
        public Color AccentColor => Color.FromArgb(108, 99, 255);
        public Color AccentHoverColor => Color.FromArgb(130, 122, 255);
        public Color AccentLightColor => Color.FromArgb(230, 226, 255);
        public Color TextColor => Color.FromArgb(26, 22, 48);
        public Color SecondaryTextColor => Color.FromArgb(90, 82, 120);
        public Color MutedTextColor => Color.FromArgb(150, 142, 170);
        public Color BorderColor => Color.FromArgb(210, 205, 220);
        public Color InputBackColor => Color.FromArgb(255, 255, 255);
        public Color InputBorderColor => Color.FromArgb(210, 205, 220);
        public Color InputFocusBorderColor => Color.FromArgb(108, 99, 255);
        public Color SelectionForeColor => Color.Black;
        public Color SuccessColor => Color.FromArgb(0, 180, 100);
        public Color ErrorColor => Color.FromArgb(220, 50, 50);
        public Color WarningColor => Color.FromArgb(230, 160, 40);
        public Color DisabledBackColor => Color.FromArgb(230, 228, 235);
        public Color DisabledForeColor => Color.FromArgb(160, 155, 175);
        public Color DividerColor => Color.FromArgb(220, 216, 228);
        public Color ToolbarBackColor => Color.FromArgb(255, 255, 255);
    }
}
