using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    internal class SubmiittForm : Form
    {
        private Toolbar toolbar;
        private Button buttonHome;
        private Button buttonRun;
        private Button buttonBack;
        private Label labelNameproblem;
        private Label labeIndtwrite;
        private ComboBox comboBoxTybeLang;
        private TextBox textBoxCode;

        public SubmiittForm(string problemName)
        {
            InitializeComponent();
            labelNameproblem.Text = problemName;
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            this.Text = "Submit Code";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(808, 549);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            buttonHome = new Button
            {
                Text = "\u2302 Home",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(100, 45),
                Location = new Point(15, 70),
                FlatStyle = FlatStyle.Flat,
            };
            buttonHome.Click += buttonHome_Click;

            buttonBack = new Button
            {
                Text = "Back",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(100, 45),
                Location = new Point(320, 480),
                FlatStyle = FlatStyle.Flat,
            };
            buttonBack.Click += buttonBack_Click;

            labelNameproblem = new Label
            {
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Size = new Size(550, 40),
                Location = new Point(120, 70),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            labeIndtwrite = new Label
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic),
                Size = new Size(300, 30),
                Location = new Point(120, 185),
                Text = "Enter the code here \u2B07\u2B07",
                TextAlign = ContentAlignment.MiddleCenter,
            };

            comboBoxTybeLang = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(140, 28),
                Location = new Point(120, 150),
                FlatStyle = FlatStyle.Flat,
            };
            comboBoxTybeLang.Items.Add("C#");
            comboBoxTybeLang.Items.Add("C++");
            comboBoxTybeLang.Items.Add("Java");
            comboBoxTybeLang.SelectedIndex = 0;

            textBoxCode = new TextBox
            {
                Font = new Font("Consolas", 10F, FontStyle.Regular),
                Location = new Point(120, 185),
                Multiline = true,
                Size = new Size(550, 280),
                ScrollBars = ScrollBars.Both,
            };

            buttonRun = new Button
            {
                Text = "Run",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 45),
                Location = new Point(430, 480),
                FlatStyle = FlatStyle.Flat,
            };
            buttonRun.Click += buttonRun_Click;

            this.Controls.Add(buttonHome);
            this.Controls.Add(buttonBack);
            this.Controls.Add(labelNameproblem);
            this.Controls.Add(labeIndtwrite);
            this.Controls.Add(comboBoxTybeLang);
            this.Controls.Add(textBoxCode);
            this.Controls.Add(buttonRun);

            Theme.Apply(this);

            ResumeLayout(false);
            PerformLayout();
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