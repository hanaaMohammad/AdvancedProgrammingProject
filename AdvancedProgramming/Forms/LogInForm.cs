using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;
using AdvancedProgramming.Session;

namespace AdvancedProgramming.Forms
{
    public class LogInForm : UserControl
    {
        public event EventHandler LoginSuccess;
        public event EventHandler BackRequested;

        private TextBox userNmaseTextBox;
        private TextBox passwordTextBox;
        private Button logInButton;
        private Button passwordTaggel;
        private bool passwordVasibilty = false;
        private Label Massage;
        private Toolbar toolbar;

        public LogInForm()
        {
            this.SuspendLayout();
            this.Size = new Size(1100, 800);
            InitializeLogInComponents();
            toolbar = new Toolbar(this, "Log In");
            this.Controls.Add(toolbar);
            this.ResumeLayout(false);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            Theme.Apply(this);
        }

        private void InitializeLogInComponents()
        {
            int cx = this.Width / 2;
            int labelX = cx - 200;
            int inputX = cx - 100;
            int inputW = 250;
            int toggleX = inputX + inputW - 35;
            int rowY = 130;
            int rowGap = 48;

            var userNameLabel = new Label { Text = "Username", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            userNmaseTextBox = new TextBox { Location = new Point(inputX, rowY), Size = new Size(inputW, 24) };

            rowY += rowGap;
            var passwordLabel = new Label { Text = "Password", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            passwordTextBox = new TextBox { Location = new Point(inputX, rowY), Size = new Size(inputW - 35, 24), PasswordChar = '*' };
            passwordTaggel = new Button { Text = "\U0001f441", Location = new Point(toggleX, rowY), Size = new Size(30, 24), FlatStyle = FlatStyle.Flat };
            passwordTaggel.FlatAppearance.BorderSize = 0;
            passwordTaggel.Click += PasswordTaggel_Click;

            rowY += rowGap + 25;
            logInButton = new Button { Text = "Log In", Location = new Point(cx - 90, rowY), Size = new Size(180, 50), FlatStyle = FlatStyle.Flat };
            logInButton.FlatAppearance.BorderSize = 0;
            logInButton.Click += LogInButton_Click;

            var btnBack = new Button
            {
                Text = "\u2190 Back",
                Location = new Point(this.Width - 85, 62),
                Size = new Size(70, 26),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
            };
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            rowY += 60;
            Massage = new Label { Text = "", Location = new Point(labelX, rowY), Size = new Size(370, 50), TextAlign = ContentAlignment.MiddleCenter };

            this.Controls.AddRange(new Control[] {
                userNameLabel, userNmaseTextBox,
                passwordLabel, passwordTextBox, passwordTaggel,
                logInButton, btnBack,
                Massage
            });
        }

        private void PasswordTaggel_Click(object sender, EventArgs e)
        {
            passwordVasibilty = !passwordVasibilty;
            passwordTextBox.PasswordChar = passwordVasibilty ? '\0' : '*';
            passwordTaggel.Text = passwordVasibilty ? "\U0001f648" : "\U0001f441";
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
                var details = user.GetUserDetails(username);
                CurrentUser.Country = details.Country;
                CurrentUser.Gender = details.Gender;
                CurrentUser.Score = user.GetScore(username);
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
                Massage.Text = "Oops!! Log in failed. Invalid username or password.";
        }
    }
}
