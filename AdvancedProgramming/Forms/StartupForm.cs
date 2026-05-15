using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AdvancedProgramming.Forms;

namespace AdvancedProgramming
{
    public class StartupForm : UserControl
    {
        public event EventHandler LoginRequested;
        public event EventHandler SignUpRequested;

        private Toolbar toolbar;
        private Timer animTimer;
        private double animAngle = 0;
        private Label lblTitle;
        private Label lblSubtitle;
        private PictureBox logoBox;
        private Button buttonLogin;
        private Button buttonSignup;
        private Panel accentLine;

        public StartupForm()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1100, 800);
            this.Name = "StartupForm";

            InitializeSplashScreen();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            this.ResumeLayout(false);

            Theme.Apply(this);
            ApplyCustomStyles();
            foreach (Control c in toolbar.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatAppearance.BorderSize = 0;
                }
            }
            StartAnimation();

            toolbar.CloseRequested += (s, e) => Application.Exit();
            Theme.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) =>
            {
                Theme.ThemeChanged -= OnThemeChanged;
                if (animTimer != null)
                {
                    animTimer.Stop();
                    animTimer.Dispose();
                }
            };
        }

        private void OnThemeChanged()
        {
            ApplyCustomStyles();
            foreach (Control c in toolbar.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatAppearance.BorderSize = 0;
                }
            }
            toolbar.UpdateTheme();
        }

        private void InitializeSplashScreen()
        {
            int cx = this.Width / 2;

            logoBox = new PictureBox
            {
                Size = new Size(130, 130),
                Location = new Point(cx - 65, 70),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
            };

            using (var bmp = new Bitmap(130, 130))
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(Theme.Current.AccentColor))
                {
                    g.FillEllipse(brush, 5, 5, 120, 120);
                }
                g.DrawString("\U0001f9e9", new Font("Segoe UI", 52), Brushes.White, 33, 33);
                logoBox.Image = (Bitmap)bmp.Clone();
            }

            lblTitle = new Label
            {
                Text = "MiniCamp Puzzle",
                Font = new Font("Segoe UI", 34, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(850, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 220),
            };

            accentLine = new Panel
            {
                Size = new Size(80, 4),
                Location = new Point(cx - 40, 300),
            };

            lblSubtitle = new Label
            {
                Text = "Start with our App",
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                BackColor = Color.Transparent,
                AutoSize = true,
                Tag = "Secondary",
            };

            buttonLogin = new Button
            {
                Text = "Log In",
                Size = new Size(170, 52),
                Location = new Point(cx - 180, 380),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
            };

            buttonSignup = new Button
            {
                Text = "Sign Up",
                Size = new Size(170, 52),
                Location = new Point(cx + 10, 380),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
            };

            this.Controls.Add(logoBox);
            this.Controls.Add(lblTitle);
            this.Controls.Add(accentLine);
            this.Controls.Add(buttonLogin);
            this.Controls.Add(buttonSignup);
            buttonLogin.Click += (s, e) => LoginRequested?.Invoke(this, EventArgs.Empty);
            buttonSignup.Click += (s, e) => SignUpRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyCustomStyles()
        {
            accentLine.BackColor = Theme.Current.AccentColor;

            buttonLogin.BackColor = Theme.Current.AccentColor;
            buttonLogin.ForeColor = Color.White;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatAppearance.MouseOverBackColor = Theme.Current.ButtonHoverBackColor;

            buttonSignup.BackColor = Color.Transparent;
            buttonSignup.ForeColor = Theme.Current.AccentColor;
            buttonSignup.FlatAppearance.BorderSize = 2;
            buttonSignup.FlatAppearance.BorderColor = Theme.Current.AccentColor;
            buttonSignup.FlatAppearance.MouseOverBackColor = Theme.Current.ButtonHoverBackColor;
        }

        private void StartAnimation()
        {
            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += (s, e) =>
            {
                animAngle += 0.04;
                int offset = (int)(Math.Sin(animAngle) * 6);
                lblTitle.Location = new Point(0, 220 + offset);
            };
            animTimer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            int cx = this.Width / 2;
            lblSubtitle.Location = new Point(cx - lblSubtitle.Width / 2, 320);
            this.Controls.Add(lblSubtitle);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (lblTitle == null) return;
            int cx = this.Width / 2;
            logoBox.Location = new Point(cx - 65, 70);
            lblTitle.Size = new Size(this.Width, 70);
            accentLine.Location = new Point(cx - 40, 300);
            lblSubtitle.Location = new Point(cx - lblSubtitle.Width / 2, 320);
            buttonLogin.Location = new Point(cx - 180, 380);
            buttonSignup.Location = new Point(cx + 10, 380);
        }
    }
}
