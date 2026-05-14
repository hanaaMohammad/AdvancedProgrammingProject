using AdvancedProgramming.ProblemClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    internal class ProblemDisplayForm : Form
    {
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
        private Panel DrawPanel;

        public ProblemDisplayForm(string problem)
        {
            InitializeComponent(problem);
        }

        private void InitializeComponent(string problem)
        {
            this.problemName = problem;
            GetProblemDetails();

            if (problemChoice == null)
            {
                MessageBox.Show("Problem not found");
                this.Close();
                return;
            }

            this.ClientSize = new Size(1100, 700);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10, 50, 10, 10),
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            descriptionLabel = new Label()
            {
                Text = "Description",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(200, 25),
            };

            descriptionBox = new TextBox()
            {
                Location = new Point(10, 40),
                Width = 490,
                Height = 120,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.description,
            };

            var labelInput = new Label()
            {
                Text = "Input",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 170),
                Size = new Size(200, 20),
            };
            input = new TextBox()
            {
                Location = new Point(10, 195),
                Width = 490,
                Height = 60,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.input,
            };

            var labelOutput = new Label()
            {
                Text = "Output",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 265),
                Size = new Size(200, 20),
            };
            output = new TextBox()
            {
                Location = new Point(10, 290),
                Width = 490,
                Height = 60,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.output,
            };

            var labelNote = new Label()
            {
                Text = "Note",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 360),
                Size = new Size(200, 20),
            };
            Note = new TextBox()
            {
                Location = new Point(10, 385),
                Width = 490,
                Height = 60,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Constraints,
            };

            LeftPanel = new Panel() { Dock = DockStyle.Fill };
            LeftPanel.Controls.Add(descriptionLabel);
            LeftPanel.Controls.Add(descriptionBox);
            LeftPanel.Controls.Add(labelInput);
            LeftPanel.Controls.Add(input);
            LeftPanel.Controls.Add(labelOutput);
            LeftPanel.Controls.Add(output);
            LeftPanel.Controls.Add(labelNote);
            LeftPanel.Controls.Add(Note);

            var labelExample = new Label()
            {
                Text = "Example",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(200, 25),
            };

            var labelInputExam = new Label()
            {
                Text = "Input",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 45),
                Size = new Size(200, 20),
            };
            InputExam = new TextBox()
            {
                Location = new Point(10, 70),
                Width = 490,
                Height = 80,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.input ?? ""
            };

            var labelOutputExam = new Label()
            {
                Text = "Output",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 160),
                Size = new Size(200, 20),
            };
            OutputExam = new TextBox()
            {
                Location = new Point(10, 185),
                Width = 490,
                Height = 80,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.output ?? ""
            };

            var labelExplanation = new Label()
            {
                Text = "Explanation",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 275),
                Size = new Size(200, 20),
            };
            explation = new TextBox()
            {
                Location = new Point(10, 300),
                Width = 490,
                Height = 80,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Multiline = true,
                ReadOnly = true,
                Text = problemChoice.Example?.explanation ?? ""
            };

            RightPanel = new Panel() { Dock = DockStyle.Fill };
            RightPanel.Controls.Add(labelExample);
            RightPanel.Controls.Add(labelInputExam);
            RightPanel.Controls.Add(InputExam);
            RightPanel.Controls.Add(labelOutputExam);
            RightPanel.Controls.Add(OutputExam);
            RightPanel.Controls.Add(labelExplanation);
            RightPanel.Controls.Add(explation);

            PaintStars();

            tableLayoutPanel.Controls.Add(LeftPanel, 0, 0);
            tableLayoutPanel.Controls.Add(RightPanel, 1, 0);
            

            solveButton = new Button()
            {
                Text = "Solve",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 460),
                Size = new Size(100, 35),
            };
            solveButton.Click += solveButton_Click;

            backButton = new Button()
            {
                Text = "Back",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(120, 460),
                Size = new Size(100, 35),
            };
            backButton.Click += backButton_Click;

            homeButton = new Button()
            {
                Text = "Home",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(230, 460),
                Size = new Size(100, 35),
            };
            homeButton.Click += homeButton_Click;

            userButton = new Button()
            {
                Text = "User",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(340, 460),
                Size = new Size(100, 35),
            };
            userButton.Click += userButton_Click;

            LeftPanel.Controls.Add(solveButton);
            LeftPanel.Controls.Add(backButton);
            LeftPanel.Controls.Add(homeButton);
            LeftPanel.Controls.Add(userButton);

            this.Controls.Add(tableLayoutPanel);

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            Theme.Apply(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

        private void solveButton_Click(object sender, EventArgs e)
        {

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            var home = new HomeFarme();
            home.Show();
            this.Close();
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            var usre = new UsreForm();
            usre.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetProblemDetails()
        {
            ProblemLoadReadJs problemLoader = new ProblemLoadReadJs();
            this.problemChoice = problemLoader.getProblemByName(problemName);
        }
        private void PaintStars()
        {
            Components.PanelStars panelStars = new Components.PanelStars();
            panelStars.Location = new Point(10, 185);
            panelStars.Width = 490;
            panelStars.Height = 80;
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
