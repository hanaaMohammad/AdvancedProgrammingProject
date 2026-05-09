using AdvancedProgramming.Forms;
using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class SignUpForm : Form
    {
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Button signUpButton;
        private Button passwordButtonToggle;
        private Button confirmPasswordButtonToggle;
        private Label messageLabel;
        private bool passwordVisible = false;// default echo // when click show
        private bool confirmPasswordVisible = false;
        private UserManagement userManager;
        private Toolbar toolbar;
        private HomeFarme homeFarme;

        public SignUpForm()
        {
            InitializeSignUpComponents();
            this.FormBorderStyle = FormBorderStyle.None;
            toolbar = new Toolbar(this, "Sign Up");
            this.Controls.Add(toolbar);
            Theme.Apply(this);
            userManager = new UserManagement();
            DatabaseManager.InitializeDatabase();
        }

        private void InitializeSignUpComponents()
        {
            this.Text = "Sign Up";
            this.Size = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            var usernameLabel = new Label { Text = "Username:", Location = new Point(30, 70), Size = new Size(100, 20) };
            userNameTextBox = new TextBox { Location = new Point(140, 68), Size = new Size(200, 20) };
            

            var passwordLabel = new Label { Text = "Password:", Location = new Point(30, 110), Size = new Size(100, 20) };
            passwordTextBox = new TextBox { Location = new Point(140, 108), Size = new Size(160, 20), PasswordChar = '*' };
            passwordButtonToggle = new Button { Text = "👁", Location = new Point(305, 106), Size = new Size(30, 23), FlatStyle = FlatStyle.Flat };
            passwordButtonToggle.Click += TogglePasswordVisibility;

            var confirmLabel = new Label { Text = "Confirm:", Location = new Point(30, 150), Size = new Size(100, 20) };
            confirmPasswordTextBox = new TextBox { Location = new Point(140, 148), Size = new Size(160, 20), PasswordChar = '*' };
            confirmPasswordButtonToggle = new Button { Text = "👁", Location = new Point(305, 146), Size = new Size(30, 23), FlatStyle = FlatStyle.Flat };
            confirmPasswordButtonToggle.Click += confirmPasswordVisibilityTaggel;

            signUpButton = new Button { Text = "Sign Up", Location = new Point(140, 200), Size = new Size(100, 30) };
            signUpButton.Click += signUpButton_Click;

            messageLabel = new Label { Location = new Point(30, 250), Size = new Size(320, 60), ForeColor = Color.Red };

            this.Controls.AddRange(new Control[] { usernameLabel, userNameTextBox, passwordLabel, passwordTextBox, passwordButtonToggle,
                confirmLabel, confirmPasswordTextBox, confirmPasswordButtonToggle, signUpButton, messageLabel });
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            passwordTextBox.PasswordChar = passwordVisible ? '\0' : '*';
            passwordButtonToggle.Text = passwordVisible ? "🙈" : "👁";
        }

        private void confirmPasswordVisibilityTaggel(object sender, EventArgs e)
        {
            confirmPasswordVisible = !confirmPasswordVisible;
            confirmPasswordTextBox.PasswordChar = confirmPasswordVisible ? '\0' : '*';
            confirmPasswordButtonToggle.Text = confirmPasswordVisible ? "🙈" : "👁";
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            string username = userNameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string confirm = confirmPasswordTextBox.Text;

            if (string.IsNullOrEmpty(username))
            {
                messageLabel.Text = "Username is required";
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                messageLabel.Text = "Password is required";
                return;
            }

            if (password.Length < 4)
            {
                messageLabel.Text = "Password must be at least 4 characters";
                return;
            }

            if (password != confirm)
            {
                messageLabel.Text = "Passwords do not match";
                return;
            }

            if (userManager.UsernameExists(username))
            {
                messageLabel.Text = "Username already exists";
                return;
            }

            if (userManager.SignUp(username, password))
            {
                messageLabel.ForeColor = Color.Green;
                messageLabel.Text = "Sign up successful!";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                confirmPasswordTextBox.Text = "";
                CurrentUser.Username = username;
                homeFarme = new HomeFarme();
                homeFarme.Show();
                this.Close();

            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (toolbar != null)
                toolbar.UpdateTheme();
        }
    }
}
