using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public static class Theme
    {
        // Color Palette
        public static readonly Color FormBackColor = Color.FromArgb(30, 30, 30);       // #1E1E1E
        public static readonly Color ControlBackColor = Color.FromArgb(37, 37, 38);    // #252526
        public static readonly Color ButtonBackColor = Color.FromArgb(51, 51, 51);     // #333333
        public static readonly Color ButtonHoverBackColor = Color.FromArgb(61, 61, 61); // #3D3D3D
        public static readonly Color AccentColor = Color.FromArgb(0, 122, 204);        // #007ACC
        public static readonly Color TextColor = Color.White;
        public static readonly Color SecondaryTextColor = Color.FromArgb(204, 204, 204); // #CCCCCC
        public static readonly Color BorderColor = Color.FromArgb(62, 62, 66);         // #3E3E42
        public static readonly Color InputBackColor = Color.FromArgb(30, 30, 30);       // #1E1E1E

        public static void Apply(Form form)
        {
            form.BackColor = FormBackColor;
            form.ForeColor = TextColor;
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case Button btn:
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = BorderColor;
                        btn.FlatAppearance.MouseOverBackColor = ButtonHoverBackColor;
                        btn.BackColor = ButtonBackColor;
                        btn.ForeColor = TextColor;
                        btn.FlatAppearance.BorderSize = 1;
                        break;

                    case Label lbl:
                        lbl.ForeColor = TextColor;
                        lbl.BackColor = Color.Transparent;
                        break;

                    case TextBox txt:
                        txt.BackColor = InputBackColor;
                        txt.ForeColor = TextColor;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case ComboBox cmb:
                        cmb.BackColor = InputBackColor;
                        cmb.ForeColor = TextColor;
                        cmb.FlatStyle = FlatStyle.Flat;
                        break;

                    case DataGridView dgv:
                        dgv.BackgroundColor = FormBackColor;
                        dgv.GridColor = BorderColor;
                        dgv.DefaultCellStyle.BackColor = ControlBackColor;
                        dgv.DefaultCellStyle.ForeColor = TextColor;
                        dgv.DefaultCellStyle.SelectionBackColor = AccentColor;
                        dgv.DefaultCellStyle.SelectionForeColor = TextColor;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = ControlBackColor;
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
                        dgv.RowHeadersDefaultCellStyle.BackColor = ControlBackColor;
                        dgv.RowHeadersDefaultCellStyle.ForeColor = TextColor;
                        dgv.EnableHeadersVisualStyles = false;
                        break;

                    case GroupBox grp:
                        grp.ForeColor = TextColor;
                        grp.BackColor = Color.Transparent;
                        break;

                    case Panel pnl:
                        pnl.BackColor = ControlBackColor;
                        break;

                    case CheckBox chk:
                    case RadioButton rdo:
                        control.ForeColor = TextColor;
                        control.BackColor = Color.Transparent;
                        break;

                    default:
                        control.ForeColor = TextColor;
                        break;
                }

                if (control.HasChildren)
                    ApplyToControls(control.Controls);
            }
        }
    }
}
