using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AdvancedProgramming.Forms;

namespace AdvancedProgramming
{
    public partial class StartupForm : Form
    {
        private Toolbar toolbar;
        private Timer animTimer;
        private double animAngle = 0;
        private Label lblTitle;
        private Label lblSubtitle;
        private PictureBox logoBox;
        private Button btnLogin;
        private Button btnSignup;
        private Panel accentLine;

        public StartupForm()
        {
            InitializeComponent();
            DatabaseManager.InitializeDatabase();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(850, 520);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            InitializeSplashScreen();
            Theme.Apply(this);
            ApplyCustomStyles();
            StartAnimation();
        }

        private void InitializeSplashScreen()
        {
            int cx = this.ClientSize.Width / 2;

            logoBox = new PictureBox
            {
                Size = new Size(130, 130),
                Location = new Point(cx - 65, 70),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.FromArgb(108, 99, 255),
                Cursor = Cursors.Hand,
            };

            using (var bmp = new Bitmap(130, 130))
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(108, 99, 255));
                g.DrawString("🧩", new Font("Segoe UI", 48), Brushes.White, 28, 28);
                logoBox.Image = (Bitmap)bmp.Clone();
            }

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, 130, 130);
                logoBox.Region = new Region(path);
            }

            lblTitle = new Label
            {
                Text = "MiniCamp Puzzle",
                Font = new Font("Segoe UI", 34, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(850, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 220),
            };

            accentLine = new Panel
            {
                Size = new Size(80, 3),
                BackColor = Color.FromArgb(108, 99, 255),
                Location = new Point(cx - 40, 300),
            };

            lblSubtitle = new Label
            {
                Text ="Start with our App ",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(160, 160, 180),
                BackColor = Color.Transparent,
                AutoSize = true,
            };
            lblSubtitle.Location = new Point(cx - lblSubtitle.Width / 2, 320);

            btnLogin = new Button
            {
                Text = "Log In",
                Size = new Size(150, 48),
                Location = new Point(cx - 165, 380),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
            };

            btnSignup = new Button
            {
                Text = "Sign Up",
                Size = new Size(150, 48),
                Location = new Point(cx + 15, 380),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
            };

            btnLogin.Click += (s, e) => new LogInForm().ShowDialog();
            btnSignup.Click += (s, e) => new SignUpForm().ShowDialog();

            this.Controls.Add(logoBox);
            this.Controls.Add(lblTitle);
            this.Controls.Add(accentLine);
            this.Controls.Add(lblSubtitle);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnSignup);
        }

        private void ApplyCustomStyles()
        {
            accentLine.BackColor = Color.FromArgb(108, 99, 255);

            btnLogin.BackColor = Color.FromArgb(108, 99, 255);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 120, 255);

            btnSignup.BackColor = Color.Transparent;
            btnSignup.ForeColor = Color.FromArgb(108, 99, 255);
            btnSignup.FlatAppearance.BorderSize = 2;
            btnSignup.FlatAppearance.BorderColor = Color.FromArgb(108, 99, 255);
            btnSignup.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 100);
        }

        private void StartAnimation()
        {
            animTimer = new Timer();
            animTimer.Interval = 25;
            animTimer.Tick += (s, e) =>
            {
                animAngle += 0.04;
                int offset = (int)(Math.Sin(animAngle) * 8);
                lblTitle.Location = new Point(0, 220 + offset);
            };
            animTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            animTimer?.Stop();
            animTimer?.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }
    }
}
