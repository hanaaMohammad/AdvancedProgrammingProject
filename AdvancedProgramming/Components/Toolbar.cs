using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class Toolbar : Panel
    {
        public event EventHandler CloseRequested;

        private ContextMenuStrip themeMenu;
        private Button btnMinimize;
        private Button btnGear;
        private Button btnClose;
        private Label titleLabel;
        private Point dragOffset;

        public Toolbar(Control parent, string title)
        {
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

            btnMinimize = new Button
            {
                Text = "\u2014",
                Location = new Point(parent.Width - 120, 10),
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = false,
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) =>
            {
                var form = this.FindForm();
                if (form != null) form.WindowState = FormWindowState.Minimized;
            };

            btnGear = new Button
            {
                Text = "\u2699",
                Location = new Point(parent.Width - 80, 10),
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = false,
            };
            btnGear.FlatAppearance.BorderSize = 0;
            btnGear.Click += BtnGear_Click;

            themeMenu = new ContextMenuStrip();
            themeMenu.BackColor = Theme.Current.ControlBackColor;
            themeMenu.ForeColor = Theme.Current.TextColor;
            var darkItem = themeMenu.Items.Add("Dark Theme");
            var lightItem = themeMenu.Items.Add("Light Theme");
            darkItem.Click += (s, e) =>
            {
                var form = parent.FindForm();
                if (form != null) Theme.SetTheme(form, ThemeType.Dark);
            };
            lightItem.Click += (s, e) =>
            {
                var form = parent.FindForm();
                if (form != null) Theme.SetTheme(form, ThemeType.Light);
            };

            btnClose = new Button
            {
                Text = "\u2715",
                Location = new Point(parent.Width - 40, 10),
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = false,
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnMinimize);
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
                    var form = this.FindForm();
                    if (form != null)
                    {
                        Point mousePos = Control.MousePosition;
                        mousePos.Offset(dragOffset.X, dragOffset.Y);
                        form.Location = mousePos;
                    }
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
            btnMinimize.ForeColor = Theme.Current.TextColor;
            btnGear.ForeColor = Theme.Current.TextColor;
            btnClose.ForeColor = Theme.Current.TextColor;
            RepositionTitle();
        }
    }
}
