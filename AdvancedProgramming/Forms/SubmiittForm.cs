using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace AdvancedProgramming.Forms
{
    internal class SubmiittForm : Form
    {
        public SubmiittForm(string problemName)
        {
            InitializeComponent();

            labelNameproblem.Text = problemName;
        }
        private Button buttonHome;
        private Button buttonRun;
        private Label labelNameproblem;
        private Label labeIndtwrite;
        private ComboBox comboBoxTybeLang;
        private TextBox textBoxCode;
        private Button buttonBack;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // button1
            this.buttonHome = new System.Windows.Forms.Button()
            {
                BackColor = System.Drawing.Color.RosyBrown,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(13, 13),
                Name = "button1",
                Size = new System.Drawing.Size(75, 50),
                TabIndex = 0,
                Text = "Home",
                UseVisualStyleBackColor = false
            };
            this.buttonHome.Click += new EventHandler(this.buttonHome_Click);
            // 
            // button2
            // 
            this.buttonBack = new System.Windows.Forms.Button()
            {
                BackColor = System.Drawing.Color.RosyBrown,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(13, 474),
                Name = "button2",
                Size = new System.Drawing.Size(75, 57),
                TabIndex = 1,
                Text = "Back",
                UseVisualStyleBackColor = false
            };
            this.buttonBack.Click += new EventHandler(this.buttonBack_Click);

            // 
            // button3
            // 
            this.buttonRun = new System.Windows.Forms.Button()
            {
                BackColor = System.Drawing.Color.Gray,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(518, 485),
                Name = "button3",
                Size = new System.Drawing.Size(149, 46),
                TabIndex = 2,
                Text = "Run",
                UseVisualStyleBackColor = false
            };
            this.buttonRun.Click += new EventHandler(this.buttonRun_Click);
            // 
            // label1
            // 
            this.labelNameproblem = new System.Windows.Forms.Label()
            {
                BackColor = System.Drawing.SystemColors.ButtonHighlight,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(128, 23),
                Name = "label1",
                Size = new System.Drawing.Size(464, 40),
                TabIndex = 3,
                Text = "label1",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            // 
            // label2
            // 
            this.labeIndtwrite = new System.Windows.Forms.Label()
            {
                BackColor = System.Drawing.Color.RosyBrown,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(192, 103),
                Name = "label2",
                Size = new System.Drawing.Size(321, 34),
                TabIndex = 4,
                Text = "Enter the code here ⬇⬇",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            // 
            // comboBox1
            // 
            this.comboBoxTybeLang = new System.Windows.Forms.ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(131, 143),
                Name = "comboBox1",
                Text = "language type",
                Size = new System.Drawing.Size(133, 24),
                TabIndex = 5
            };
            this.comboBoxTybeLang.Items.Add("C#");
            this.comboBoxTybeLang.Items.Add("C++");
            this.comboBoxTybeLang.Items.Add("Java");
            // textBox1
            // 
            this.textBoxCode = new System.Windows.Forms.TextBox()
            {
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(131, 143),
                Multiline = true,
                Name = "textBox1",
                Size = new System.Drawing.Size(461, 344),
                TabIndex = 6,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            };
            // 
            // submmitForm
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(808, 549);
            this.Controls.Add(this.comboBoxTybeLang);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.labeIndtwrite);
            this.Controls.Add(this.labelNameproblem);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonHome);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "submmitForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void buttonHome_Click(object sender, EventArgs e)
        {
            HomeFarme form = new HomeFarme();
            form.Show();
            this.Hide();
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            LevelProblemForm form = new LevelProblemForm();
            form.Show();
            this.Hide();
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {

        }
    }
}