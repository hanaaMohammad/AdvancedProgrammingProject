using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class Toolbar : Panel
    {
        private ContextMenuStrip themeMenu;
        private Button btnGear;
        private Button btnClose;
        private Label titleLabel;
        private Form parentForm;
        private Point dragOffset;

        public Toolbar(Form form, string title)
        {
            parentForm = form;
            this.Height = 55;
            this.Dock = DockStyle.Top;
            this.BackColor = Theme.Current.ControlBackColor;
            this.ForeColor = Theme.Current.TextColor;

            titleLabel = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Theme.Current.TextColor,
                BackColor = Color.Transparent
            };
            titleLabel.Location = new Point((this.Width - titleLabel.Width) / 2, (this.Height - titleLabel.Height) / 2);

            btnGear = new Button
            {
                Text = "⚙",
                Location = new Point(form.Width - 80, 10),
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
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

            btnClose = new Button
            {
                Text = "✕",
                Location = new Point(form.Width - 40, 10),
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => form.Close();

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnGear);
            this.Controls.Add(btnClose);

            this.Resize += (s, e) => RepositionTitle();

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

        private void RepositionTitle()
        {
            titleLabel.Location = new Point((this.Width - titleLabel.Width) / 2, (this.Height - titleLabel.Height) / 2);
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
            titleLabel.ForeColor = Theme.Current.TextColor;
            btnGear.ForeColor = Theme.CurrentThemeType == ThemeType.Dark ? Color.White : Color.Black;
            btnClose.ForeColor = Theme.Current.TextColor;
            RepositionTitle();
        }
    }
}
