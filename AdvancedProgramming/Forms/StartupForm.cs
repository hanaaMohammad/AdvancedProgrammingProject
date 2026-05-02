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
            var btnSignUp = new Button
            {
                Text = "Sign Up",
                Location = new Point(350, 210),
                Size = new Size(100, 30)
            };
            btnSignUp.Click += (s, e) => new SignUpForm().ShowDialog();
            this.Controls.Add(btnSignUp);
        }
    }
}
