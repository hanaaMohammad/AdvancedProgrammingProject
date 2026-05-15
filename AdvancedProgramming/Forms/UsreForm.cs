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
        private Panel panel1;
        private Label labelGender;
        private Label labelCountry;
        private Label labelUsreName;
        private Button btnHome;
        private Toolbar toolbar;

        public UserForm()
        {
            this.Size = new Size(1100, 800);
            InitializeComponent();
            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);
            AddHomeButton();
            Theme.Apply(this);
        }

        private void AddHomeButton()
        {
            btnHome = new Button
            {
                Text = "\U0001f3e0",
                Location = new Point(10, 55),
                Size = new Size(50, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16F),
            };
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.Click += BtnHome_Click;
            this.Controls.Add(btnHome);
            btnHome.BringToFront();
        }

        private void InitializeComponent()
        {
            int cx = this.Width / 2;
            this.SuspendLayout();

            labelUsreName = new Label()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(cx - 150, 106),
                Size = new Size(400, 55),
                TabIndex = 0,
                Text = "Name : " + CurrentUser.Username,
                TextAlign = ContentAlignment.MiddleCenter
            };

            labelTitle = new Label()
            {
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(0, 55),
                Size = new Size(1100, 45),
                TabIndex = 3,
                Text = "User Profile",
                TextAlign = ContentAlignment.MiddleCenter
            };

            labelScore = new Label()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(cx - 150, 181),
                Size = new Size(400, 56),
                TabIndex = 2,
                Text = "Score : " + CurrentUser.Score.ToString(),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.pictureUser = new PictureBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(60, 84),
                Size = new Size(178, 283),
                TabIndex = 4,
                TabStop = false,
            };

            this.panel1 = new Panel();
            this.panel1.SuspendLayout();

            panel1.Location = new System.Drawing.Point(0, 10);
            panel1.Size = new Size(1100, 750);
            panel1.TabIndex = 5;

            labelCountry = new Label()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(cx - 150, 324),
                Size = new Size(400, 43),
                TabIndex = 6,
                Text = "Country : " + CurrentUser.Country,
                TextAlign = ContentAlignment.MiddleCenter
            };

            labelGender = new Label()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(cx - 150, 255),
                Size = new Size(400, 50),
                TabIndex = 5,
                Text = "gender : " + CurrentUser.Gender,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel1.Controls.Add(labelCountry);
            panel1.Controls.Add(labelGender);
            panel1.Controls.Add(pictureUser);
            panel1.Controls.Add(labelScore);
            panel1.Controls.Add(labelUsreName);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);

            this.BackgroundImageLayout = ImageLayout.Center;

            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            UsreLoad();
        }

        private void UsreLoad()
        {
            string fileName = CurrentUser.Gender == "Female" ? "Famale.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (File.Exists(imagePath))
                pictureUser.Image = Image.FromFile(imagePath);
            pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            HomeRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
