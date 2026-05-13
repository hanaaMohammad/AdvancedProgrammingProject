using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace AdvancedProgramming.Forms
{
    internal class UsreForm : Form
    {
        private Label labelScore;
        private Label labelTitle;
        private PictureBox pictureUser;
        private Panel panel1;
        private Label labelGender;
        private Label labelCountry;
        private Label labelUsreName;
        private Button btnHome;
        private Toolbar toolbar;

        public UsreForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            labelUsreName.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            AddHomeButton();
            Theme.Apply(this);
        }

        private void AddHomeButton()
        {
            btnHome = new Button
            {
                Text = "🏠",
                Location = new Point(10, 50),
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
            



            this.SuspendLayout();
            labelUsreName = new Label()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
                ForeColor = Color.White,
                Location = new Point(200, 106),
                Size = new Size(326, 55),
                TabIndex = 0,
                Text = "Name : " + CurrentUser.Username,
                TextAlign = ContentAlignment.MiddleCenter
            };
        labelUsreName.Click += new EventHandler(label1_Click);

            labelTitle = new Label() {
        BackColor = Color.Black,
             Font = new Font("Arial", 12F, System.Drawing.FontStyle.Bold),
           ForeColor = Color.White,
           Location = new Point(0, -1),

           Size = new Size(583, 90),
          TabIndex = 3,
              Text = "Profile User",
               TextAlign = ContentAlignment.MiddleCenter };
            labelScore = new Label() {
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
                ForeColor = Color.White,
                Location = new Point(200, 181),

                Size = new Size(326, 56),
                TabIndex = 2,
                Text = "Score : " + CurrentUser.score.ToString(),
                TextAlign = ContentAlignment.MiddleCenter };

            this.pictureUser = new PictureBox()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(3, 84),
              
                Size = new Size(178, 283),
                TabIndex = 4,
                TabStop = false,
            };
               this.panel1 = new Panel();
 
            this.panel1.SuspendLayout();

         
            panel1.Location = new System.Drawing.Point(0, -7);
            panel1.Size = new Size(583, 413);
            panel1.TabIndex = 5;
            labelCountry = new Label() {
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
                ForeColor = Color.White,
                Location = new Point(200, 324),
               Size = new Size(326, 43),
                TabIndex = 6,
               Text = "Country : " + CurrentUser.Country,
               TextAlign = ContentAlignment.MiddleCenter };



            labelGender = new Label() {
            BorderStyle = BorderStyle.Fixed3D,
              Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)))),
             ForeColor = Color.White,
             Location = new Point(200, 255),
                Size = new Size(326, 50),
               TabIndex = 5,
              Text = "gender : " + CurrentUser.Gender  ,
               TextAlign = ContentAlignment.MiddleCenter };

            panel1.Controls.Add(labelCountry);
            panel1.Controls.Add(labelGender);
            panel1.Controls.Add(pictureUser);
            panel1.Controls.Add(labelScore);
            panel1.Controls.Add(labelUsreName);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
        
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.ClientSize = new Size(581, 405);

            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            UsreLoad();


        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

        private void UsreLoad()
        {
            string fileName = CurrentUser.Gender == "Female" ? "Famale.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (File.Exists(imagePath))
                pictureUser.Image = Image.FromFile(imagePath);
            pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            var home = new HomeFarme();
            home.Show();
            this.Close();
        }
    }
}
