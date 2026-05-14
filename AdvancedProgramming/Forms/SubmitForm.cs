using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class SubmitForm : Form
    {
        private Toolbar toolbar;
        private ComboBox languageCombo;
        private TextBox codeEditor;
        private Button submitButton;
        private Button homeButton;
        private Label titleLabel;
        private Label languageLabel;
     
        private Problem problemChoice;

        public SubmitForm(Problem problem)
        {
            if (problem == null) {
                MessageBox.Show("Problem is null");
                this.Close();
                return;

            }
            this.problemChoice = problem;
            InitializeComponent();
        }

    

        private void InitializeComponent()
        {
            this.Text = "Submit Code";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(900, 700);
           

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            titleLabel = new Label
            {
                Text = "Submit Your Solution",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 50),
                Location = new Point((this.ClientSize.Width - 400) / 2, 70),
            };

            languageLabel = new Label
            {
                Text = "Language:",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(100, 30),
                Location = new Point(40, 135),
            };

            languageCombo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12F),
                Size = new Size(200, 30),
                Location = new Point(150, 133),
                FlatStyle = FlatStyle.Flat,
                DrawMode = DrawMode.OwnerDrawFixed,
            };
            languageCombo.Items.Add("C++");
            languageCombo.Items.Add("Java");
            languageCombo.SelectedIndex = 0;
            languageCombo.DrawItem += LanguageCombo_DrawItem;

            codeEditor = new TextBox
            {
                Multiline = true,
                Font = new Font("Consolas", 11F),
                Size = new Size(800, 400),
                Location = new Point(40, 180),
                ScrollBars = ScrollBars.Both,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "// Write your code here\n",
            };

            int btnY = this.ClientSize.Height - 80;

            submitButton = new Button
            {
                Text = "Submit",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(140, 45),
                Location = new Point(40, btnY),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            };
            submitButton.Click += SubmitButton_Click;

            homeButton = new Button
            {
                Text = "Home",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(140, 45),
                Location = new Point(200, btnY),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            };
            homeButton.Click += HomeButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(languageLabel);
            this.Controls.Add(languageCombo);
            this.Controls.Add(codeEditor);
            this.Controls.Add(submitButton);
            this.Controls.Add(homeButton);

            Theme.Apply(this);
        }

        private void LanguageCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            string text = languageCombo.Items[e.Index].ToString();
            Color color = text == "C++" ? Color.FromArgb(0, 120, 215) : Color.FromArgb(237, 125, 49);
            using (Brush brush = new SolidBrush(color))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
    string name=problemChoice.title;
    string language = languageCombo.SelectedItem.ToString();
    string code = codeEditor.Text;
            RunCode run = new RunCode(problemChoice);


























        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            var home = new HomeFarme();
            home.Show();
            this.Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
            if (submitButton == null) return;
            int btnY = this.ClientSize.Height - 80;
            submitButton.Location = new Point(40, btnY);
            homeButton.Location = new Point(200, btnY);
            codeEditor.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 270);
        }
      
    }
}
