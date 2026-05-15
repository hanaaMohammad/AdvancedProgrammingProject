using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace AdvancedProgramming
{
    public class StartupForm : UserControl
    {
        public event EventHandler LoginRequested;
        public event EventHandler SignUpRequested;

        private Toolbar toolbar;
        private Label lblTitle;
        private Label lblSubtitle;
        private PictureBox logoBox;
        private Button buttonLogin;
        private Button buttonSignup;
        private Panel accentLine;
    public StartupForm()
        {
            this.SuspendLayout();
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            this.Name = "StartupForm";

            InitializeSplashScreen();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            this.ResumeLayout(false);

            Theme.StylePage(this);
            ApplyCustomStyles();

            toolbar.CloseRequested += (s, e) => Application.Exit();
        }

        private void InitializeSplashScreen()
        {
            int cx = this.Width / 2;

            logoBox = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(cx - 60, 120),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
            };

            using (var bmp = new Bitmap(120, 120))
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(Theme.Current.AccentColor))
                {
                    g.FillEllipse(brush, 5, 5, 110, 110);
                }
                g.DrawString("\U0001f9e9", new Font("Segoe UI", 48), Brushes.White, 28, 28);
                logoBox.Image = (Bitmap)bmp.Clone();
            }

            lblTitle = new Label
            {
                Text = "MiniCamp Puzzle",
                Font = DesignTokens.Typography.DisplayLarge,
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(600, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(cx - 300, 260),
            };

            accentLine = new Panel
            {
                Size = new Size(60, 4),
                Location = new Point(cx - 30, 350),
                BackColor = Theme.Current.AccentColor,
            };

            lblSubtitle = new Label
            {
                Text = "Solve code challenges. Level up your skills.",
                Font = DesignTokens.Typography.BodyLarge,
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(500, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(cx - 250, 370),
                Tag = "Secondary",
            };

            buttonLogin = new Button
            {
                Text = "Log In",
                Size = new Size(DesignTokens.Sizing.ButtonWidthMd, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx - 190, 440),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = DesignTokens.Typography.ButtonLabel,
                Tag = "Primary",
            };

            buttonSignup = new Button
            {
                Text = "Sign Up",
                Size = new Size(DesignTokens.Sizing.ButtonWidthMd, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx + 10, 440),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = DesignTokens.Typography.ButtonLabel,
                Tag = "Secondary",
            };

            this.Controls.Add(logoBox);
            this.Controls.Add(lblTitle);
            this.Controls.Add(accentLine);
            this.Controls.Add(lblSubtitle);
            this.Controls.Add(buttonLogin);
            this.Controls.Add(buttonSignup);

            buttonLogin.Click += (s, e) => LoginRequested?.Invoke(this, EventArgs.Empty);
            buttonSignup.Click += (s, e) => SignUpRequested?.Invoke(this, EventArgs.Empty);

            FormAccessibility.SetShortcutHint(buttonLogin, "Enter", "Log in");
            FormAccessibility.SetShortcutHint(buttonSignup, "Tab", "Create an account");
        }

        private void ApplyCustomStyles()
        {
            accentLine.BackColor = Theme.Current.AccentColor;
            lblTitle.ForeColor = Theme.Current.TextColor;
            lblSubtitle.ForeColor = Theme.Current.SecondaryTextColor;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (lblTitle == null) return;
            int cx = this.Width / 2;
            logoBox.Location = new Point(cx - 60, 120);
            lblTitle.Location = new Point(cx - 300, 260);
            accentLine.Location = new Point(cx - 30, 350);
            lblSubtitle.Location = new Point(cx - 250, 370);
            buttonLogin.Location = new Point(cx - 190, 440);
            buttonSignup.Location = new Point(cx + 10, 440);
        }
    }
}
