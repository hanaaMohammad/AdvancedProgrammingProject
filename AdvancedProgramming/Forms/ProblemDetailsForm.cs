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

        public ProblemDetailsForm(string problem)
        {
            this.problemName = problem;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.headerLabel = new Label();
            this.descriptionLabel = new Label();
            this.descriptionBox = new TextBox();
            this.solveButton = new Button();
            this.backButton = new Button();
            this.feedbackLabel = new Label();

            this.headerPanel.SuspendLayout();
            this.SuspendLayout();

            this.ClientSize = new Size(755, 700);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Location = new Point(-1, 39);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new Size(755, 80);
            this.headerPanel.TabIndex = 0;

            this.headerLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            this.headerLabel.Location = new Point(0, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new Size(755, 50);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = problemName;
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.descriptionLabel.Location = new Point(50, 140);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new Size(150, 25);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Description:";

            this.descriptionBox.BorderStyle = BorderStyle.FixedSingle;
            this.descriptionBox.Font = new Font("Segoe UI", 11F);
            this.descriptionBox.Location = new Point(50, 175);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new Size(650, 200);
            this.descriptionBox.TabIndex = 2;
            this.descriptionBox.Text = "";
            this.descriptionBox.ScrollBars = ScrollBars.Vertical;

            this.solveButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.solveButton.Location = new Point(300, 430);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new Size(120, 45);
            this.solveButton.TabIndex = 3;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = false;

            this.backButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.backButton.Location = new Point(200, 550);
            this.backButton.Name = "backButton";
            this.backButton.Size = new Size(120, 45);
            this.backButton.TabIndex = 6;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new EventHandler(this.backButton_Click);

            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Font = new Font("Segoe UI", 14F, FontStyle.Italic);
            this.feedbackLabel.ForeColor = Color.LightGreen;
            this.feedbackLabel.Location = new Point(50, 610);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new Size(0, 25);
            this.feedbackLabel.TabIndex = 7;
            this.feedbackLabel.Text = "";

            this.Controls.Add(this.feedbackLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "ProblemDetailsForm";
            Theme.Apply(this);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
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
