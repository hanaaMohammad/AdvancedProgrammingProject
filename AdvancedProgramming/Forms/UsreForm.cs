using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AdvancedProgramming.Session;

namespace AdvancedProgramming.Forms
{
    internal class UsreForm : Form
    {
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label4;
        private Label label5;
        private Label label1;
        private Button btnHome;

        public UsreForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            AddHomeButton();
            Theme.Apply(this);
            label2.BackColor = Color.FromArgb(50, 40, 80);
            label1.BackColor = Color.FromArgb(28, 28, 44);
            label3.BackColor = Color.FromArgb(28, 28, 44);
        }

        private void AddHomeButton()
        {
            btnHome = new Button
            {
                Text = "🏠",
                Location = new Point(10, 10),
                Size = new Size(50, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16F),
                BackColor = Color.FromArgb(108, 99, 255),
                ForeColor = Color.White
            };
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.Click += BtnHome_Click;
            this.Controls.Add(btnHome);
            btnHome.BringToFront();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            var home = new HomeFarme();
            home.Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            this.label4 = new Label();
            this.label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            this.label1.BackColor = Color.FromArgb(64, 64, 64);
            this.label1.BorderStyle = BorderStyle.Fixed3D;
            this.label1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold | FontStyle.Italic);
            this.label1.ForeColor = Color.White;
            this.label1.Location = new Point(200, 106);
            this.label1.Name = "label1";
            this.label1.Size = new Size(326, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "usre name:";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;

            this.label2.BackColor = Color.Black;
            this.label2.Font = new Font("Arial", 12F, FontStyle.Bold);
            this.label2.ForeColor = Color.White;
            this.label2.Location = new Point(0, -1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(583, 90);
            this.label2.TabIndex = 3;
            this.label2.Text = "user acount";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;

            this.label3.BackColor = Color.FromArgb(64, 64, 64);
            this.label3.BorderStyle = BorderStyle.Fixed3D;
            this.label3.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold | FontStyle.Italic);
            this.label3.ForeColor = Color.White;
            this.label3.Location = new Point(200, 181);
            this.label3.Name = "label3";
            this.label3.Size = new Size(326, 56);
            this.label3.TabIndex = 2;
            this.label3.Text = "score this account";
            this.label3.TextAlign = ContentAlignment.MiddleCenter;

            this.pictureBox1.BackColor = Color.FromArgb(224, 224, 224);
            this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            this.pictureBox1.Location = new Point(3, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(178, 283);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;

            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(0, -7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(583, 413);
            this.panel1.TabIndex = 5;

            this.label4.BackColor = Color.FromArgb(64, 64, 64);
            this.label4.BorderStyle = BorderStyle.Fixed3D;
            this.label4.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold | FontStyle.Italic);
            this.label4.ForeColor = Color.White;
            this.label4.Location = new Point(200, 255);
            this.label4.Name = "label4";
            this.label4.Size = new Size(326, 50);
            this.label4.TabIndex = 5;
            this.label4.Text = "gender";
            this.label4.TextAlign = ContentAlignment.MiddleCenter;

            this.label5.BackColor = Color.FromArgb(64, 64, 64);
            this.label5.BorderStyle = BorderStyle.Fixed3D;
            this.label5.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold | FontStyle.Italic);
            this.label5.ForeColor = Color.White;
            this.label5.Location = new Point(200, 324);
            this.label5.Name = "label5";
            this.label5.Size = new Size(326, 43);
            this.label5.TabIndex = 6;
            this.label5.Text = "contry";
            this.label5.TextAlign = ContentAlignment.MiddleCenter;

            this.BackColor = Color.FromArgb(64, 0, 64);
            this.BackgroundImageLayout = ImageLayout.Center;
            this.ClientSize = new Size(581, 405);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "UsreForm";
            this.Load += Usre_Load;

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Usre_Load(object sender, EventArgs e)
        {
            string fileName = CurrentUser.Gender == "Female" ? "Fmale.jpg" : "Male.jpg";
            string imagePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Image\", fileName));
            if (File.Exists(imagePath))
                pictureBox1.Image = Image.FromFile(imagePath);
        }
    }
}
