using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public partial class StartupForm : Form
    {
        private Toolbar toolbar;

        public StartupForm()
        {
            InitializeComponent();
            DatabaseManager.InitializeDatabase();
            this.FormBorderStyle = FormBorderStyle.None;
            toolbar = new Toolbar(this, "Advanced Programming");
            this.Controls.Add(toolbar);
            InitializeMainForm();
            Theme.Apply(this);
        }



        private void InitializeMainForm()
        {
            this.Text = "Main Form";

            var btnSignUp = new Button
            {
                Text = "Sign Up",
                Location = new Point(350, 210),
                Size = new Size(100, 30)
            };
            btnSignUp.Click += (s, e) => new SignUpForm().ShowDialog();

            this.Controls.Add(btnSignUp);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (toolbar != null)
                toolbar.UpdateTheme();
        }
    }
}
