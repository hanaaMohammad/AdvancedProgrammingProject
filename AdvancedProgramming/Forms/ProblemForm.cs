using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AdvancedProgramming.Forms
{
    internal class ProblemForm : Form
    {
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private ListBox ListBox1;
        private Toolbar toolbar;

        public ProblemForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            this.label1.Font = new Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new Point(-1, 39);
            this.label1.Size = new Size(755, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "- problem solving -";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.Click += new EventHandler(this.label1_Click);

            this.label2.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.label2.Location = new Point(87, 150);
            this.label2.Size = new Size(567, 49);
            this.label2.TabIndex = 2;
            this.label2.Text = "Choose a level from the problem-solving levels";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            this.label2.Click += new EventHandler(this.label1_Click);

            this.button1.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button1.Location = new Point(257, 220);
            this.button1.Size = new Size(206, 69);
            this.button1.TabIndex = 3;
            this.button1.Text = "Easy";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);

            this.button2.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button2.Location = new Point(257, 310);
     
            this.button2.Size = new Size(206, 67);
            this.button2.TabIndex = 4;
            this.button2.Text = "Medium";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);

            this.button3.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button3.Location = new Point(257, 400);
     
            this.button3.Size = new Size(206, 66);
            this.button3.TabIndex = 5;
            this.button3.Text = "Hard";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);

            this.ListBox1.Location = new Point(50, 220);
            this.ListBox1.Size = new Size(180, 250);
            this.ListBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);

            this.ClientSize = new Size(755, 619);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
       

            Theme.Apply(this);
            this.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

     

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox1.Items.Add("Program to Print Full Pyramid Pattern (Star Pattern)");
            ListBox1.Items.Add("Program to find if a character is vowel or Consonant");
            ListBox1.Items.Add("");

        }
        private void button2_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox1.Items.Add("Floor in a Sorted Array");
            ListBox1.Items.Add("");
            ListBox1.Items.Add("");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox1.Items.Add("");
            ListBox1.Items.Add("");
            ListBox1.Items.Add("");

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem == null) return;
        

            string problem = ListBox1.SelectedItem.ToString();

            ProblemDetailsForm form = new ProblemDetailsForm(problem);
            form.Show();
        }
        }

}
