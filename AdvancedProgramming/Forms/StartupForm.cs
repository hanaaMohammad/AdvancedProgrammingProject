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
        private const int SideMargin = 40;

        private Toolbar toolbar;
        private Panel heroCard;
        private Panel loginPill;
        private Panel signUpPill;

        public void FocusLogin() => loginPill?.Focus();

        public StartupForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FocusLogin();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitializeComponent()
        {
            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            var accent = Theme.Current.AccentColor;
            heroCard = CatalogUi.CreateCard(Color.FromArgb(60, accent), 24);

            var logo = CreateLogo();
            logo.Location = new Point(0, 28);

            var title = new Label
            {
                Text = "MiniCamp Puzzle",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 168),
                Size = new Size(440, 48),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var accentLine = new Panel
            {
                Size = new Size(64, 4),
                Location = new Point(188, 224),
                BackColor = accent,
                Tag = "NoTheme",
            };

            var subtitle = new Label
            {
                Text = "Solve code challenges. Level up your skills.",
                Font = new Font("Segoe UI", 12),
                ForeColor = CatalogUi.MutedText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 242),
                Size = new Size(400, 28),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var tagline = new Label
            {
                Text = "Practice algorithms, run tests, and track your progress.",
                Font = new Font("Segoe UI", 9),
                ForeColor = CatalogUi.MutedText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 276),
                Size = new Size(400, 22),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            heroCard.Controls.Add(logo);
            heroCard.Controls.Add(title);
            heroCard.Controls.Add(accentLine);
            heroCard.Controls.Add(subtitle);
            heroCard.Controls.Add(tagline);
            Controls.Add(heroCard);

            loginPill = CatalogUi.CreateActionPill(
                "Log In \u2192",
                true,
                accent,
                (s, e) => ShowScreen(new LogInForm()));
            loginPill.TabStop = true;

            signUpPill = CreateSecondaryPill("Sign Up", accent, (s, e) => ShowScreen(new SignUpForm()));
            Controls.Add(loginPill);
            Controls.Add(signUpPill);

            FormAccessibility.SetShortcutHint(loginPill, "Enter", "Log in");
            FormAccessibility.SetShortcutHint(signUpPill, "Tab", "Create an account");

            ResumeLayout(false);
            ApplyLayout();
        }

        private static PictureBox CreateLogo()
        {
            var logoBox = new PictureBox
            {
                Size = new Size(120, 120),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            using (var bmp = new Bitmap(120, 120))
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(Theme.Current.AccentColor))
                    g.FillEllipse(brush, 5, 5, 110, 110);
                g.DrawString("\U0001f9e9", new Font("Segoe UI", 48), Brushes.White, 28, 28);
                logoBox.Image = (Bitmap)bmp.Clone();
            }

            return logoBox;
        }

        private static Panel CreateSecondaryPill(string text, Color accent, EventHandler onClick)
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
            CatalogUi.EnableDoubleBuffer(pill);

            pill.Paint += (s, e) =>
            {
                bool hover = (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = GraphicsHelper.RoundedRect(rect, 16))
                {
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
                Tag = "NoTheme",
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (heroCard == null)
                return;

            int cardW = Math.Min(440, Width - SideMargin * 2);
            int cx = Width / 2;
            int cardH = 320;
            int cardY = Math.Max(100, (Height - cardH - 80) / 2);

            heroCard.SetBounds(cx - cardW / 2, cardY, cardW, cardH);

            if (heroCard.Controls.Count > 0 && heroCard.Controls[0] is PictureBox logo)
                logo.Location = new Point((cardW - logo.Width) / 2, 28);

            if (heroCard.Controls.Count > 1 && heroCard.Controls[1] is Label title)
            {
                title.Width = cardW;
                title.Location = new Point(0, 168);
            }

            if (heroCard.Controls.Count > 2 && heroCard.Controls[2] is Panel line)
                line.Location = new Point((cardW - line.Width) / 2, 224);

            int pillY = heroCard.Bottom + 28;
            int gap = 16;
            int totalW = loginPill.Width + signUpPill.Width + gap;
            loginPill.Location = new Point(cx - totalW / 2, pillY);
            signUpPill.Location = new Point(loginPill.Right + gap, pillY);
        }
    }
}
