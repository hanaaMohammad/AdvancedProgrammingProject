using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    internal class UsreForm : Form
    {
        private Label labelScore;
        private Label labelTitle;
        private PictureBox pictureBoxUser;
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
        
            this.labelUsreName = new Label();
            this.labelTitle = new Label();
            this.labelScore = new Label();
           
            this.panel1 = new Panel();
            this.labelCountry = new Label();
            this.labelGender = new Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
           
            this.labelUsreName.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelUsreName.BorderStyle =BorderStyle.Fixed3D;
            this.labelUsreName.Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelUsreName.ForeColor = Color.White;
            this.labelUsreName.Location = new Point(200, 106);
            this.labelUsreName.Size = new Size(326, 55);
            this.labelUsreName.TabIndex = 0;
            this.labelUsreName.Text = CurrentUser.Username;
            this.labelUsreName.TextAlign = ContentAlignment.MiddleCenter;
            this.labelUsreName.Click += new EventHandler(this.label1_Click);
         
            this.labelTitle.BackColor = Color.Black;
            this.labelTitle.Font = new Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = Color.White;
            this.labelTitle.Location = new Point(0, -1);
          
            this.labelTitle.Size = new Size(583, 90);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Profile User";
            this.labelTitle.TextAlign =ContentAlignment.MiddleCenter;
         
            this.labelScore.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelScore.BorderStyle = BorderStyle.Fixed3D;
            this.labelScore.Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelScore.ForeColor = Color.White;
            this.labelScore.Location = new Point(200, 181);
           
            this.labelScore.Size = new Size(326, 56);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text =CurrentUser.score.ToString();
            this.labelScore.TextAlign = ContentAlignment.MiddleCenter;

            this.pictureBoxUser = new PictureBox()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(3, 84),
              
                Size = new Size(178, 283),
                TabIndex = 4,
                TabStop = false,
            };
         UsreLoad();


            this.panel1.Controls.Add(this.labelCountry);
            this.panel1.Controls.Add(this.labelGender);
            this.panel1.Controls.Add(this.pictureBoxUser);
            this.panel1.Controls.Add(this.labelScore);
            this.panel1.Controls.Add(this.labelUsreName);
            this.panel1.Location = new System.Drawing.Point(0, -7);
            this.panel1.Size = new Size(583, 413);
            this.panel1.TabIndex = 5;
         
            this.labelCountry.BorderStyle = BorderStyle.Fixed3D;
            this.labelCountry.Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelCountry.ForeColor = Color.White;
            this.labelCountry.Location = new Point(200, 324);
            this.labelCountry.Size = new Size(326, 43);
            this.labelCountry.TabIndex = 6;
            this.labelCountry.Text = "contry";
            this.labelCountry.TextAlign = ContentAlignment.MiddleCenter;
         
            this.labelGender.BorderStyle = BorderStyle.Fixed3D;
            this.labelGender.Font = new Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelGender.ForeColor = Color.White;
            this.labelGender.Location = new Point(200, 255);
            this.labelGender.Name = "label4";
            this.labelGender.Size = new Size(326, 50);
            this.labelGender.TabIndex = 5;
            this.labelGender.Text = "gender";
            this.labelGender.TextAlign = ContentAlignment.MiddleCenter;
           
            this.BackgroundImageLayout = ImageLayout.Center;
            this.ClientSize = new Size(581, 405);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle =FormBorderStyle.None;
            
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
           
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
                pictureBoxUser.Image = Image.FromFile(imagePath);
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
