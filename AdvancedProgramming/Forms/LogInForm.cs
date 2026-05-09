using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;
using AdvancedProgramming.Session;



namespace AdvancedProgramming.Forms
{
    public class LogInForm : Form
    {
        private TextBox userNmaseTextBox;
        private TextBox passwordTextBox;
        private Button logInButton;
        private Button passwordTaggel;
        private bool passwordVasibilty = false;
        private Label Massage;
        private Toolbar toolbar;
        private HomeFarme homeFarme;
        

        public LogInForm()
        {
            InitializeLogInComponents();
        }

        private void InitializeLogInComponents()
        {
            this.Size = new Size(400, 440);
            this.Text = "Log In";
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.homeFarme = new HomeFarme();

            // Add toolbar
            toolbar = new Toolbar(this, "Log In");
            this.Controls.Add(toolbar);

            // Adjust controls position to account for toolbar (toolbar height is 40)
            var userNameLabel = new Label { Text = "Username:", Location = new Point(30, 70), Size = new Size(100, 20) };
            userNmaseTextBox = new TextBox { Location = new Point(140, 68), Size = new Size(200, 20) };

            var passwordLabel = new Label { Text = "Password:", Location = new Point(30, 110), Size = new Size(100, 20) };

            passwordTextBox = new TextBox { Location = new Point(140, 108), Size = new Size(160, 20), PasswordChar = '*' };

            passwordTaggel = new Button { Text = "👁", Location = new Point(305, 106), Size = new Size(30, 23), FlatStyle = FlatStyle.Flat };

            logInButton = new Button { Text = "log in", Location = new Point(140, 200), Size = new Size(100, 30) };
            passwordTaggel.Click += PasswordTaggel_Click;
            logInButton.Click += LogInButton_Click;

            Massage = new Label { Text = "", Location = new Point(30, 290), Size = new Size(320, 60), FlatStyle = FlatStyle.Flat };

            Controls.Add(Massage);
            Controls.Add(passwordTaggel);
            Controls.Add(logInButton);
            Controls.Add(passwordTextBox);
            Controls.Add(userNmaseTextBox);
            Controls.Add(userNameLabel);
            Controls.Add(passwordLabel);

            var btnHome = new Button
            {
                Text = "🏠",
                Location = new Point(270, 200),
                Size = new Size(60, 30),
                FlatStyle = FlatStyle.Flat
            };
            btnHome.Click += (s, e) => this.Close();
            Controls.Add(btnHome);

            // Apply theme
            Theme.Apply(this);
        }

        private void PasswordTaggel_Click(object sender, EventArgs e)
        {
            passwordVasibilty = !passwordVasibilty;
            passwordTextBox.PasswordChar = passwordVasibilty ? '\0' : '*';
            passwordTaggel.Text = passwordVasibilty ? "🙈" : "👁";
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string password = passwordTextBox.Text;
            string username = userNmaseTextBox.Text;
            var user = new UserManagement();

            Massage.Text = "";

            if (string.IsNullOrEmpty(username))
            {
                Massage.Text = "UserName is required, please enter it !!";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                Massage.Text = "Password is required, please enter it !!";
                return;
            }

            if (user.SignIn(username, password))
            {
                Massage.Text = "Log in successful!";
                CurrentUser.Username = username;
                homeFarme.Show();
                this.Close();



            }
            else
                Massage.Text = "Oops!! Log in failed. Invalid username or password.";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (toolbar != null)
                toolbar.UpdateTheme();
        }
   
    
    
    
    
    
    
    
    
    
    
    
    }
}
