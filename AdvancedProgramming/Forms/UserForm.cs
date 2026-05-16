using AdvancedProgramming.Components;
using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class UserForm : AppForm
    {
        private const int SideMargin = 40;
        private const int HeaderTop = CatalogUi.ContentTop;

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel profileCard;
        private PictureBox pictureUser;
        private Label labelUsername;
        public UserForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var accent = Theme.Current.AccentColor;
            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            PageBackButton.AddHome(this, (s, e) => GoAppHome());

            headerCard = CatalogUi.CreateCard(Color.FromArgb(50, accent), 20);
            headerCard.Controls.Add(new Label
            {
                Text = "Your Profile",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 20),
                Size = new Size(400, 32),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            headerCard.Controls.Add(new Label
            {
                Text = "Track your progress and account details",
                Font = new Font("Segoe UI", 10),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(24, 54),
                Size = new Size(500, 22),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            Controls.Add(headerCard);

            profileCard = CatalogUi.CreateCard(CatalogUi.DefaultBorder, 20);
            BuildProfileCard();
            Controls.Add(profileCard);

            ResumeLayout(false);
            ApplyLayout();
            LoadUserImage();
        }

        private void BuildProfileCard()
        {
            var avatarHost = new Panel
            {
                Size = new Size(160, 200),
                Location = new Point(28, 24),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(avatarHost);
            avatarHost.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 0, avatarHost.Width - 1, avatarHost.Height - 1);
                CatalogUi.PaintInset(e.Graphics, inset, 14);
            };

            pictureUser = new PictureBox
            {
                Location = new Point(8, 8),
                Size = new Size(144, 184),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = CatalogUi.InsetBack,
                Tag = "NoTheme",
            };
            avatarHost.Controls.Add(pictureUser);

            int infoX = 210;
            int infoW = 480;

            labelUsername = CreateInfoLabel(CurrentUser.Username, new Font("Segoe UI", 18, FontStyle.Bold), infoX, 28, infoW, 32, Color.White);

            var scorePill = CatalogUi.CreateStatusPill("Score: " + CurrentUser.Score, Theme.Current.SuccessColor);
            scorePill.Location = new Point(infoX, 72);

            AddInfoRow(profileCard, "Gender", CurrentUser.Gender, infoX, 120, infoW);
            AddInfoRow(profileCard, "Country", CurrentUser.Country, infoX, 168, infoW);

            profileCard.Controls.Add(avatarHost);
            profileCard.Controls.Add(labelUsername);
            profileCard.Controls.Add(scorePill);
        }

        private static Label CreateInfoLabel(string text, Font font, int x, int y, int w, int h, Color color)
        {
            return new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
        }

        private static void AddInfoRow(Panel parent, string caption, string value, int x, int y, int width)
        {
            var row = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, 44),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(row);
            row.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 0, row.Width - 1, row.Height - 1);
                CatalogUi.PaintInset(e.Graphics, inset, 10);
            };
            row.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(12, 6),
                AutoSize = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            row.Controls.Add(new Label
            {
                Text = value ?? "—",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                Location = new Point(12, 22),
                Size = new Size(width - 24, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            parent.Controls.Add(row);
        }

        private void LoadUserImage()
        {
            string fileName = CurrentUser.Gender == "Female" ? "Female.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (File.Exists(imagePath))
                pictureUser.Image = Image.FromFile(imagePath);
            else
            {
                using (var bmp = new Bitmap(144, 184))
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(CatalogUi.InsetBack);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(Theme.Current.AccentColor))
                        g.FillEllipse(brush, 32, 40, 80, 80);
                    g.DrawString("\U0001f464", new Font("Segoe UI", 36), Brushes.White, 48, 58);
                    pictureUser.Image = (Bitmap)bmp.Clone();
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (headerCard == null)
                return;

            int contentW = Math.Max(560, Width - SideMargin * 2);
            int cx = Width / 2;
            int left = cx - contentW / 2;

            headerCard.SetBounds(left, HeaderTop, contentW, 88);

            int profileTop = headerCard.Bottom + 16;
            int profileH = Math.Max(260, Height - profileTop - 32);
            profileCard.SetBounds(left, profileTop, contentW, profileH);

            if (profileCard.Controls.Count > 0 && profileCard.Controls[0] is Panel avatar)
            {
                int infoX = 210;
                int infoW = Math.Max(200, contentW - infoX - 28);
                if (profileCard.Controls.Count > 1 && profileCard.Controls[1] is Label user)
                    user.Width = infoW;
            }
        }
    }
}
