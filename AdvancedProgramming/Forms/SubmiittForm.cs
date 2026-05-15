using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Service;
using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Session;

namespace AdvancedProgramming.Forms
{
    public class SubmitForm : UserControl
    {
        public event EventHandler<CodeRunnerTestResultList> TestResultsReady;
        public event EventHandler HomeRequested;
        public event EventHandler BackRequested;

        private Toolbar toolbar;
        private Button buttonHome;
        private Button buttonRunn;
        private Button buttonBack;
        private Label labelNameproblem;
        private Label labelCodePrompt;
        private ComboBox comboBoxTybeLang;
        private TextBox textBoxCode;

        public SubmitForm(string problemName)
        {
            this.Size = new Size(1100, 800);
            InitializeComponent();
            labelNameproblem.Text = problemName;
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            int cx = this.Width / 2;
            SuspendLayout();

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
                Location = new Point(cx - 110, 560),
                FlatStyle = FlatStyle.Flat,
            };
            buttonBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            labelNameproblem = new Label
            {
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Size = new Size(550, 40),
                Location = new Point(cx - 275, 70),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            labelCodePrompt = new Label
            {
                Size = new Size(250, 20),
                Location = new Point(cx - 125, 195),
                Text = "Enter the code here",
                TextAlign = ContentAlignment.MiddleCenter,
            };

            comboBoxTybeLang = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(140, 28),
                Location = new Point(cx - 70, 150),
                FlatStyle = FlatStyle.Flat,
            };

            comboBoxTybeLang.Items.Add("C#");
            comboBoxTybeLang.Items.Add("Java");
            comboBoxTybeLang.SelectedIndex = 0;

            textBoxCode = new TextBox
            {
                Font = new Font("Consolas", 10F, FontStyle.Regular),
                Location = new Point(cx - 275, 230),
                Multiline = true,
                Size = new Size(550, 300),
                ScrollBars = ScrollBars.Both,
            };

            buttonRunn = new Button
            {
                Text = "Run",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 45),
                Location = new Point(cx + 10, 560),
                FlatStyle = FlatStyle.Flat,
            };
            buttonRunn.Click += buttonRun_Click;

            this.Controls.Add(buttonHome);
            this.Controls.Add(buttonBack);
            this.Controls.Add(labelNameproblem);
            this.Controls.Add(labelCodePrompt);
            this.Controls.Add(comboBoxTybeLang);
            this.Controls.Add(textBoxCode);
            this.Controls.Add(buttonRunn);

            Theme.Apply(this);

            ResumeLayout(false);
            PerformLayout();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            HomeRequested?.Invoke(this, EventArgs.Empty);
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
            int passedCount = 0;
            foreach (var r in results)
            {
                if (r.Passed)
                    passedCount++;
                else
                    allPassed = false;
            }

            if (allPassed && passedCount > 0)
            {
                var userMgmt = new UserManagement();
                userMgmt.UpdateScore(CurrentUser.Username, passedCount * 10);
                CurrentUser.Score = userMgmt.GetScore(CurrentUser.Username);
            }

            TestResultsReady?.Invoke(this, new CodeRunnerTestResultList
            {
                Results = results,
                AllPassed = allPassed
            });
        }
    }
}
