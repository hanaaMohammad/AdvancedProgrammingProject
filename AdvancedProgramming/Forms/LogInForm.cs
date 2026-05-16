using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Components;
using AdvancedProgramming.Session;


namespace AdvancedProgramming.Forms
{
    public class LogInForm : UserControl
    {
        public event EventHandler LoginSuccess;
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button logInButton;
        private Button passwordToggle;
        private bool passwordVisible = false;
        private Label messageLabel;
        private Toolbar toolbar;

        public LogInForm()
        {
            this.SuspendLayout();
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            toolbar = new Toolbar(this, "Log In");
            this.Controls.Add(toolbar);
            InitializeLogInComponents();
            this.ResumeLayout(false);
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Theme.StylePage(this);

            FormAccessibility.SetShortcutHint(logInButton, "Enter", "Sign in");
            FormAccessibility.SetShortcutHint(usernameTextBox, "Tab", "Username");
        }

        public void SubmitLogin()
        {
            LogInButton_Click(logInButton, EventArgs.Empty);
        }

        private void InitializeLogInComponents()
        {
            int cx = this.Width / 2;
            int formW = 400;
            int leftX = cx - formW / 2;
            int inputW = DesignTokens.Sizing.InputWidth;
            int inputX = cx - inputW / 2;
            int labelX = cx - inputW / 2;
            int rowY = 180;
            int rowGap = 60;

            var headerLabel = new Label
            {
                Text = "Welcome back",
                Font = DesignTokens.Typography.DisplaySmall,
                AutoSize = false,
                Size = new Size(formW, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(leftX, 120),
            };
            var subheadLabel = new Label
            {
                Text = "Sign in to continue solving challenges",
                Font = DesignTokens.Typography.BodyMedium,
                AutoSize = false,
                Size = new Size(formW, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(leftX, 165),
                Tag = "Secondary",
            };

            var userNameLabel = new Label
            {
                Text = "Username",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            usernameTextBox = new TextBox
            {
                Location = new Point(inputX, rowY + 26),
                Size = new Size(inputW, DesignTokens.Sizing.InputHeight),
                Font = DesignTokens.Typography.BodyMedium,
            };

            rowY += rowGap;
            var passwordLabel = new Label
            {
                Text = "Password",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            passwordTextBox = new TextBox
            {
                Location = new Point(inputX, rowY + 26),
                Size = new Size(inputW - 40, DesignTokens.Sizing.InputHeight),
                PasswordChar = '*',
                Font = DesignTokens.Typography.BodyMedium,
            };
            passwordToggle = new Button
            {
                Text = "\U0001f441",
                Location = new Point(inputX + inputW - 38, rowY + 26),
                Size = new Size(36, DesignTokens.Sizing.InputHeight),
                FlatStyle = FlatStyle.Flat,
                Tag = "Ghost",
                Font = DesignTokens.Typography.BodyMedium,
            };
            passwordToggle.FlatAppearance.BorderSize = 0;
            passwordToggle.Click += PasswordToggle_Click;

            rowY += rowGap + 20;
            logInButton = new Button
            {
                Text = "Log In",
                Size = new Size(inputW, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(inputX, rowY),
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.ButtonLabel,
                Tag = "Primary",
                Cursor = Cursors.Hand,
            };
            logInButton.Click += LogInButton_Click;

            rowY += 80;
            messageLabel = new Label
            {
                Text = "",
                Location = new Point(leftX, rowY),
                Size = new Size(formW, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Error",
            };

            var (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));

            this.Controls.AddRange(new Control[] {
                headerLabel, subheadLabel,
                userNameLabel, usernameTextBox,
                passwordLabel, passwordTextBox, passwordToggle,
                logInButton, btnBack, btnHome,
                messageLabel
            });
        }

        private void PasswordToggle_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            passwordTextBox.PasswordChar = passwordVisible ? '\0' : '*';
            passwordToggle.Text = passwordVisible ? "\U0001f648" : "\U0001f441";
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string password = passwordTextBox.Text;
            string username = usernameTextBox.Text;
            var user = new UserManagement();

            messageLabel.Text = "";

            if (string.IsNullOrEmpty(username))
            {
                messageLabel.Text = "Username is required";
                messageLabel.Tag = "Error";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                messageLabel.Text = "Password is required";
                messageLabel.Tag = "Error";
                return;
            }

            if (user.SignIn(username, password))
            {
                messageLabel.Text = "";
                CurrentUser.Username = username;
                var details = user.GetUserDetails(username);
                CurrentUser.Country = details.Country;
                CurrentUser.Gender = details.Gender;
                CurrentUser.Score = user.GetScore(username);
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                messageLabel.Text = "Invalid username or password";
                messageLabel.Tag = "Error";
            }
        }
    }
}
