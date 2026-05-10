using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    internal class ProblemDetailsForm : Form
    {
        private Label headerLabel;
        private Panel headerPanel;
        private TextBox descriptionBox;
        private Label descriptionLabel;
        private Button solveButton;
        private Button backButton;
        private Label feedbackLabel;
        private string problemName;
        private Toolbar toolbar;
        private Label ColorDiscrip;
        private Label ColorRed;
        private Label ColorGreen;
        private Label ColorYellow;


        public ProblemDetailsForm(string problem)
        {
            this.problemName = problem;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(755, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "ProblemDetailsForm";

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            headerPanel = new Panel()
            {
                Location = new Point(-1, 39),
                Name = "headerPanel",
                Size = new Size(755, 80),
                TabIndex = 0
            };

            headerLabel = new Label()
            {
                Font = new Font("Segoe UI", 16.2F, FontStyle.Bold),
                Location = new Point(0, 15),
                Name = "headerLabel",
                Size = new Size(755, 50),
                TabIndex = 0,
                Text = problemName,
                TextAlign = ContentAlignment.MiddleCenter
            };

            descriptionLabel = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(50, 140),
                Name = "descriptionLabel",
                Size = new Size(150, 25),
                TabIndex = 1,
                Text = "Description:"
            };

            descriptionBox = new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(50, 175),
                Multiline = true,
                Name = "descriptionBox",
                ReadOnly = true,
                Size = new Size(650, 180),
                TabIndex = 2,
                Text = "",
                ScrollBars = ScrollBars.Vertical
            };

            ColorDiscrip = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(50, 380),
                Name = "ColorDiscrip",
                Size = new Size(150, 25),
                TabIndex = 8,
                Text = "Color Description:"
            };

            ColorRed = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Red,
                Location = new Point(50, 410),
                Name = "ColorRed",
                Size = new Size(150, 25),
                TabIndex = 9,
                Text = "Red: Hard level"
            };

            ColorGreen = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(50, 440),
                Name = "ColorGreen",
                Size = new Size(150, 25),
                TabIndex = 10,
                Text = "Green: Easy level"
            };

            ColorYellow = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Yellow,
                Location = new Point(50, 470),
                Name = "ColorYellow",
                Size = new Size(150, 25),
                TabIndex = 11,
                Text = "Yellow: Medium level"
            };

            solveButton = new Button()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(317, 520),
                Name = "solveButton",
                Size = new Size(120, 45),
                TabIndex = 3,
                Text = "Solve",
                UseVisualStyleBackColor = false
            };

            backButton = new Button()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(317, 580),
                Name = "backButton",
                Size = new Size(120, 45),
                TabIndex = 6,
                Text = "Back",
                UseVisualStyleBackColor = false
            };
            backButton.Click += new EventHandler(this.backButton_Click);

            feedbackLabel = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                ForeColor = Color.LightGreen,
                Location = new Point(50, 650),
                Name = "feedbackLabel",
                Size = new Size(0, 25),
                TabIndex = 7,
                Text = ""
            };

            headerPanel.Controls.Add(headerLabel);

            this.Controls.Add(headerPanel);
            this.Controls.Add(descriptionLabel);
            this.Controls.Add(descriptionBox);
            this.Controls.Add(ColorDiscrip);
            this.Controls.Add(ColorRed);
            this.Controls.Add(ColorGreen);
            this.Controls.Add(ColorYellow);
            this.Controls.Add(solveButton);
            this.Controls.Add(backButton);
            this.Controls.Add(feedbackLabel);

            Theme.Apply(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}