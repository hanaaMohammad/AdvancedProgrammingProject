using AdvancedProgramming.ProblemClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace AdvancedProgramming.Forms
{
    internal class ProblemDisplayForm : Form
    {
        private TextBox descriptionBox;
        private Label descriptionLabel;
        private Button solveButton;
        private Button backButton;
        private string problemName;
        private Toolbar toolbar;
        private Problem problemChoice;



        public ProblemDisplayForm(string problem)
        {
            this.problemName = problem;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            toolbar = new Toolbar(this, problemName)
            {
                BackColor = Color.FromArgb(20, 20, 35),
                Dock = DockStyle.Top,
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(1343, 40),
                TabIndex = 0
            };
            this.toolbar.Paint += new PaintEventHandler(this.toolbar_Paint);
            GetProblemDetails();

            this.descriptionLabel = new Label()
            {
                Location = new Point(0, 0),
                Size = new Size(100, 23),
                TabIndex = 2
            };

            this.descriptionBox = new TextBox()
            {
                Location = new Point(0, 0),
                Size = new Size(100, 26),
                TabIndex = 3
            };

         
            this.solveButton = new Button()
            {
                Location = new Point(0, 0),
           
                Size = new Size(75, 23),
                TabIndex = 4
            };

       
            this.backButton = new Button()
            {
                Location = new Point(0, 0),
           
                Size = new Size(75, 23),
                TabIndex = 5
            };
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
         
            this.ClientSize = new System.Drawing.Size(1343, 713);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.backButton);
          
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
   
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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

        private void toolbar_Paint(object sender, PaintEventArgs e)
        {

        }
        private void GetProblemDetails()
        {
            ProbleLoadReadJs problemLoader = new ProbleLoadReadJs();
            this.problemChoice = problemLoader.getProblemByName(problemName);
            
        }
    }
}