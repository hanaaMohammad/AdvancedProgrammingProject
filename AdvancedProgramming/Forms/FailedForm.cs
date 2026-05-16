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
    public class FailedForm : AppForm
    {
        private const int SideMargin = 40;

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel resultsCard;
        private Panel resultsScroll;
        private Label headerLabel;
        private Label summaryLabel;

        private List<CodeRunnerTestResult> testResults;
        private Color resultAccent;
        private bool allPassed;

        public FailedForm(string name, List<CodeRunnerTestResult> results)
        {
            testResults = results ?? new List<CodeRunnerTestResult>();
            InitializeComponent();
            BuildTestCards(name);
        }

        private void InitializeComponent()
        {
            int passedCount = testResults.Count(r => r.Passed);
            allPassed = testResults.Count > 0 && passedCount == testResults.Count;
            resultAccent = allPassed ? AppColors.Success : AppColors.Error;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button backButton = MakeNavButton("\u2190 Back", 16, BackButton_Click);
            Button homeButton = MakeNavButton("Home", 104, HomeButton_Click);
            Controls.Add(backButton);
            Controls.Add(homeButton);
            backButton.BringToFront();
            homeButton.BringToFront();

            headerCard = UiHelper.CreateCard(Color.FromArgb(50, resultAccent), 20);
            BuildHeader(passedCount);
            Controls.Add(headerCard);

            resultsCard = UiHelper.CreateCard(AppColors.DefaultBorder, 20);
            resultsScroll = new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
            };
            resultsCard.Controls.Add(resultsScroll);
            Controls.Add(resultsCard);

            int contentW = AppSizes.FormWidth - SideMargin * 2;
            headerCard.SetBounds(SideMargin, AppSizes.ContentTop, contentW, 108);
            resultsCard.SetBounds(SideMargin, 224, contentW, 556);
            resultsScroll.SetBounds(0, 0, contentW, 556);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowAsMainForm(new LevelProblemForm());
        }

        private void BuildHeader(int passedCount)
        {
            string icon = allPassed ? "\u2705" : "\u274c";
            string headerText = allPassed ? "All Tests Passed" : "Some Tests Failed";

            headerCard.Controls.Add(new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 40),
                Location = new Point(24, 16),
                Size = new Size(56, 56),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            });

            headerLabel = new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = resultAccent,
                Location = new Point(92, 18),
                Size = new Size(500, 32),
                BackColor = Color.Transparent,
            };

            summaryLabel = new Label
            {
                Text = passedCount + " / " + testResults.Count + " test cases passed",
                Font = new Font("Segoe UI", 11),
                ForeColor = AppColors.MutedText,
                Location = new Point(92, 52),
                Size = new Size(400, 24),
                BackColor = Color.Transparent,
            };

            headerCard.Controls.Add(headerLabel);
            headerCard.Controls.Add(summaryLabel);
            headerCard.Controls.Add(new Label
            {
                Text = allPassed ? "Score updated — great work!" : "Review the details below and try again.",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.MutedText,
                Location = new Point(24, 78),
                Size = new Size(700, 20),
                BackColor = Color.Transparent,
            });
        }

        private void BuildTestCards(string problemName)
        {
            Problem problem = ProblemLoadReadJs.GetByName(problemName);
            var testCases = problem?.TestCase ?? new List<TestCase>();
            if (testCases.Count == 0)
                return;

            int y = 12;
            int cardW = AppSizes.FormWidth - SideMargin * 2 - 32;

            for (int i = 0; i < testCases.Count; i++)
            {
                bool passed = i < testResults.Count && testResults[i].Passed;
                Panel card = CreateTestCaseCard(
                    i + 1, testCases[i],
                    i < testResults.Count ? testResults[i] : null,
                    passed, cardW);
                card.Location = new Point(16, y);
                resultsScroll.Controls.Add(card);
                y += card.Height + 14;
            }
        }

        private Panel CreateTestCaseCard(int index, TestCase testCase, CodeRunnerTestResult result, bool passed, int width)
        {
            Color accent = passed ? AppColors.Success : AppColors.Error;
            Panel card = UiHelper.CreateCard(Color.FromArgb(50, accent), 14);
            card.Width = width;

            card.Controls.Add(new Label
            {
                Text = "Test case " + index,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 14),
                AutoSize = true,
                BackColor = Color.Transparent,
            });

            Panel statusPill = UiHelper.CreateStatusPill(passed ? "PASSED" : "FAILED", accent);
            statusPill.Location = new Point(width - statusPill.Width - 16, 12);
            card.Controls.Add(statusPill);

            int y = 48;
            y = AddResultRow(card, y, width, "Input", testCase.input ?? "");
            y = AddResultRow(card, y, width, "Expected", result?.ExpectedOutput ?? testCase.output?.ToString() ?? "");
            y = AddResultRow(card, y, width, "Actual", result?.ActualOutput ?? "—");

            card.Height = y + 14;
            return card;
        }

        private int AddResultRow(Panel card, int y, int cardWidth, string label, string value)
        {
            int blockW = cardWidth - 32;
            Font bodyFont = label == "Input"
                ? new Font("Consolas", 10)
                : new Font("Segoe UI", 10);
            int textH = Math.Max(36, MeasureHeight(value, bodyFont, blockW - 24));
            int blockH = 22 + 8 + textH;

            var block = new Panel
            {
                Location = new Point(16, y),
                Size = new Size(blockW, blockH),
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                UiHelper.PaintInset(e.Graphics, inset, 10);
            };

            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(blockW, 18),
                BackColor = Color.Transparent,
            });

            block.Controls.Add(new TextBox
            {
                Text = value ?? "",
                ReadOnly = true,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.InsetBack,
                ForeColor = Color.White,
                Font = bodyFont,
                Location = new Point(10, 28),
                Size = new Size(blockW - 20, textH),
                TabStop = false,
            });

            card.Controls.Add(block);
            return y + blockH + 8;
        }

        private static int MeasureHeight(string text, Font font, int width)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 36;
            var size = TextRenderer.MeasureText(text, font, new Size(width, 0), TextFormatFlags.WordBreak);
            return size.Height + 10;
        }
    }
}
