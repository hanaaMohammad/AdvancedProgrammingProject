using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public static class Theme
    {
        public static DarkTheme Current { get; } = new DarkTheme();

        public static void Apply(Control control)
        {
            control.SuspendLayout();
            ApplyToControl(control, isRoot: true);
            ApplyToControls(control.Controls);
            control.ResumeLayout();
        }

        private static void ApplyToControl(Control control, bool isRoot = false)
        {
            if (control is Form || isRoot)
            {
                control.BackColor = Current.FormBackColor;
                control.ForeColor = Current.TextColor;
                control.Font = DesignTokens.Typography.Family;
                return;
            }

            if (control.Tag?.ToString() == "NoTheme")
                return;

            if (control.Tag?.ToString() == "Card")
            {
                control.BackColor = Current.CardColor;
                return;
            }

            if (control.Tag?.ToString() == "Surface")
            {
                control.BackColor = Current.SurfaceColor;
                return;
            }
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.Tag?.ToString() == "NoTheme")
                {
                    if (control.HasChildren)
                        ApplyToControls(control.Controls);
                    continue;
                }

                switch (control)
                {
                    case Button btn:
                        ApplyButton(btn);
                        break;

                    case Label lbl:
                        ApplyLabel(lbl);
                        break;

                    case TextBox txt:
                        ApplyTextBox(txt);
                        break;

                    case RichTextBox rtxt:
                        rtxt.BackColor = Current.InputBackColor;
                        rtxt.ForeColor = Current.TextColor;
                        rtxt.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case ComboBox cmb:
                        cmb.BackColor = Current.InputBackColor;
                        cmb.ForeColor = Current.TextColor;
                        cmb.FlatStyle = FlatStyle.Flat;
                        break;

                    case ListBox lst:
                        lst.BackColor = Current.InputBackColor;
                        lst.ForeColor = Current.TextColor;
                        lst.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case DataGridView dgv:
                        ApplyDataGridView(dgv);
                        break;

                    case GroupBox grp:
                        grp.ForeColor = Current.TextColor;
                        grp.BackColor = Color.Transparent;
                        break;

                    case Panel pnl:
                        pnl.BackColor = Current.ControlBackColor;
                        break;

                    case CheckBox chk:
                    case RadioButton rdo:
                        control.ForeColor = Current.TextColor;
                        control.BackColor = Color.Transparent;
                        break;

                    case PictureBox pb:
                        break;

                    default:
                        control.ForeColor = Current.TextColor;
                        control.BackColor = Current.SurfaceColor;
                        break;
                }

                if (control.HasChildren)
                    ApplyToControls(control.Controls);
            }
        }

        private static void ApplyButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Current.BorderColor;

            switch (btn.Tag?.ToString())
            {
                case "Primary":
                    btn.BackColor = Current.AccentColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Current.AccentHoverColor;
                    break;
                case "Secondary":
                    btn.BackColor = Current.ButtonSecondaryBackColor;
                    btn.ForeColor = Current.TextColor;
                    btn.FlatAppearance.BorderColor = Current.BorderColor;
                    btn.FlatAppearance.MouseOverBackColor = Current.ButtonSecondaryHoverBackColor;
                    break;
                case "Ghost":
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Current.TextColor;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Current.ButtonSecondaryHoverBackColor;
                    break;
                case "Danger":
                    btn.BackColor = Current.ErrorColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 110, 110);
                    break;
                case "Success":
                    btn.BackColor = Current.SuccessColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 220, 130);
                    break;
                default:
                    btn.BackColor = Current.ButtonBackColor;
                    btn.ForeColor = Current.TextColor;
                    btn.FlatAppearance.BorderColor = Current.BorderColor;
                    btn.FlatAppearance.MouseOverBackColor = Current.ButtonHoverBackColor;
                    break;
            }
        }

        private static void ApplyLabel(Label lbl)
        {
            lbl.BackColor = Color.Transparent;
            switch (lbl.Tag?.ToString())
            {
                case "Secondary":
                    lbl.ForeColor = Current.SecondaryTextColor;
                    break;
                case "Muted":
                    lbl.ForeColor = Current.MutedTextColor;
                    break;
                case "Error":
                    lbl.ForeColor = Current.ErrorColor;
                    break;
                case "Success":
                    lbl.ForeColor = Current.SuccessColor;
                    break;
                case "Warning":
                    lbl.ForeColor = Current.WarningColor;
                    break;
                default:
                    lbl.ForeColor = Current.TextColor;
                    break;
            }
        }

        private static void ApplyTextBox(TextBox txt)
        {
            txt.BackColor = Current.InputBackColor;
            txt.ForeColor = Current.TextColor;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void ApplyDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Current.SurfaceColor;
            dgv.GridColor = Current.DividerColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.BackColor = Current.CardColor;
            dgv.DefaultCellStyle.ForeColor = Current.TextColor;
            dgv.DefaultCellStyle.SelectionBackColor = Current.AccentColor;
            dgv.DefaultCellStyle.SelectionForeColor = Current.SelectionForeColor;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Current.ControlBackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Current.TextColor;
            dgv.RowHeadersDefaultCellStyle.BackColor = Current.ControlBackColor;
            dgv.RowHeadersDefaultCellStyle.ForeColor = Current.TextColor;
            dgv.EnableHeadersVisualStyles = false;
        }

        public static void StylePage(Control page)
        {
            page.BackColor = Current.FormBackColor;
            page.ForeColor = Current.TextColor;
        }

        public static string FormatLevel(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
                return "Practice";
            return char.ToUpper(level[0]) + level.Substring(1).ToLower();
        }

        public static Color GetLevelColor(string level)
        {
            switch (level?.Trim().ToLower())
            {
                case "easy": return Current.SuccessColor;
                case "medium": return Current.WarningColor;
                case "hard": return Current.ErrorColor;
                default: return Current.SecondaryTextColor;
            }
        }
    }
}
