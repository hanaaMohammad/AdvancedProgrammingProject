using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class LevelProblemForm : UserControl
    {
        public event EventHandler<string> ProblemSelected;
        public event EventHandler BackRequested;

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

        public LevelProblemForm()
        {
            this.Size = new Size(1100, 800);
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

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            int cx = this.Width / 2;
            int btnCx = cx - 103;

            this.label1.Font = new Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new Point(0, 39);
            this.label1.Size = new Size(1100, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "- problem solving -";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.Click += new EventHandler(this.label1_Click);

            this.label2.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.label2.Location = new Point(cx - 283, 150);
            this.label2.Size = new Size(567, 49);
            this.label2.TabIndex = 2;
            this.label2.Text = "Choose a level from the problem-solving levels";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            this.label2.Click += new EventHandler(this.label1_Click);

            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button1.Location = new Point(btnCx, 220);
            this.button1.Size = new Size(206, 69);
            this.button1.TabIndex = 3;
            this.button1.Text = "Easy";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new EventHandler(this.button1_Click);

            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button2.Location = new Point(btnCx, 310);
            this.button2.Size = new Size(206, 67);
            this.button2.TabIndex = 4;
            this.button2.Text = "Medium";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);

            this.button3.FlatStyle = FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.button3.Location = new Point(btnCx, 400);
            this.button3.Size = new Size(206, 66);
            this.button3.TabIndex = 5;
            this.button3.Text = "Hard";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new EventHandler(this.button3_Click);

            comboBox1 = new ComboBox
            {
                Location = new Point(cx + 120, 220),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false,
                DrawMode = DrawMode.OwnerDrawFixed
            };
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.DrawItem += new DrawItemEventHandler(this.comboBox1_DrawItem);

            ColorDiscrip = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(cx - 300, 250),
                Name = "ColorDiscrip",
                Size = new Size(150, 25),
                TabIndex = 8,
                Text = "Color Description:"
            };

            ColorRed = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(cx - 300, 330),
                Name = "ColorRed",
                Size = new Size(150, 25),
                TabIndex = 9,
                Text = "Red: Hard level"
            };

            ColorGreen = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(cx - 300, 400),
                Name = "ColorGreen",
                Size = new Size(150, 25),
                TabIndex = 10,
                Text = "Green: Easy level"
            };

            ColorYellow = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(cx - 300, 500),
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

            Theme.Apply(this);
            ColorRed.ForeColor = Color.Red;
            ColorGreen.ForeColor = Color.Green;
            ColorYellow.ForeColor = Color.Yellow;
            this.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e) { }

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
            comboBox1.Items.Add("Happy Number");
            comboBox1.Items.Add("Lucky Numbers in a Matrix");
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
            ProblemSelected?.Invoke(this, problem);
        }
    }
}
