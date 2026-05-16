using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class FailedForm : UserControl
    {
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private Toolbar toolbar;
        private Button backButton;
        private Button homeButton;
        private List<CodeRunnerTestResult> testResults;
        private Label headerLabel;
        private Label summaryLabel;

        public FailedForm(string name, List<CodeRunnerTestResult> results)
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            testResults = results;
            InitializeComponent();
            LoadProblem(name);
            toolbar.CloseRequested += (s, e) => Application.Exit();
        }

        private void InitializeComponent()
        {
            this.AutoScroll = true;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            int cx = this.Width / 2;

            int passedCount = testResults.Count(r => r.Passed);

            string icon = passedCount == testResults.Count ? "\u2705" : "\u274c";
            Color headerColor = passedCount == testResults.Count ? Theme.Current.SuccessColor : Theme.Current.ErrorColor;
            string headerText = passedCount == testResults.Count ? "All Tests Passed" : "Some tests failed";

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 48F, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(80, 80),
                Location = new Point(cx - 40, 75),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            headerLabel = new Label
            {
                Text = headerText,
                Font = DesignTokens.Typography.HeadingLarge,
                Size = new Size(400, 40),
                Location = new Point(cx - 200, 160),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = headerColor,
                BackColor = Color.Transparent,
            };

            summaryLabel = new Label
            {
                Text = $"{passedCount} / {testResults.Count} test cases passed",
                Font = DesignTokens.Typography.BodyMedium,
                Size = new Size(400, 25),
                Location = new Point(cx - 200, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Secondary",
                BackColor = Color.Transparent,
            };

            (backButton, homeButton) = PageBackButton.Create(
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));

            this.Controls.Add(iconLabel);
            this.Controls.Add(headerLabel);
            this.Controls.Add(summaryLabel);
            this.Controls.Add(backButton);
            this.Controls.Add(homeButton);

            Theme.StylePage(this);
        }

        private void LoadProblem(string name)
        {
            var problem = ProblemLoadReadJs.GetByName(name);
            var testCases = problem?.TestCase ?? new List<TestCase>();

            if (testCases.Count == 0)
                return;

            int cx = this.Width / 2;
            int startY = 245;

            for (int i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                int targetTop = startY + i * 110;

                Panel panel = new Panel
                {
                    Width = 620,
                    Height = 100,
                    Left = cx - 310,
                    Top = targetTop,
                    BackColor = Theme.Current.SurfaceColor,
                };

                bool passed = testResults != null && i < testResults.Count && testResults[i].Passed;
                Color statusColor = passed ? Theme.Current.SuccessColor : Theme.Current.ErrorColor;
                string statusText = passed ? "PASSED" : "FAILED";

                Label statusLabel = new Label
                {
                    Text = statusText,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = statusColor,
                    BackColor = Color.Transparent,
                    AutoSize = false,
                    Size = new Size(80, 24),
                    Location = new Point(12, 8),
                    TextAlign = ContentAlignment.MiddleLeft,
                };

                string detailText = testCase.ToString();
                if (testResults != null && i < testResults.Count)
                {
                    var result = testResults[i];
                    detailText += "\nExpected: " + result.ExpectedOutput + "\nActual: " + result.ActualOutput;
                }

                Label detailLabel = new Label
                {
                    Text = detailText,
                    Font = DesignTokens.Typography.BodySmall,
                    ForeColor = Theme.Current.TextColor,
                    BackColor = Color.Transparent,
                    AutoSize = false,
                    Size = new Size(580, 60),
                    Location = new Point(12, 32),
                };

                panel.Controls.Add(statusLabel);
                panel.Controls.Add(detailLabel);
                this.Controls.Add(panel);
            }
        }
    }
}
