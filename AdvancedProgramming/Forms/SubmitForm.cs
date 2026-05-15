using System;
using System.Drawing;
using System.Threading.Tasks;
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
        private Button buttonRun;
        private Button buttonBack;
        private Label labelProblemName;
        private Label labelCodePrompt;
        private ComboBox languageCombo;
        private TextBox codeEditor;
        private LoadingOverlay loadingOverlay;
        private bool isRunning;

        public SubmitForm(string problemName)
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent();
            labelProblemName.Text = problemName;
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
                Font = DesignTokens.Typography.BodySmall,
                Size = new Size(DesignTokens.Sizing.ButtonWidthSm, 40),
                Location = new Point(DesignTokens.Spacing.Md, toolbar.Height + DesignTokens.Spacing.Sm),
                FlatStyle = FlatStyle.Flat,
                Tag = "Ghost",
            };
            buttonHome.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);

            labelProblemName = new Label
            {
                Font = DesignTokens.Typography.HeadingMedium,
                Size = new Size(500, 35),
                Location = new Point(cx - 250, 80),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            languageCombo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = DesignTokens.Typography.BodyMedium,
                Size = new Size(120, 28),
                Location = new Point(cx - 275, 135),
                FlatStyle = FlatStyle.Flat,
            };
            languageCombo.Items.Add("C#");
            languageCombo.SelectedIndex = 0;

            labelCodePrompt = new Label
            {
                Text = "Enter your solution:",
                Font = DesignTokens.Typography.BodySmall,
                Size = new Size(200, 20),
                Location = new Point(cx - 275, 168),
                Tag = "Secondary",
            };

            codeEditor = new TextBox
            {
                Font = DesignTokens.Typography.Code,
                Location = new Point(cx - 275, 195),
                Multiline = true,
                Size = new Size(550, 320),
                ScrollBars = ScrollBars.Both,
                AcceptsTab = true,
                AcceptsReturn = true,
                Text = "using System;\r\n\r\nclass Program\r\n{\r\n    static void Main(string[] args)\r\n    {\r\n        \r\n    }\r\n}",
            };

            buttonRun = new Button
            {
                Text = "Run",
                Font = DesignTokens.Typography.ButtonLabel,
                Size = new Size(DesignTokens.Sizing.ButtonWidthSm, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx + 55, 540),
                FlatStyle = FlatStyle.Flat,
                Tag = "Primary",
                Cursor = Cursors.Hand,
            };
            buttonRun.Click += ButtonRun_Click;

            buttonBack = new Button
            {
                Text = "Back",
                Font = DesignTokens.Typography.BodyMedium,
                Size = new Size(DesignTokens.Sizing.ButtonWidthSm, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx - 175, 540),
                FlatStyle = FlatStyle.Flat,
                Tag = "Secondary",
            };
            buttonBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            loadingOverlay = new LoadingOverlay();

            this.Controls.Add(buttonHome);
            this.Controls.Add(buttonBack);
            this.Controls.Add(labelProblemName);
            this.Controls.Add(labelCodePrompt);
            this.Controls.Add(languageCombo);
            this.Controls.Add(codeEditor);
            this.Controls.Add(buttonRun);
            this.Controls.Add(loadingOverlay);

            FormAccessibility.SetShortcutHint(buttonRun, "Ctrl+Enter", "Run test cases");
            FormAccessibility.SetShortcutHint(buttonBack, "Esc", "Go back");
            FormAccessibility.SetShortcutHint(codeEditor, "Tab", "Indent code");

            Theme.StylePage(this);

            ResumeLayout(false);
            PerformLayout();
        }

        public void RunTests()
        {
            if (!isRunning)
                ButtonRun_Click(buttonRun, EventArgs.Empty);
        }

        private async void ButtonRun_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            string code = codeEditor.Text;
            string problemName = labelProblemName.Text;

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

            SetRunningState(true);

            try
            {
                var results = await Task.Run(() =>
                {
                    var runner = new CodeRunner();
                    return runner.RunTestCases(code, problem.TestCase);
                }).ConfigureAwait(true);

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
            catch (Exception ex)
            {
                MessageBox.Show("Test execution failed: " + ex.Message, "Error");
            }
            finally
            {
                SetRunningState(false);
            }
        }

        private void SetRunningState(bool running)
        {
            isRunning = running;
            buttonRun.Enabled = !running;
            buttonBack.Enabled = !running;
            buttonHome.Enabled = !running;
            codeEditor.Enabled = !running;
            languageCombo.Enabled = !running;

            if (running)
                loadingOverlay.Show();
            else
                loadingOverlay.HideOverlay();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            loadingOverlay?.BringToFront();
        }
    }
}
