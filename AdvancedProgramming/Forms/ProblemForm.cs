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
        private ComboBox comboBox1;
        private Toolbar toolbar;
        private string currentDifficulty = "";
        private Label ColorDiscrip;
        private Label ColorRed;
        private Label ColorGreen;
        private Label ColorYellow;

        public ProblemForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
           
            this.SuspendLayout();

            this.ClientSize = new Size(755, 619);

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

           comboBox1 = new ComboBox{
             Location = new Point(490, 220),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false,
                DrawMode = DrawMode.OwnerDrawFixed };
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.DrawItem += new DrawItemEventHandler(this.comboBox1_DrawItem);
            ColorDiscrip = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(50, 250),
                Name = "ColorDiscrip",
                Size = new Size(150, 25),
                TabIndex = 8,
                Text = "Color Description:"
            };

            ColorRed = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(50, 330),
                Name = "ColorRed",
                Size = new Size(150, 25),
                TabIndex = 9,
                Text = "Red: Hard level"
            };

            ColorGreen = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(50, 400),
                Name = "ColorGreen",
                Size = new Size(150, 25),
                TabIndex = 10,
                Text = "Green: Easy level"
            };

            ColorYellow = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(50,500),
                Name = "ColorYellow",
                Size = new Size(150, 25),
                TabIndex = 11,
                Text = "Yellow: Medium level"
            };





            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(ColorDiscrip);
            this.Controls.Add(ColorRed);
            this.Controls.Add(ColorGreen);
            this.Controls.Add(ColorYellow);
         
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
       

            Theme.Apply(this);
            ColorRed.ForeColor = Color.Red;
            ColorGreen.ForeColor = Color.Green;
            ColorYellow.ForeColor = Color.Yellow;
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
            currentDifficulty = "Easy";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Program to Print Full Pyramid Pattern (Star Pattern)");
            comboBox1.Items.Add("Program to find if a character is vowel or Consonant");
            comboBox1.Items.Add("Print Fibonacci Series");
            comboBox1.Visible = true;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            currentDifficulty = "Medium";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Floor in a Sorted Array");
            comboBox1.Items.Add("Move All Zeroes to End");
            comboBox1.Items.Add("T-primes");
            comboBox1.Visible = true;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            currentDifficulty = "Hard";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Floor in a Sorted Array");
            comboBox1.Items.Add("Move All Zeroes to End");
            comboBox1.Items.Add("T-primes");
            comboBox1.Visible = true;

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Color itemColor;
            switch (currentDifficulty)
            {
                case "Easy": itemColor = Color.Green; break;
                case "Medium": itemColor = Color.Yellow; break;
                case "Hard": itemColor = Color.Red; break;
                default: itemColor = Color.Black; break;
            }

            e.DrawBackground();
            using (Brush brush = new SolidBrush(itemColor))
            {
                e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string problem = comboBox1.SelectedItem.ToString();

            ProblemDetailsForm form = new ProblemDetailsForm(problem);
            form.Show();
        }
        }

}
