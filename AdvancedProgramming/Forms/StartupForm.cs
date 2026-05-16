using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AdvancedProgramming.Components;
using AdvancedProgramming.Forms;

namespace AdvancedProgramming
{
    public class StartupForm : AppForm
    {
        private Toolbar toolbar;
        private Panel heroCard;
        private Panel loginPill;
        private Panel signUpPill;

        public StartupForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            int centerX = AppSizes.FormWidth / 2;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += Toolbar_CloseRequested;
            Controls.Add(toolbar);

            Color accent = AppColors.Accent;
            heroCard = UiHelper.CreateCard(Color.FromArgb(60, accent), 24);
            heroCard.SetBounds(centerX - 220, 200, 440, 320);

            PictureBox logo = CreateLogo();
            logo.Location = new Point(160, 28);

            var title = new Label
            {
                Text = "MiniCamp Puzzle",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 168),
                Size = new Size(440, 48),
                BackColor = Color.Transparent,
            };

            var accentLine = new Panel
            {
                Size = new Size(64, 4),
                Location = new Point(188, 224),
                BackColor = accent,
            };

            var subtitle = new Label
            {
                Text = "Solve code challenges. Level up your skills.",
                Font = new Font("Segoe UI", 12),
                ForeColor = AppColors.MutedText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 242),
                Size = new Size(400, 28),
                BackColor = Color.Transparent,
            };

            var tagline = new Label
            {
                Text = "Practice algorithms, run tests, and track your progress.",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.MutedText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 276),
                Size = new Size(400, 22),
                BackColor = Color.Transparent,
            };

            heroCard.Controls.Add(logo);
            heroCard.Controls.Add(title);
            heroCard.Controls.Add(accentLine);
            heroCard.Controls.Add(subtitle);
            heroCard.Controls.Add(tagline);
            Controls.Add(heroCard);

            loginPill = UiHelper.CreateActionPill("Log In \u2192", true, accent, LoginButton_Click);
            signUpPill = CreateSecondaryPill("Sign Up", accent, SignUpButton_Click);

            int pillY = 548;
            int gap = 16;
            int totalW = loginPill.Width + signUpPill.Width + gap;
            loginPill.Location = new Point(centerX - totalW / 2, pillY);
            signUpPill.Location = new Point(loginPill.Right + gap, pillY);
            Controls.Add(loginPill);
            Controls.Add(signUpPill);
        }

        private void Toolbar_CloseRequested(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            ShowOtherForm(new LogInForm());
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            ShowOtherForm(new SignUpForm());
        }

        private PictureBox CreateLogo()
        {
            var logoBox = new PictureBox
            {
                Size = new Size(120, 120),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
            };

            using (var bmp = new Bitmap(120, 120))
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(AppColors.Accent))
                    g.FillEllipse(brush, 5, 5, 110, 110);
                g.DrawString("\U0001f9e9", new Font("Segoe UI", 48), Brushes.White, 28, 28);
                logoBox.Image = (Bitmap)bmp.Clone();
            }

            return logoBox;
        }

        private Panel CreateSecondaryPill(string text, Color accent, EventHandler onClick)
        {
            var font = new Font("Segoe UI", 10, FontStyle.Bold);
            int w = Math.Max(120, TextRenderer.MeasureText(text, font).Width + 36);
            var pill = new Panel
            {
                Size = new Size(w, 40),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Tag = false,
            };

            pill.Paint += (s, e) =>
            {
                bool hover = (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = new GraphicsPath())
                {
                    int r = 16;
                    int d = r * 2;
                    path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                    path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                    path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();
                    Color fill = hover ? Color.FromArgb(32, 40, 58) : Color.FromArgb(22, 30, 48);
                    using (var brush = new SolidBrush(fill))
                    using (var pen = new Pen(Color.FromArgb(80, accent), 1.5f))
                    {
                        e.Graphics.FillPath(brush, path);
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            var label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = font,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
            };
            pill.Controls.Add(label);
            pill.MouseEnter += (s, e) => { pill.Tag = true; pill.Invalidate(); };
            pill.MouseLeave += (s, e) => { pill.Tag = false; pill.Invalidate(); };
            label.MouseEnter += (s, e) => { pill.Tag = true; pill.Invalidate(); };
            label.MouseLeave += (s, e) => { pill.Tag = false; pill.Invalidate(); };
            pill.Click += onClick;
            label.Click += onClick;
            return pill;
        }
    }
}
