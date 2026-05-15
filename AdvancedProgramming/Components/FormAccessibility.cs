using System.Linq;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public static class FormAccessibility
    {
        private static ToolTip sharedToolTip;

        public static void SetShortcutHint(Control control, string shortcutKeys, string description = null)
        {
            if (control == null) return;

            string hint = string.IsNullOrEmpty(description)
                ? shortcutKeys
                : description + " (" + shortcutKeys + ")";

            control.AccessibleDescription = hint;

            if (sharedToolTip == null)
            {
                sharedToolTip = new ToolTip
                {
                    AutoPopDelay = 8000,
                    InitialDelay = 400,
                    ReshowDelay = 200,
                    ShowAlways = true,
                };
            }

            sharedToolTip.SetToolTip(control, hint);
        }

        public static void FocusFirstInput(Control container)
        {
            if (container == null) return;

            var first = container.Controls
                .OfType<TextBox>()
                .FirstOrDefault(tb => tb.Visible && tb.Enabled && !tb.ReadOnly);

            if (first != null)
            {
                first.Focus();
                first.Select();
            }
        }

        public static void FocusPrimaryAction(Control container)
        {
            if (container == null) return;

            var primary = container.Controls
                .OfType<Button>()
                .FirstOrDefault(b => b.Visible && b.Enabled && b.Tag?.ToString() == "Primary");

            primary?.Focus();
        }
    }
}
