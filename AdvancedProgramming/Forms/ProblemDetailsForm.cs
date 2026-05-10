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
        private TextBox answerBox;
        private Label answerLabel;
        private Button submitButton;
        private Button backButton;
        private Label feedbackLabel;
        private string problemName;

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
            this.answerLabel = new Label();
            this.answerBox = new TextBox();
            this.submitButton = new Button();
            this.backButton = new Button();
            this.feedbackLabel = new Label();

            this.headerPanel.SuspendLayout();
            this.SuspendLayout();

            this.headerPanel.BackColor = Color.Black;
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Location = new Point(-1, -1);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new Size(755, 100);
            this.headerPanel.TabIndex = 0;

            this.headerLabel.Font = new Font("Microsoft Sans Serif", 16.2F);
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Location = new Point(0, 20);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new Size(755, 60);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = problemName;
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.descriptionLabel.ForeColor = Color.White;
            this.descriptionLabel.Location = new Point(50, 130);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new Size(150, 25);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Description:";

            this.descriptionBox.BackColor = Color.FromArgb(40, 40, 40);
            this.descriptionBox.BorderStyle = BorderStyle.FixedSingle;
            this.descriptionBox.Font = new Font("Microsoft Sans Serif", 11F);
            this.descriptionBox.ForeColor = Color.White;
            this.descriptionBox.Location = new Point(50, 160);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new Size(650, 200);
            this.descriptionBox.TabIndex = 2;
            this.descriptionBox.Text = "";
            this.descriptionBox.ScrollBars = ScrollBars.Vertical;

            this.answerLabel.AutoSize = true;
            this.answerLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.answerLabel.ForeColor = Color.White;
            this.answerLabel.Location = new Point(50, 390);
            this.answerLabel.Name = "answerLabel";
            this.answerLabel.Size = new Size(100, 25);
            this.answerLabel.TabIndex = 3;
            this.answerLabel.Text = "Your Answer:";

            this.answerBox.BackColor = Color.FromArgb(40, 40, 40);
            this.answerBox.BorderStyle = BorderStyle.FixedSingle;
            this.answerBox.Font = new Font("Microsoft Sans Serif", 11F);
            this.answerBox.ForeColor = Color.White;
            this.answerBox.Location = new Point(50, 420);
            this.answerBox.Multiline = true;
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new Size(650, 100);
            this.answerBox.TabIndex = 4;
            this.answerBox.ScrollBars = ScrollBars.Vertical;

            this.submitButton.BackColor = Color.FromArgb(255, 192, 192);
            this.submitButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.submitButton.Location = new Point(450, 540);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new Size(120, 45);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new EventHandler(this.submitButton_Click);

            this.backButton.BackColor = Color.FromArgb(255, 192, 192);
            this.backButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.backButton.Location = new Point(200, 540);
            this.backButton.Name = "backButton";
            this.backButton.Size = new Size(120, 45);
            this.backButton.TabIndex = 6;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new EventHandler(this.backButton_Click);

            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Italic);
            this.feedbackLabel.ForeColor = Color.LightGreen;
            this.feedbackLabel.Location = new Point(50, 600);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new Size(0, 25);
            this.feedbackLabel.TabIndex = 7;
            this.feedbackLabel.Text = "";

            this.BackColor = Color.FromArgb(64, 0, 64);
            this.ClientSize = new Size(755, 700);
            this.Controls.Add(this.feedbackLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.answerLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "ProblemDetailsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string answer = answerBox.Text.Trim();
            if (string.IsNullOrEmpty(answer))
            {
                feedbackLabel.ForeColor = Color.Orange;
                feedbackLabel.Text = "Please enter an answer.";
            }
            else
            {
                feedbackLabel.ForeColor = Color.LightGreen;
                feedbackLabel.Text = "Answer submitted!";
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
