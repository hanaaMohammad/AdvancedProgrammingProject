using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public static class Theme
    {
        public static ThemeType CurrentThemeType { get; private set; } = ThemeType.Dark;
        public static ITheme Current { get; private set; } = new DarkTheme();

        public static void ToggleTheme(Form form)
        {
            if (CurrentThemeType == ThemeType.Dark)
            {
                CurrentThemeType = ThemeType.Light;
                Current = new LightTheme();
            }
            else
            {
                CurrentThemeType = ThemeType.Dark;
                Current = new DarkTheme();
            }
            Apply(form);
        }

        public static void SetTheme(Form form, ThemeType themeType)
        {
            CurrentThemeType = themeType;
            Current = themeType == ThemeType.Dark ? (ITheme)new DarkTheme() : new LightTheme();
            Apply(form);
        }

        public static void Apply(Form form)
        {
            form.SuspendLayout();
            form.BackColor = Current.FormBackColor;
            form.ForeColor = Current.TextColor;
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ApplyToControls(form.Controls);
            form.ResumeLayout();
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case Button btn:
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = Current.BorderColor;
                        btn.FlatAppearance.MouseOverBackColor = Current.ButtonHoverBackColor;
                        btn.BackColor = Current.ButtonBackColor;
                        btn.ForeColor = Current.TextColor;
                        btn.FlatAppearance.BorderSize = 1;
                        break;

                    case Label lbl:
                        lbl.ForeColor = Current.TextColor;
                        lbl.BackColor = Color.Transparent;
                        break;

                    case TextBox txt:
                        txt.BackColor = Current.InputBackColor;
                        txt.ForeColor = Current.TextColor;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case ComboBox cmb:
                        cmb.BackColor = Current.InputBackColor;
                        cmb.ForeColor = Current.TextColor;
                        cmb.FlatStyle = FlatStyle.Flat;
                        break;

                    case DataGridView dgv:
                        dgv.BackgroundColor = Current.FormBackColor;
                        dgv.GridColor = Current.BorderColor;
                        dgv.DefaultCellStyle.BackColor = Current.ControlBackColor;
                        dgv.DefaultCellStyle.ForeColor = Current.TextColor;
                        dgv.DefaultCellStyle.SelectionBackColor = Current.AccentColor;
                        dgv.DefaultCellStyle.SelectionForeColor = Current.SelectionForeColor;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = Current.ControlBackColor;
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Current.TextColor;
                        dgv.RowHeadersDefaultCellStyle.BackColor = Current.ControlBackColor;
                        dgv.RowHeadersDefaultCellStyle.ForeColor = Current.TextColor;
                        dgv.EnableHeadersVisualStyles = false;
                        break;

                    case GroupBox grp:
                        grp.ForeColor = Current.TextColor;
                        grp.BackColor = Color.Transparent;
                        break;

                    case Panel pnl:
                        if (pnl is Components.PanelStars) break;
                        pnl.BackColor = Current.ControlBackColor;
                        break;

                    case ListBox lst:
                        lst.BackColor = Current.InputBackColor;
                        lst.ForeColor = Current.TextColor;
                        break;

                    case CheckBox chk:
                    case RadioButton rdo:
                        control.ForeColor = Current.TextColor;
                        control.BackColor = Color.Transparent;
                        break;

                    default:
                        control.ForeColor = Current.TextColor;
                        break;
                }

                if (control.HasChildren)
                    ApplyToControls(control.Controls);
            }
        }
    }
}
