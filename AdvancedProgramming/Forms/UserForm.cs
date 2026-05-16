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

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel profileCard;
        private PictureBox pictureUser;
        private Label labelUsername;

        public UserForm()
        {
            InitializeComponent();
            LoadUserImage();
        }

        private void InitializeComponent()
        {
            Color accent = AppColors.Accent;
            int contentW = AppSizes.FormWidth - SideMargin * 2;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button homeBtn = MakeNavButton("Home", 16, HomeButton_Click);
            Controls.Add(homeBtn);
            homeBtn.BringToFront();

            headerCard = UiHelper.CreateCard(Color.FromArgb(50, accent), 20);
            headerCard.Controls.Add(new Label
            {
                Text = "Your Profile",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 20),
                Size = new Size(400, 32),
                BackColor = Color.Transparent,
            });
            headerCard.Controls.Add(new Label
            {
                Text = "Track your progress and account details",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.MutedText,
                Location = new Point(24, 54),
                Size = new Size(500, 22),
                BackColor = Color.Transparent,
            });
            Controls.Add(headerCard);

            profileCard = UiHelper.CreateCard(AppColors.DefaultBorder, 20);
            BuildProfileCard();
            Controls.Add(profileCard);

            headerCard.SetBounds(SideMargin, AppSizes.ContentTop, contentW, 88);
            profileCard.SetBounds(SideMargin, 204, contentW, 564);
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowAsMainForm(new LevelProblemForm());
        }

        private void BuildProfileCard()
        {
            var avatarHost = new Panel
            {
                Size = new Size(160, 200),
                Location = new Point(28, 24),
                BackColor = Color.Transparent,
            };
            avatarHost.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 0, avatarHost.Width - 1, avatarHost.Height - 1);
                UiHelper.PaintInset(e.Graphics, inset, 14);
            };

            pictureUser = new PictureBox
            {
                Location = new Point(8, 8),
                Size = new Size(144, 184),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = AppColors.InsetBack,
            };
            avatarHost.Controls.Add(pictureUser);

            int infoX = 210;
            int infoW = 480;

            labelUsername = new Label
            {
                Text = CurrentUser.Username,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(infoX, 28),
                Size = new Size(infoW, 32),
                BackColor = Color.Transparent,
            };

            Panel scorePill = UiHelper.CreateStatusPill("Score: " + CurrentUser.Score, AppColors.Success);
            scorePill.Location = new Point(infoX, 72);

            AddInfoRow(profileCard, "Gender", CurrentUser.Gender, infoX, 120, infoW);
            AddInfoRow(profileCard, "Country", CurrentUser.Country, infoX, 168, infoW);

            profileCard.Controls.Add(avatarHost);
            profileCard.Controls.Add(labelUsername);
            profileCard.Controls.Add(scorePill);
        }

        private void AddInfoRow(Panel parent, string caption, string value, int x, int y, int width)
        {
            var row = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, 44),
                BackColor = Color.Transparent,
            };
            row.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 0, row.Width - 1, row.Height - 1);
                UiHelper.PaintInset(e.Graphics, inset, 10);
            };
            row.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(12, 6),
                AutoSize = true,
                BackColor = Color.Transparent,
            });
            row.Controls.Add(new Label
            {
                Text = value ?? "—",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                Location = new Point(12, 22),
                Size = new Size(width - 24, 18),
                BackColor = Color.Transparent,
            });
            parent.Controls.Add(row);
        }

        private void LoadUserImage()
        {
            string fileName = CurrentUser.Gender == "Female" ? "Female.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (File.Exists(imagePath))
            {
                pictureUser.Image = Image.FromFile(imagePath);
                return;
            }

            using (var bmp = new Bitmap(144, 184))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(AppColors.InsetBack);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(AppColors.Accent))
                    g.FillEllipse(brush, 32, 40, 80, 80);
                g.DrawString("\U0001f464", new Font("Segoe UI", 36), Brushes.White, 48, 58);
                pictureUser.Image = (Bitmap)bmp.Clone();
            }
        }
    }
}
