using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class FailedForm : UserControl
    {
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private Timer animTimer;
        private List<Panel> resultPanels = new List<Panel>();
        private List<int> targetTops = new List<int>();
        private int currentAnimIndex = 0;
        private Toolbar toolbar;
        private Button backButton;
        private Button homeButton;
        private List<CodeRunnerTestResult> testResults;
        private Label headerLabel;

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

            var iconLabel = new Label
            {
                Text = "\u274c",
                Font = new Font("Segoe UI", 48F, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(80, 80),
                Location = new Point(cx - 40, 75),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            headerLabel = new Label
            {
                Text = "Some tests failed",
                Font = DesignTokens.Typography.HeadingLarge,
                Size = new Size(400, 40),
                Location = new Point(cx - 200, 160),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Theme.Current.ErrorColor,
                BackColor = Color.Transparent,
            };

            var descLabel = new Label
            {
                Text = "Review the details below and try again",
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
            this.Controls.Add(descLabel);
            this.Controls.Add(backButton);
            this.Controls.Add(homeButton);

            Theme.StylePage(this);
        }

        private void LoadProblem(string name)
        {
            var loader = new ProblemLoadReadJs();
            var problem = loader.getProblemByName(name);
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
                    Top = -150,
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
                if (!passed && testResults != null && i < testResults.Count)
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
                resultPanels.Add(panel);
                targetTops.Add(targetTop);
            }

            animTimer = new Timer();
            animTimer.Interval = 16;
            animTimer.Tick += AnimatePanels;
            animTimer.Start();
        }

        private void AnimatePanels(object sender, EventArgs e)
        {
            if (currentAnimIndex >= resultPanels.Count)
            {
                animTimer.Stop();
                return;
            }

            Panel panel = resultPanels[currentAnimIndex];
            int target = targetTops[currentAnimIndex];

            if (panel.Top < target)
                panel.Top += 10;
            else
                currentAnimIndex++;
        }
    }
}
