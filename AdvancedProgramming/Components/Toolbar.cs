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
        private Control parentForm;

        public Toolbar(Control parent, string title)
        {
            parentForm = parent;
            this.Height = 52;
            this.Dock = DockStyle.Top;
            this.BackColor = Theme.Current.ToolbarBackColor;
            this.ForeColor = Theme.Current.TextColor;
            this.Tag = "NoTheme";

            titleLabel = new Label
            {
                Text = title,
                AutoSize = true,
                Font = DesignTokens.Typography.HeadingSmall,
                ForeColor = Theme.Current.TextColor,
                BackColor = Color.Transparent,
            };

            btnMinimize = CreateWindowButton("\u2014", parent.Width - 126);
            btnMinimize.Click += (s, e) =>
            {
                var form = this.FindForm();
                if (form != null) form.WindowState = FormWindowState.Minimized;
            };

            btnGear = CreateWindowButton("\u2699", parent.Width - 84);
            btnGear.Click += BtnGear_Click;

            btnClose = CreateWindowButton("\u2715", parent.Width - 42);
            btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);

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

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnMinimize);
            this.Controls.Add(btnGear);
            this.Controls.Add(btnClose);

            this.Resize += (s, e) => RepositionControls();

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

            RepositionControls();
        }

        private Button CreateWindowButton(string text, int x)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, 8),
                Size = new Size(DesignTokens.Sizing.IconButtonSize, DesignTokens.Sizing.IconButtonSize),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.Current.TextColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = false,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Tag = "Ghost",
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void RepositionControls()
        {
            titleLabel.Location = new Point(DesignTokens.Spacing.Lg, (this.Height - titleLabel.Height) / 2);
            btnMinimize.Location = new Point(this.Width - 126, 7);
            btnGear.Location = new Point(this.Width - 84, 7);
            btnClose.Location = new Point(this.Width - 42, 7);
        }

        private void BtnGear_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in themeMenu.Items)
            {
                item.ForeColor = Theme.Current.TextColor;
            }
            themeMenu.Show(Cursor.Position);
        }

        public void UpdateTheme()
        {
            this.BackColor = Theme.Current.ToolbarBackColor;
            titleLabel.ForeColor = Theme.Current.TextColor;
            btnMinimize.ForeColor = Theme.Current.TextColor;
            btnGear.ForeColor = Theme.Current.TextColor;
            btnClose.ForeColor = Theme.Current.TextColor;
            themeMenu.BackColor = Theme.Current.ControlBackColor;
            themeMenu.ForeColor = Theme.Current.TextColor;
            RepositionControls();
        }
    }
}
