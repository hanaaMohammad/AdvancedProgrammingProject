using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class AcceptedForm : UserControl
    {
        public event EventHandler HomeRequested;
        public event EventHandler BackRequested;

        private Panel panel1;
        private Label labelSumbb;
        private Button buttonHome;
        private Label label1;
        private PictureBox pictureBox1trusoliotion;
        private Toolbar toolbar;
        private Button backButton;

        public AcceptedForm()
        {
            this.Size = new Size(1100, 800);
            InitializeComponent();
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            int cx = this.Width / 2;
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
                Location = new Point(cx - 95, 480),
                Size = new Size(90, 58),
                FlatStyle = FlatStyle.Flat,
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(backButton);

            this.panel1.Controls.Add(this.labelSumbb);
            this.panel1.Controls.Add(this.pictureBox1trusoliotion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 250);
            this.panel1.TabIndex = 0;

            this.labelSumbb.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.labelSumbb.Location = new System.Drawing.Point(cx - 264, 15);
            this.labelSumbb.Name = "labelSumbb";
            this.labelSumbb.Size = new System.Drawing.Size(529, 81);
            this.labelSumbb.TabIndex = 1;
            this.labelSumbb.Text = "-------Submmit\u2705\U0001f389------";
            this.labelSumbb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pictureBox1trusoliotion.Location = new System.Drawing.Point(cx - 183, 59);
            this.pictureBox1trusoliotion.Name = "pictureBox1trusoliotion";
            this.pictureBox1trusoliotion.Size = new System.Drawing.Size(367, 180);
            this.pictureBox1trusoliotion.TabIndex = 2;
            this.pictureBox1trusoliotion.TabStop = false;

            this.buttonHome.FlatStyle = FlatStyle.Flat;
            this.buttonHome.FlatAppearance.BorderSize = 0;
            this.buttonHome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.buttonHome.Location = new System.Drawing.Point(cx + 5, 480);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(90, 58);
            this.buttonHome.TabIndex = 1;
            this.buttonHome.Text = "HOME";
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);

            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(cx - 400, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 139);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code executed successfully! Your program has run without any errors. All operations were completed as expected.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonHome);
            this.Controls.Add(this.panel1);
            this.Name = "AcceptedForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1trusoliotion)).EndInit();
            this.ResumeLayout(false);

            Theme.Apply(this);
        }

        private void buttonHome_Click(object sender, System.EventArgs e)
        {
            HomeRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
