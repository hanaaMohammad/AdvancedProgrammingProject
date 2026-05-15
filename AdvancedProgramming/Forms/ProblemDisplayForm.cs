using AdvancedProgramming.ProblemClasses;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class ProblemDisplayForm : UserControl
    {
        public event EventHandler SolveRequested;
        public event EventHandler HomeRequested;
        public event EventHandler UserRequested;
        public event EventHandler BackRequested;

        private Toolbar toolbar;
        private TextBox descriptionBox;
        private Label descriptionLabel;
        private Button solveButton;
        private Button backButton;
        private Button homeButton;
        private Button userButton;
        private string problemName;
        private Problem problemChoice;
        private TextBox inputBox;
        private TextBox outputBox;
        private TextBox constraintsBox;
        private TableLayoutPanel tableLayoutPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private TextBox inputExampleBox;
        private TextBox outputExampleBox;
        private TextBox explanationBox;
        private Button showSolutionButton;
        private Label labelSolution;
        private TextBox solutionBox;
        private bool solutionVisible;
        private bool isAvailable;

        public ProblemDisplayForm(string problem)
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent(problem);
        }

        private void InitializeComponent(string problem)
        {
            this.problemName = problem;
            GetProblemDetails();

            if (problemChoice == null)
            {
                MessageBox.Show("Problem not found");
                return;
            }

            isAvailable = ProblemCatalog.IsAvailable(problemName);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(DesignTokens.Spacing.Md, 55, DesignTokens.Spacing.Md, DesignTokens.Spacing.Md),
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            int m = DesignTokens.Spacing.Md;
            int lh = 24;
            int gap = DesignTokens.Spacing.Sm;
            int cw = 470;
            int y;

            leftPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

            y = m;
            descriptionLabel = new Label
            {
                Text = "Description",
                Font = DesignTokens.Typography.HeadingMedium,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            descriptionBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 120,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.description,
                Font = DesignTokens.Typography.BodyMedium,
            };
            y += 120 + gap;

            var lblInput = new Label
            {
                Text = "Input Format",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            inputBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 60,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.input,
                Font = DesignTokens.Typography.BodySmall,
            };
            y += 60 + gap;

            var lblOutput = new Label
            {
                Text = "Output Format",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            outputBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 60,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.output,
                Font = DesignTokens.Typography.BodySmall,
            };
            y += 60 + gap;

            var lblConstraints = new Label
            {
                Text = "Constraints",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            constraintsBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 60,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Constraints,
                Font = DesignTokens.Typography.BodySmall,
            };
            y += 60 + gap + 10;

            int bw = 100;
            int bg = 10;
            solveButton = new Button
            {
                Text = isAvailable ? "Solve" : "Coming Soon",
                Font = DesignTokens.Typography.ButtonLabel,
                Location = new Point(m, y),
                Size = new Size(bw, 40),
                Tag = isAvailable ? "Primary" : "Secondary",
                Cursor = isAvailable ? Cursors.Hand : Cursors.Default,
                Enabled = isAvailable,
            };
            backButton = new Button
            {
                Text = "Back",
                Font = DesignTokens.Typography.BodyMedium,
                Location = new Point(m + bw + bg, y),
                Size = new Size(bw, 40),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            homeButton = new Button
            {
                Text = "Home",
                Font = DesignTokens.Typography.BodyMedium,
                Location = new Point(m + 2 * (bw + bg), y),
                Size = new Size(bw, 40),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            userButton = new Button
            {
                Text = "Profile",
                Font = DesignTokens.Typography.BodyMedium,
                Location = new Point(m + 3 * (bw + bg), y),
                Size = new Size(bw, 40),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };

            bool hasSolution = !string.IsNullOrWhiteSpace(problemChoice.solution);
            showSolutionButton = new Button
            {
                Text = "Show Solution",
                Font = DesignTokens.Typography.BodyMedium,
                Location = new Point(m + 4 * (bw + bg), y),
                Size = new Size(bw + 20, 40),
                Tag = "Ghost",
                Cursor = Cursors.Hand,
                Enabled = hasSolution,
                Visible = isAvailable,
            };

            leftPanel.Controls.AddRange(new Control[] {
                descriptionLabel, descriptionBox,
                lblInput, inputBox,
                lblOutput, outputBox,
                lblConstraints, constraintsBox,
                solveButton, backButton, homeButton, userButton, showSolutionButton,
            });

            rightPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

            y = m;
            var labelExample = new Label
            {
                Text = "Example",
                Font = DesignTokens.Typography.HeadingMedium,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;

            var lblInputExam = new Label
            {
                Text = "Input",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            inputExampleBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 100,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.input ?? "",
                Font = DesignTokens.Typography.Code,
                BackColor = Theme.Current.InputBackColor,
            };
            y += 100 + gap;

            var lblOutputExam = new Label
            {
                Text = "Output",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            outputExampleBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 100,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.output ?? "",
                Font = DesignTokens.Typography.Code,
                BackColor = Theme.Current.InputBackColor,
            };
            y += 100 + gap;

            var labelExplanation = new Label
            {
                Text = "Explanation",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            explanationBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 100,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.explanation ?? "",
                Font = DesignTokens.Typography.BodySmall,
            };

            y += 100 + gap;
            labelSolution = new Label
            {
                Text = "Solution",
                Font = DesignTokens.Typography.HeadingSmall,
                Location = new Point(m, y),
                Size = new Size(cw, lh),
                Visible = false,
            };
            y += lh + 4;
            solutionBox = new TextBox
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 150,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.solution ?? "",
                Font = DesignTokens.Typography.Code,
                BackColor = Theme.Current.InputBackColor,
                Visible = false,
            };

            rightPanel.Controls.AddRange(new Control[] {
                labelExample,
                lblInputExam, inputExampleBox,
                lblOutputExam, outputExampleBox,
                labelExplanation, explanationBox,
                labelSolution, solutionBox,
            });

            PaintStars();

            tableLayoutPanel.Controls.Add(leftPanel, 0, 0);
            tableLayoutPanel.Controls.Add(rightPanel, 1, 0);

            this.Controls.Add(tableLayoutPanel);

            solveButton.Click += (s, e) =>
            {
                if (isAvailable)
                    SolveRequested?.Invoke(this, EventArgs.Empty);
            };
            backButton.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            homeButton.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);
            userButton.Click += (s, e) => UserRequested?.Invoke(this, EventArgs.Empty);
            showSolutionButton.Click += (s, e) => ToggleSolution();

            FormAccessibility.SetShortcutHint(solveButton, "Enter", "Open code editor");
            FormAccessibility.SetShortcutHint(backButton, "Esc", "Go back");
            FormAccessibility.SetShortcutHint(showSolutionButton, "Click", "Toggle solution");

            Theme.StylePage(this);
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
            labelSolution.Visible = solutionVisible;
            solutionBox.Visible = solutionVisible;
        }

        private void GetProblemDetails()
        {
            ProblemLoadReadJs problemLoader = new ProblemLoadReadJs();
            this.problemChoice = problemLoader.getProblemByName(problemName);
        }

        private void PaintStars()
        {
            Components.PanelStars panelStars = new Components.PanelStars();
            panelStars.Location = new Point(outputExampleBox.Left, outputExampleBox.Top);
            panelStars.Width = outputExampleBox.Width;
            panelStars.Height = outputExampleBox.Height;
            if (this.problemChoice.type == "pattren")
            {
                outputExampleBox.Visible = false;
                panelStars.Visible = true;
            }
            else
            {
                outputExampleBox.Visible = true;
                panelStars.Visible = false;
            }
            rightPanel.Controls.Add(panelStars);
            panelStars.BringToFront();
            panelStars.Invalidate();
        }
    }
}
