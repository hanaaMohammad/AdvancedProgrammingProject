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
        private const int HeaderTop = CatalogUi.ContentTop;

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel resultsCard;
        private Panel resultsScroll;
        private Label headerLabel;
        private Label summaryLabel;

        private readonly List<CodeRunnerTestResult> testResults;
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
            resultAccent = allPassed ? Theme.Current.SuccessColor : Theme.Current.ErrorColor;

            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            var (backButton, homeButton) = PageBackButton.Create(
                (s, e) => GoBack(),
                (s, e) => GoAppHome());
            Controls.Add(backButton);
            Controls.Add(homeButton);
            backButton.BringToFront();
            homeButton.BringToFront();

            Color border = Color.FromArgb(50, resultAccent);
            headerCard = CatalogUi.CreateCard(border, 20);
            BuildHeader(passedCount);
            Controls.Add(headerCard);

            resultsCard = CatalogUi.CreateCard(CatalogUi.DefaultBorder, 20);
            resultsScroll = new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            resultsCard.Controls.Add(resultsScroll);
            Controls.Add(resultsCard);

            FormAccessibility.SetShortcutHint(backButton, "Esc", "Go back");

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildHeader(int passedCount)
        {
            string icon = allPassed ? "\u2705" : "\u274c";
            string headerText = allPassed ? "All Tests Passed" : "Some Tests Failed";

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 40, FontStyle.Regular),
                Location = new Point(24, 16),
                Size = new Size(56, 56),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            headerLabel = new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = resultAccent,
                Location = new Point(92, 18),
                Size = new Size(500, 32),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            summaryLabel = new Label
            {
                Text = $"{passedCount} / {testResults.Count} test cases passed",
                Font = new Font("Segoe UI", 11),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(92, 52),
                Size = new Size(400, 24),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var scoreNote = new Label
            {
                Text = allPassed ? "Score updated — great work!" : "Review the details below and try again.",
                Font = new Font("Segoe UI", 9),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(24, 78),
                Size = new Size(700, 20),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            headerCard.Controls.Add(iconLabel);
            headerCard.Controls.Add(headerLabel);
            headerCard.Controls.Add(summaryLabel);
            headerCard.Controls.Add(scoreNote);
        }

        private void BuildTestCards(string problemName)
        {
            var problem = ProblemLoadReadJs.GetByName(problemName);
            var testCases = problem?.TestCase ?? new List<TestCase>();
            if (testCases.Count == 0)
                return;

            int y = 12;
            int cardW = 800;

            for (int i = 0; i < testCases.Count; i++)
            {
                bool passed = i < testResults.Count && testResults[i].Passed;
                var card = CreateTestCaseCard(i + 1, testCases[i], i < testResults.Count ? testResults[i] : null, passed, cardW);
                card.Location = new Point(16, y);
                resultsScroll.Controls.Add(card);
                y += card.Height + 14;
            }
        }

        private Panel CreateTestCaseCard(int index, TestCase testCase, CodeRunnerTestResult result, bool passed, int width)
        {
            Color accent = passed ? Theme.Current.SuccessColor : Theme.Current.ErrorColor;
            var card = CatalogUi.CreateCard(Color.FromArgb(50, accent), 14);
            card.Width = width;
            CatalogUi.EnableDoubleBuffer(card);

            var title = new Label
            {
                Text = "Test case " + index,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 14),
                AutoSize = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var statusPill = CatalogUi.CreateStatusPill(passed ? "PASSED" : "FAILED", accent);
            statusPill.Location = new Point(width - statusPill.Width - 16, 12);

            int y = 48;
            y = AddResultRow(card, ref y, width, "Input", testCase.input ?? "");
            y = AddResultRow(card, ref y, width, "Expected", result?.ExpectedOutput ?? testCase.output?.ToString() ?? "");
            y = AddResultRow(card, ref y, width, "Actual", result?.ActualOutput ?? "—");

            card.Height = y + 14;
            card.Controls.Add(title);
            card.Controls.Add(statusPill);
            return card;
        }

        private static int AddResultRow(Panel card, ref int y, int cardWidth, string label, string value)
        {
            int blockW = cardWidth - 32;
            int textH = Math.Max(36, MeasureHeight(value, blockW - 24));
            int blockH = 22 + 8 + textH;

            var block = new Panel
            {
                Location = new Point(16, y),
                Size = new Size(blockW, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(block);

            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                CatalogUi.PaintInset(e.Graphics, inset, 10);
            };

            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(0, 0),
                Size = new Size(blockW, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            var body = new TextBox
            {
                Text = value ?? "",
                ReadOnly = true,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                BackColor = CatalogUi.InsetBack,
                ForeColor = Color.White,
                Font = label == "Input" ? DesignTokens.Typography.Code : DesignTokens.Typography.BodySmall,
                Location = new Point(10, 28),
                Size = new Size(blockW - 20, textH),
                TabStop = false,
                Tag = "NoTheme",
            };
            block.Controls.Add(body);
            card.Controls.Add(block);

            y += blockH + 8;
            return y;
        }

        private static int MeasureHeight(string text, int width)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 36;
            var size = TextRenderer.MeasureText(text, DesignTokens.Typography.BodySmall,
                new Size(width, 0), TextFormatFlags.WordBreak);
            return size.Height + 10;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (headerCard == null)
                return;

            int contentW = Math.Max(600, Width - SideMargin * 2);
            int cx = Width / 2;
            int left = cx - contentW / 2;

            headerCard.SetBounds(left, HeaderTop, contentW, 108);

            int resultsTop = headerCard.Bottom + 16;
            int resultsH = Math.Max(300, Height - resultsTop - 24);
            resultsCard.SetBounds(left, resultsTop, contentW, resultsH);
            resultsScroll.SetBounds(0, 0, contentW, resultsH);

            int cardW = contentW - 32;
            foreach (Control c in resultsScroll.Controls)
            {
                if (c is Panel card)
                    card.Width = cardW;
            }
        }
    }
}
