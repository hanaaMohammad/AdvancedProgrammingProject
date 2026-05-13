using System;
using System.Drawing;
using System.Windows.Forms;
namespace AdvancedProgramming.Forms
{
    internal class ProblemDisplay : Form
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
   


        public ProblemDisplay(string problem)
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
               
            };

            headerLabel = new Label()
            {
                Font = new Font("Segoe UI", 16.2F, FontStyle.Bold),
                Location = new Point(0, 15),
                Size = new Size(755, 50),
            
                Text = problemName,
                TextAlign = ContentAlignment.MiddleCenter
            };


            descriptionLabel = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(50, 140),
                Size = new Size(150, 25),
             
                Text = "Description:"
            };

            descriptionBox = new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(50, 175),
                Multiline = true,
      
                ReadOnly = true,
                Size = new Size(650, 180),
              
                Text = "",
                ScrollBars = ScrollBars.Vertical
            };

          
            solveButton = new Button()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(317, 520),
               
                Size = new Size(120, 45),
                TabIndex = 1,
                Text = "Solve",
                UseVisualStyleBackColor = false
            };

            backButton = new Button()
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(317, 580),
                Size = new Size(120, 45),
                TabIndex = 2,
                Text = "Back",
                UseVisualStyleBackColor = false
            };
            backButton.Click += new EventHandler(this.backButton_Click);
//???
            feedbackLabel = new Label()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                ForeColor = Color.LightGreen,
                Location = new Point(50, 650),
                Size = new Size(0, 25),
                TabIndex = 7,
                Text = ""
            };

            headerPanel.Controls.Add(headerLabel);

            this.Controls.Add(headerPanel);
            this.Controls.Add(descriptionLabel);
            this.Controls.Add(descriptionBox);
            
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