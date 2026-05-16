using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class ProblemDisplayForm : UserControl
    {
        public event EventHandler SolveRequested;
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private const int ContentWidth = 760;
        private const int ContentTop = 188;
        private const int ContentHeight = 400;

        private Toolbar toolbar;
        private Button btnBack;
        private Button btnHome;
        private Label titleLabel;
        private Label levelLabel;
        private Label subtitleLabel;
        private Label comingSoonLabel;
        private TabControl tabControl;
        private TabPage tabStatement;
        private TabPage tabExample;
        private TextBox descriptionBox;
        private TextBox inputBox;
        private TextBox outputBox;
        private TextBox constraintsBox;
        private TextBox inputExampleBox;
        private TextBox outputExampleBox;
        private TextBox explanationBox;
        private RichTextBox solutionBox;
        private Panel actionPanel;
        private Label labelSolution;
        private Panel solutionPanel;
        private Panel exampleOutputHost;
        private Components.PanelStars panelStars;
        private Button solveButton;
        private Button showSolutionButton;
        private string problemName;
        private Problem problemChoice;
        private bool solutionVisible;
        private bool isAvailable;

        public ProblemDisplayForm(string problem)
        {
            Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent(problem);
        }

        private void InitializeComponent(string problem)
        {
            problemName = problem;
            GetProblemDetails();

            if (problemChoice == null)
            {
                MessageBox.Show("Problem not found", "MiniCamp Puzzle",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAvailable = ProblemCatalog.IsAvailable(problemName);
            string displayTitle = string.IsNullOrWhiteSpace(problemChoice.title)
                ? problemName
                : problemChoice.title;
            SuspendLayout();

            int cx = Width / 2;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            Controls.Add(toolbar);

            (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));

            titleLabel = new Label
            {
                Text = displayTitle,
                Font = DesignTokens.Typography.DisplaySmall,
                AutoSize = false,
                Size = new Size(ContentWidth, 52),
                Location = new Point(cx - ContentWidth / 2, 84),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            levelLabel = new Label
            {
                Text = FormatLevel(problemChoice.level),
                Font = DesignTokens.Typography.HeadingSmall,
                AutoSize = false,
                Size = new Size(ContentWidth, 26),
                Location = new Point(cx - ContentWidth / 2, 136),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = GetLevelColor(problemChoice.level),
                BackColor = Color.Transparent,
            };

            subtitleLabel = new Label
            {
                Text = "Read the statement, then choose Solve when you are ready",
                Font = DesignTokens.Typography.BodyLarge,
                AutoSize = false,
                Size = new Size(ContentWidth, 24),
                Location = new Point(cx - ContentWidth / 2, 162),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Secondary",
            };

            comingSoonLabel = new Label
            {
                Text = "This problem is coming soon. Only the starter problem can be opened in the editor.",
                Font = DesignTokens.Typography.BodySmall,
                AutoSize = false,
                Size = new Size(520, 36),
                Location = new Point(cx - 260, ContentTop - 28),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Warning",
                Visible = !isAvailable,
            };

            tabControl = new TabControl
            {
                Font = DesignTokens.Typography.BodyMedium,
                Size = new Size(ContentWidth, ContentHeight),
                Location = new Point(cx - ContentWidth / 2, ContentTop),
                Padding = new Point(DesignTokens.Spacing.Md, DesignTokens.Spacing.Sm),
            };

            tabStatement = new TabPage("Statement")
            {
                Padding = new Padding(DesignTokens.Spacing.Md),
                AutoScroll = true,
            };
            tabExample = new TabPage("Example")
            {
                Padding = new Padding(DesignTokens.Spacing.Md),
                AutoScroll = true,
            };

            BuildStatementTab();
            BuildExampleTab();

            tabControl.TabPages.Add(tabStatement);
            tabControl.TabPages.Add(tabExample);

            actionPanel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(ContentWidth, DesignTokens.Sizing.ButtonHeight),
            };

            solveButton = CreateActionButton(
                isAvailable ? "Solve" : "Coming Soon",
                isAvailable ? "Primary" : "Secondary",
                isAvailable);
            solveButton.Click += (s, e) =>
            {
                if (isAvailable)
                    SolveRequested?.Invoke(this, EventArgs.Empty);
            };

            bool hasSolution = !string.IsNullOrWhiteSpace(problemChoice.solution);
            showSolutionButton = CreateActionButton("Show Solution", "Secondary", hasSolution);
            showSolutionButton.Visible = isAvailable;
            showSolutionButton.Click += (s, e) => ToggleSolution();

            actionPanel.Controls.Add(solveButton);
            actionPanel.Controls.Add(showSolutionButton);

            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();
            Controls.Add(titleLabel);
            Controls.Add(levelLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(comingSoonLabel);
            Controls.Add(tabControl);
            Controls.Add(actionPanel);

            FormAccessibility.SetShortcutHint(solveButton, "Enter", "Open code editor");
            FormAccessibility.SetShortcutHint(btnBack, "Esc", "Go back");
            FormAccessibility.SetShortcutHint(tabControl, "Ctrl+Tab", "Switch tabs");

            Theme.StylePage(this);
            Theme.Apply(this);
            StyleTabControl();
            ApplySolutionHighlight();

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildStatementTab()
        {
            int y = DesignTokens.Spacing.Sm;
            int fieldW = ContentWidth - DesignTokens.Spacing.Md * 4;

            y = AddField(tabStatement, "Description", problemChoice.description, y, fieldW, 130,
                DesignTokens.Typography.BodyMedium, out descriptionBox);
            y = AddField(tabStatement, "Input format", problemChoice.input, y, fieldW, 72,
                DesignTokens.Typography.BodySmall, out inputBox);
            y = AddField(tabStatement, "Output format", problemChoice.output, y, fieldW, 72,
                DesignTokens.Typography.BodySmall, out outputBox);
            AddField(tabStatement, "Constraints", problemChoice.Constraints, y, fieldW, 72,
                DesignTokens.Typography.BodySmall, out constraintsBox);
        }

        private void BuildExampleTab()
        {
            int y = DesignTokens.Spacing.Sm;
            int fieldW = ContentWidth - DesignTokens.Spacing.Md * 4;
            bool isPattern = string.Equals(problemChoice.type, "pattren", StringComparison.OrdinalIgnoreCase);

            y = AddField(tabExample, "Sample input", problemChoice.Example?.input ?? "", y, fieldW, 88,
                DesignTokens.Typography.Code, out inputExampleBox);

            exampleOutputHost = new Panel
            {
                Location = new Point(DesignTokens.Spacing.Md, y),
                Size = new Size(fieldW, isPattern ? 110 : 88),
            };

            var lblOutput = new Label
            {
                Text = "Sample output",
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
                Location = new Point(0, 0),
                Size = new Size(fieldW, DesignTokens.Sizing.LabelHeight),
            };
            exampleOutputHost.Controls.Add(lblOutput);

            if (isPattern)
            {
                panelStars = new Components.PanelStars
                {
                    Location = new Point(0, DesignTokens.Sizing.LabelHeight + 4),
                    Size = new Size(fieldW, 88),
                };
                exampleOutputHost.Controls.Add(panelStars);
            }
            else
            {
                outputExampleBox = CreateReadOnlyBox(
                    problemChoice.Example?.output ?? "",
                    DesignTokens.Typography.Code,
                    fieldW,
                    88);
                outputExampleBox.Location = new Point(0, DesignTokens.Sizing.LabelHeight + 4);
                exampleOutputHost.Controls.Add(outputExampleBox);
            }

            tabExample.Controls.Add(exampleOutputHost);
            y += exampleOutputHost.Height + DesignTokens.Spacing.Md;

            y = AddField(tabExample, "Explanation", problemChoice.Example?.explanation ?? "", y, fieldW, 88,
                DesignTokens.Typography.BodySmall, out explanationBox);

            solutionPanel = new Panel
            {
                Location = new Point(DesignTokens.Spacing.Md, y),
                Size = new Size(fieldW, 180),
                Visible = false,
            };

            labelSolution = new Label
            {
                Text = "Reference solution",
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
                Location = new Point(0, 0),
                Size = new Size(fieldW, DesignTokens.Sizing.LabelHeight),
            };

            int solutionHeight = GetCodeViewHeight(problemChoice.solution, 140, 260);
            solutionBox = new RichTextBox
            {
                Location = new Point(0, DesignTokens.Sizing.LabelHeight + 4),
                Size = new Size(fieldW, solutionHeight),
                Tag = "NoTheme",
                TabStop = false,
            };

            solutionPanel.Size = new Size(fieldW, DesignTokens.Sizing.LabelHeight + 4 + solutionHeight);
            solutionPanel.Controls.Add(labelSolution);
            solutionPanel.Controls.Add(solutionBox);
            tabExample.Controls.Add(solutionPanel);
        }

        private void ApplySolutionHighlight()
        {
            if (solutionBox == null)
                return;
            CSharpCodeHighlighter.Apply(solutionBox, problemChoice?.solution ?? string.Empty);
        }

        private static int AddField(
            Control parent,
            string caption,
            string text,
            int y,
            int width,
            int height,
            Font font,
            out TextBox box)
        {
            var label = new Label
            {
                Text = caption,
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
                Location = new Point(DesignTokens.Spacing.Md, y),
                Size = new Size(width, DesignTokens.Sizing.LabelHeight),
            };

            box = CreateReadOnlyBox(text, font, width, height);
            box.Location = new Point(DesignTokens.Spacing.Md, y + DesignTokens.Sizing.LabelHeight + 4);

            parent.Controls.Add(label);
            parent.Controls.Add(box);

            return y + DesignTokens.Sizing.LabelHeight + height + DesignTokens.Spacing.Md;
        }

        private static TextBox CreateReadOnlyBox(string text, Font font, int width, int height)
        {
            return new TextBox
            {
                Text = text ?? string.Empty,
                ReadOnly = true,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = font,
                Size = new Size(width, height),
                BorderStyle = BorderStyle.FixedSingle,
                TabStop = false,
            };
        }

        private static Button CreateActionButton(string text, string tag, bool enabled)
        {
            return new Button
            {
                Text = text,
                Font = tag == "Primary"
                    ? DesignTokens.Typography.ButtonLabel
                    : DesignTokens.Typography.BodyMedium,
                Size = new Size(DesignTokens.Sizing.ButtonWidthMd, DesignTokens.Sizing.ButtonHeight),
                FlatStyle = FlatStyle.Flat,
                Tag = tag,
                Cursor = enabled ? Cursors.Hand : Cursors.Default,
                Enabled = enabled,
            };
        }

        private static int GetCodeViewHeight(string code, int minHeight, int maxHeight)
        {
            string normalized = CSharpCodeHighlighter.NormalizeCode(code);
            if (string.IsNullOrEmpty(normalized))
                return minHeight;

            int lines = normalized.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
            return Math.Min(maxHeight, Math.Max(minHeight, lines * 18 + 16));
        }

        private void ToggleSolution()
        {
            if (string.IsNullOrWhiteSpace(problemChoice?.solution))
            {
                MessageBox.Show(
                    "No solution is available for this problem yet.",
                    "Solution",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            solutionVisible = !solutionVisible;
            showSolutionButton.Text = solutionVisible ? "Hide Solution" : "Show Solution";
            solutionPanel.Visible = solutionVisible;

            if (solutionVisible)
            {
                tabControl.SelectedTab = tabExample;
                ApplySolutionHighlight();
            }
        }

        private void GetProblemDetails()
        {
            var problemLoader = new ProblemLoadReadJs();
            problemChoice = problemLoader.getProblemByName(problemName);
        }

        private void StyleTabControl()
        {
            if (tabControl == null)
                return;

            tabControl.BackColor = Theme.Current.SurfaceColor;
            tabControl.ForeColor = Theme.Current.TextColor;

            foreach (TabPage page in tabControl.TabPages)
            {
                page.BackColor = Theme.Current.SurfaceColor;
                page.ForeColor = Theme.Current.TextColor;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (titleLabel == null)
                return;

            int cx = Width / 2;

            titleLabel.Location = new Point(cx - ContentWidth / 2, 84);
            levelLabel.Location = new Point(cx - ContentWidth / 2, 136);
            subtitleLabel.Location = new Point(cx - ContentWidth / 2, 162);
            comingSoonLabel.Location = new Point(cx - comingSoonLabel.Width / 2, ContentTop - 28);
            tabControl.Location = new Point(cx - ContentWidth / 2, ContentTop);
            tabControl.Size = new Size(ContentWidth, Math.Max(260, Height - ContentTop - 88));

            LayoutActionButtons(cx, tabControl.Bottom + DesignTokens.Spacing.Lg);
        }

        private void LayoutActionButtons(int cx, int actionY)
        {
            if (actionPanel == null || solveButton == null)
                return;

            int btnW = DesignTokens.Sizing.ButtonWidthMd;
            int btnH = DesignTokens.Sizing.ButtonHeight;
            int gap = DesignTokens.Spacing.Md;
            bool twoButtons = showSolutionButton != null && showSolutionButton.Visible;

            int count = twoButtons ? 2 : 1;
            int totalW = btnW * count + gap * (count - 1);
            int startX = cx - totalW / 2;

            solveButton.Location = new Point(0, 0);
            solveButton.Size = new Size(btnW, btnH);

            if (twoButtons)
            {
                showSolutionButton.Location = new Point(btnW + gap, 0);
                showSolutionButton.Size = new Size(btnW, btnH);
            }

            actionPanel.Location = new Point(startX, actionY);
            actionPanel.Size = new Size(totalW, btnH);
        }

        private static string FormatLevel(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
                return "Practice";
            return char.ToUpper(level[0]) + level.Substring(1).ToLower();
        }

        private static Color GetLevelColor(string level)
        {
            switch (level?.Trim().ToLower())
            {
                case "easy":
                    return Color.FromArgb(0, 200, 117);
                case "medium":
                    return Color.FromArgb(255, 183, 64);
                case "hard":
                    return Color.FromArgb(255, 82, 82);
                default:
                    return Theme.Current.SecondaryTextColor;
            }
        }

    }
}
