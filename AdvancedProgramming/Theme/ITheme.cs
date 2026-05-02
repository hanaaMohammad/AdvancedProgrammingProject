using System.Drawing;

namespace AdvancedProgramming
{
    public enum ThemeType
    {
        Dark,
        Light
    }

    public interface ITheme
    {
        Color FormBackColor { get; }
        Color ControlBackColor { get; }
        Color ButtonBackColor { get; }
        Color ButtonHoverBackColor { get; }
        Color AccentColor { get; }
        Color TextColor { get; }
        Color SecondaryTextColor { get; }
        Color BorderColor { get; }
        Color InputBackColor { get; }
        Color SelectionForeColor { get; }
    }
}
