using System;
using System.Drawing;
using System.Windows.Forms;


namespace AdvancedProgramming
{
    public class Toolbar : Panel
    {
        public event EventHandler CloseRequested;

        private Button btnMinimize;
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

            btnMinimize = CreateWindowButton("\u2014", parent.Width - 84);
            btnMinimize.Click += (s, e) =>
            {
                var form = this.FindForm();
                if (form != null) form.WindowState = FormWindowState.Minimized;
            };

            btnClose = CreateWindowButton("\u2715", parent.Width - 42);
            btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnMinimize);
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
            btnMinimize.Location = new Point(this.Width - 84, 7);
            btnClose.Location = new Point(this.Width - 42, 7);
        }
    }
}
