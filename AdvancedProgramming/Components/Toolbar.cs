using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class Toolbar : Panel
    {
        private ContextMenuStrip themeMenu;
        private Button btnGear;
        private Form parentForm;
        private Point dragOffset;

        public Toolbar(Form form, string title)
        {
            parentForm = form;
            this.Height = 40;
            this.Dock = DockStyle.Top;
            this.BackColor = Theme.Current.ControlBackColor;
            this.ForeColor = Theme.Current.TextColor;

            var titleLabel = new Label
            {
                Text = title,
                Location = new Point(10, 10),
                Size = new Size(200, 20),
                ForeColor = Theme.Current.TextColor,
                BackColor = Color.Transparent
            };

            btnGear = new Button
            {
                Text = "⚙",
                Location = new Point(form.Width - 70, 5),
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor
            };
            btnGear.FlatAppearance.BorderSize = 0;
            btnGear.Click += BtnGear_Click;

            themeMenu = new ContextMenuStrip();
            themeMenu.BackColor = Theme.Current.ControlBackColor;
            themeMenu.ForeColor = Theme.Current.TextColor;
            var darkItem = themeMenu.Items.Add("Dark Theme");
            var lightItem = themeMenu.Items.Add("Light Theme");
            darkItem.Click += (s, e) => { Theme.SetTheme(form, ThemeType.Dark); btnGear.ForeColor = Color.White; };
            lightItem.Click += (s, e) => { Theme.SetTheme(form, ThemeType.Light); btnGear.ForeColor = Color.Black; };

            var btnClose = new Button
            {
                Text = "✕",
                Location = new Point(form.Width - 35, 5),
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => form.Close();

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnGear);
            this.Controls.Add(btnClose);

            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    dragOffset = new Point(-e.X, -e.Y);
            };
            this.MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point mousePos = Control.MousePosition;
                    mousePos.Offset(dragOffset.X, dragOffset.Y);
                    form.Location = mousePos;
                }
            };
        }

        private void BtnGear_Click(object sender, EventArgs e)
        {
            themeMenu.Show(Cursor.Position);
        }

        public void UpdateTheme()
        {
            this.BackColor = Theme.Current.ControlBackColor;
            this.ForeColor = Theme.Current.TextColor;
            themeMenu.BackColor = Theme.Current.ControlBackColor;
            themeMenu.ForeColor = Theme.Current.TextColor;
            foreach (Control c in this.Controls)
            {
                if (c is Label) c.ForeColor = Theme.Current.TextColor;
                if (c is Button btn && btn.Text == "⚙")
                    btn.ForeColor = Theme.CurrentThemeType == ThemeType.Dark ? Color.White : Color.Black;
                if (c is Button btn2 && btn2.Text == "✕")
                    btn2.ForeColor = Theme.Current.TextColor;
            }
        }
    }
}
