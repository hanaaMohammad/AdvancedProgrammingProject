using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Service;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming.Forms
{
    internal class SubmiittForm : Form
    {
        private Toolbar toolbar;
        private Button buttonHome;
        private Button buttonRunn;
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

                Size = new Size(250, 20),
                Location = new Point(125, 190),
                Text = "\\\\Enter the code here",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };

            comboBoxTybeLang = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(140, 28),
                Location = new Point(120, 150),
                FlatStyle = FlatStyle.Flat,
            };
        
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

            buttonRunn = new Button
            {
                Text = "Run",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 45),
                Location = new Point(430, 480),
                FlatStyle = FlatStyle.Flat,
            };
            buttonRunn.Click += buttonRun_Click;

            this.Controls.Add(buttonHome);
            this.Controls.Add(buttonBack);
            this.Controls.Add(labelNameproblem);
            this.Controls.Add(labeIndtwrite);
            this.Controls.Add(comboBoxTybeLang);
            this.Controls.Add(textBoxCode);
            this.Controls.Add(buttonRunn);

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
            string code = textBoxCode.Text;
            string language = comboBoxTybeLang.SelectedItem.ToString();
            string problemName = labelNameproblem.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter your code.", "Warning");
                return;
            }

            var problemLoader = new ProblemLoadReadJs();
            var problem = problemLoader.getProblemByName(problemName);

            if (problem == null || problem.TestCase == null || problem.TestCase.Count == 0)
            {
                MessageBox.Show("No test cases found for this problem.", "Error");
                return;
            }

            var runner = new CodeRunner();
            var results = runner.RunTestCases(code, language, problem.TestCase);

            bool allPassed = true;
            foreach (var r in results)
            {
                if (!r.Passed)
                {
                    allPassed = false;
                    break;
                }
            }

            if (allPassed)
            {
                AxpectedForm form = new AxpectedForm();
                form.Show();
                this.Hide();
            }
            else
            {
                Faild form = new Faild(problemName, results);
                form.Show();
                this.Hide();
            }
        }
    }
}
