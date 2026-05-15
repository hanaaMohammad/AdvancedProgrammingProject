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
        Color SurfaceColor { get; }
        Color CardColor { get; }
        Color ControlBackColor { get; }
        Color ButtonBackColor { get; }
        Color ButtonHoverBackColor { get; }
        Color ButtonSecondaryBackColor { get; }
        Color ButtonSecondaryHoverBackColor { get; }
        Color AccentColor { get; }
        Color AccentHoverColor { get; }
        Color AccentLightColor { get; }
        Color TextColor { get; }
        Color SecondaryTextColor { get; }
        Color MutedTextColor { get; }
        Color BorderColor { get; }
        Color InputBackColor { get; }
        Color InputBorderColor { get; }
        Color InputFocusBorderColor { get; }
        Color SelectionForeColor { get; }
        Color SuccessColor { get; }
        Color ErrorColor { get; }
        Color WarningColor { get; }
        Color DisabledBackColor { get; }
        Color DisabledForeColor { get; }
        Color DividerColor { get; }
        Color ToolbarBackColor { get; }
    }
}
