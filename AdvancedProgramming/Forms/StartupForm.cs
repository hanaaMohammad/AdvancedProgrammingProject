using AdvancedProgramming.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public partial class StartupForm : Form
    {
        public StartupForm()
        {
            InitializeComponent();
            DatabaseManager.InitializeDatabase();
            InitializeMainForm();
            Theme.Apply(this);
        }

        private void InitializeMainForm()
        {
            this.Text = "Main Form";
            var signUpBitton = new Button
            {
                Text = "Sign Up",
                Location = new Point(350, 210),
                Size = new Size(100, 30)
            };
            var logInButton = new Button
            {
                Text = "Log in",
                Location = new Point(150, 210),
                Size = new Size(100, 30)

            }
            ;
            logInButton.Click += (s, e) => new LogInForm().ShowDialog();
            signUpBitton.Click += (s, e) => new SignUpForm().ShowDialog();
            this.Controls.Add(signUpBitton);
            this.Controls.Add(logInButton);
        }
    }
}
