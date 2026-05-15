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
        private TextBox input;
        private TextBox output;
        private TextBox Note;
        private TableLayoutPanel tableLayoutPanel;
        private Panel LeftPanel;
        private Panel RightPanel;
        private TextBox InputExam;
        private TextBox OutputExam;
        private TextBox explation;

        public ProblemDisplayForm(string problem)
        {
            this.Size = new Size(1100, 800);
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

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10, 50, 10, 10),
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            int m = 10;
            int lh = 22;
            int gap = 8;
            int cw = 490;
            int y;

            LeftPanel = new Panel() { Dock = DockStyle.Fill };

            y = m;
            descriptionLabel = new Label()
            {
                Text = "Description",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            descriptionBox = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 130,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.description,
            };
            y += 130 + gap;

            var lblInput = new Label()
            {
                Text = "Input",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            input = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 70,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.input,
            };
            y += 70 + gap;

            var lblOutput = new Label()
            {
                Text = "Output",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            output = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 70,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.output,
            };
            y += 70 + gap;

            var lblNote = new Label()
            {
                Text = "Constraints",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            Note = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 70,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.Constraints,
            };
            y += 70 + gap + 10;

            int bw = 100;
            int bg = 10;
            solveButton = new Button()
            {
                Text = "Solve",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(bw, 35),
            };
            backButton = new Button()
            {
                Text = "Back",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m + bw + bg, y),
                Size = new Size(bw, 35),
            };
            homeButton = new Button()
            {
                Text = "Home",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m + 2 * (bw + bg), y),
                Size = new Size(bw, 35),
            };
            userButton = new Button()
            {
                Text = "User",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m + 3 * (bw + bg), y),
                Size = new Size(bw, 35),
            };

            LeftPanel.Controls.AddRange(new Control[] {
                descriptionLabel, descriptionBox,
                lblInput, input,
                lblOutput, output,
                lblNote, Note,
                solveButton, backButton, homeButton, userButton,
            });

            RightPanel = new Panel() { Dock = DockStyle.Fill };

            y = m;
            var labelExample = new Label()
            {
                Text = "Example",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;

            var lblInputExam = new Label()
            {
                Text = "Input",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            InputExam = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 120,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.Example?.input ?? "",
            };
            y += 120 + gap;

            var lblOutputExam = new Label()
            {
                Text = "Output",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            OutputExam = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 120,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.Example?.output ?? "",
            };
            y += 120 + gap;

            var labelExplanation = new Label()
            {
                Text = "Explanation",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(m, y),
                Size = new Size(cw, lh),
            };
            y += lh + 4;
            explation = new TextBox()
            {
                Location = new Point(m, y),
                Width = cw,
                Height = 120,
                Multiline = true,
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = problemChoice.Example?.explanation ?? "",
            };

            RightPanel.Controls.AddRange(new Control[] {
                labelExample,
                lblInputExam, InputExam,
                lblOutputExam, OutputExam,
                labelExplanation, explation,
            });

            PaintStars();

            tableLayoutPanel.Controls.Add(LeftPanel, 0, 0);
            tableLayoutPanel.Controls.Add(RightPanel, 1, 0);

            this.Controls.Add(tableLayoutPanel);

            solveButton.Click += solveButton_Click;
            backButton.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            homeButton.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);
            userButton.Click += (s, e) => UserRequested?.Invoke(this, EventArgs.Empty);

            Theme.Apply(this);
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            SolveRequested?.Invoke(this, EventArgs.Empty);
        }

        private void GetProblemDetails()
        {
            ProblemLoadReadJs problemLoader = new ProblemLoadReadJs();
            this.problemChoice = problemLoader.getProblemByName(problemName);
        }

        private void PaintStars()
        {
            Components.PanelStars panelStars = new Components.PanelStars();
            panelStars.Location = new Point(OutputExam.Left, OutputExam.Top);
            panelStars.Width = OutputExam.Width;
            panelStars.Height = OutputExam.Height;
            if (this.problemChoice.type == "pattren")
            {
                OutputExam.Visible = false;
                panelStars.Visible = true;
            }
            else
            {
                OutputExam.Visible = true;
                panelStars.Visible = false;
            }
            RightPanel.Controls.Add(panelStars);
            panelStars.BringToFront();
            panelStars.Invalidate();
        }
    }
}
