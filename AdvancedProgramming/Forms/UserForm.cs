using AdvancedProgramming.Session;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class UserForm : UserControl
    {
        public event EventHandler HomeRequested;

        private Label labelScore;
        private Label labelTitle;
        private PictureBox pictureUser;
        private Label labelGender;
        private Label labelCountry;
        private Label labelUsername;
        private Button btnHome;
        private Toolbar toolbar;

        public UserForm()
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent();
            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);
            AddHomeButton();
            Theme.StylePage(this);
        }

        private void AddHomeButton()
        {
            btnHome = new Button
            {
                Text = "\U0001f3e0",
                Location = new Point(DesignTokens.Spacing.Md, toolbar.Height + DesignTokens.Spacing.Sm),
                Size = new Size(DesignTokens.Sizing.IconButtonSize, DesignTokens.Sizing.IconButtonSize),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16F),
                Tag = "Ghost",
            };
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(btnHome);
            btnHome.BringToFront();
        }

        private void InitializeComponent()
        {
            int cx = this.Width / 2;
            int cardW = 480;
            int cardX = cx - cardW / 2;

            labelTitle = new Label
            {
                Font = DesignTokens.Typography.HeadingMedium,
                Location = new Point(0, 65),
                Size = new Size(this.Width, 45),
                Text = "User Profile",
                TextAlign = ContentAlignment.MiddleCenter,
            };

            var cardPanel = new Panel
            {
                Location = new Point(cardX, 130),
                Size = new Size(cardW, 400),
                Tag = "Card",
            };

            pictureUser = new PictureBox
            {
                Location = new Point(30, 30),
                Size = new Size(150, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Theme.Current.InputBackColor,
            };

            int labelX = 200;
            int labelW = 250;

            labelUsername = new Label
            {
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(labelX, 30),
                Size = new Size(labelW, 30),
                Text = CurrentUser.Username,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            labelScore = new Label
            {
                Font = DesignTokens.Typography.BodyLarge,
                Location = new Point(labelX, 75),
                Size = new Size(labelW, 28),
                Text = "Score: " + CurrentUser.Score,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "Secondary",
            };

            labelGender = new Label
            {
                Font = DesignTokens.Typography.BodyLarge,
                Location = new Point(labelX, 115),
                Size = new Size(labelW, 28),
                Text = "Gender: " + CurrentUser.Gender,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "Secondary",
            };

            labelCountry = new Label
            {
                Font = DesignTokens.Typography.BodyLarge,
                Location = new Point(labelX, 155),
                Size = new Size(labelW, 28),
                Text = "Country: " + CurrentUser.Country,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "Secondary",
            };

            cardPanel.Controls.Add(pictureUser);
            cardPanel.Controls.Add(labelUsername);
            cardPanel.Controls.Add(labelScore);
            cardPanel.Controls.Add(labelGender);
            cardPanel.Controls.Add(labelCountry);

            this.Controls.Add(labelTitle);
            this.Controls.Add(cardPanel);

            LoadUserImage();
        }

        private void LoadUserImage()
        {
            string fileName = CurrentUser.Gender == "Female" ? "Female.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (!File.Exists(imagePath))
            {
                string altPath = Path.Combine(Application.StartupPath, "Image", fileName);
                if (File.Exists(altPath))
                    imagePath = altPath;
            }
            if (File.Exists(imagePath))
            {
                try
                {
                    pictureUser.Image = Image.FromFile(imagePath);
                }
                catch { }
            }
        }
    }
}
