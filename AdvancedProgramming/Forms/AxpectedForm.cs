using System;
using System.Drawing;
using System.Windows.Forms;
namespace AdvancedProgramming.Forms
{
    public class AxpectedForm : Form
    {
        private Panel panel1;
        private Label labelSumbb;
        private Button buttonHome;
        private Label label1;
        private PictureBox pictureBox1trusoliotion;
        private Toolbar toolbar;
        private Button backButton;

        public AxpectedForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxpectedForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSumbb = new System.Windows.Forms.Label();
            this.pictureBox1trusoliotion = new System.Windows.Forms.PictureBox();
            this.buttonHome = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1trusoliotion)).BeginInit();
            this.SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            backButton = new Button
            {
                Text = "Back",
                Location = new Point(390, 666),
                Size = new Size(90, 58),
                FlatStyle = FlatStyle.Flat,
            };
            backButton.Click += (s, e) => this.Close();
            this.Controls.Add(backButton);

            // panel1
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.labelSumbb);
            this.panel1.Controls.Add(this.pictureBox1trusoliotion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1882, 340);
            this.panel1.TabIndex = 0;

            // labelSumbb
            this.labelSumbb.BackColor = System.Drawing.Color.Black;
            this.labelSumbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSumbb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelSumbb.Location = new System.Drawing.Point(261, 48);
            this.labelSumbb.Name = "labelSumbb";
            this.labelSumbb.Size = new System.Drawing.Size(529, 81);
            this.labelSumbb.TabIndex = 1;
            this.labelSumbb.Text = "-------Submmit✅🎉------";
            this.labelSumbb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pictureBox1trusoliotion
            this.pictureBox1trusoliotion.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1trusoliotion.Image")));
            this.pictureBox1trusoliotion.Location = new System.Drawing.Point(340, 59);
            this.pictureBox1trusoliotion.Name = "pictureBox1trusoliotion";
            this.pictureBox1trusoliotion.Size = new System.Drawing.Size(367, 281);
            this.pictureBox1trusoliotion.TabIndex = 2;
            this.pictureBox1trusoliotion.TabStop = false;
            this.pictureBox1trusoliotion.Click += new System.EventHandler(this.pictureBox1_Click);

            // buttonHome
            this.buttonHome.Location = new System.Drawing.Point(498, 666);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(90, 58);
            this.buttonHome.TabIndex = 1;
            this.buttonHome.Text = "HOME";
            this.buttonHome.UseVisualStyleBackColor = true;
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);

            // label1
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(203, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(646, 139);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code executed successfully! Your program has run without any errors. All operatin" +
    "s were completed as expected.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // AxpectedForm
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1882, 1055);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonHome);
            this.Controls.Add(this.panel1);
            this.Name = "AxpectedForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1trusoliotion)).EndInit();
            this.ResumeLayout(false);

            Theme.Apply(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {

        }
        private void buttonHome_Click(object sender, System.EventArgs e) { }

    }
}
