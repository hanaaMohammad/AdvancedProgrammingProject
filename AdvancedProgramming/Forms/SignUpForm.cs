using AdvancedProgramming.Components;
using AdvancedProgramming.Forms;
using AdvancedProgramming.Session;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class SignUpForm : UserControl
    {
        public event EventHandler SignUpSuccess;
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Button signUpButton;
        private Button passwordToggle;
        private Button confirmPasswordToggle;
        private Label messageLabel;
        private bool passwordVisible = false;
        private bool confirmPasswordVisible = false;
        private UserManagement userManager;
        private Toolbar toolbar;
        private ComboBox countryCombo;
        private GroupBox genderGroup;
        private RadioButton maleRadio;
        private RadioButton femaleRadio;

        public SignUpForm()
        {
            this.SuspendLayout();
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeSignUpComponents();
            toolbar = new Toolbar(this, "Sign Up");
            this.Controls.Add(toolbar);
            PageBackButton.AddTo(this,
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));
            this.ResumeLayout(false);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            Theme.StylePage(this);
            userManager = new UserManagement();
            DatabaseManager.InitializeDatabase();
        }

        private void InitializeSignUpComponents()
        {
            int cx = this.Width / 2;
            int formW = 420;
            int leftX = cx - formW / 2;
            int inputW = DesignTokens.Sizing.InputWidth;
            int inputX = cx - inputW / 2;
            int labelX = cx - inputW / 2;
            int rowY = 130;
            int rowGap = 56;

            var headerLabel = new Label
            {
                Text = "Create your account",
                Font = DesignTokens.Typography.DisplaySmall,
                AutoSize = false,
                Size = new Size(formW, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(leftX, 80),
            };
            var subheadLabel = new Label
            {
                Text = "Join MiniCamp and start solving puzzles",
                Font = DesignTokens.Typography.BodyMedium,
                AutoSize = false,
                Size = new Size(formW, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(leftX, 122),
                Tag = "Secondary",
            };

            var usernameLabel = new Label
            {
                Text = "Username",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            userNameTextBox = new TextBox
            {
                Location = new Point(inputX, rowY + 24),
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
                Location = new Point(inputX, rowY + 24),
                Size = new Size(inputW - 40, DesignTokens.Sizing.InputHeight),
                PasswordChar = '*',
                Font = DesignTokens.Typography.BodyMedium,
            };
            passwordToggle = new Button
            {
                Text = "\U0001f441",
                Location = new Point(inputX + inputW - 38, rowY + 24),
                Size = new Size(36, DesignTokens.Sizing.InputHeight),
                FlatStyle = FlatStyle.Flat,
                Tag = "Ghost",
            };
            passwordToggle.FlatAppearance.BorderSize = 0;
            passwordToggle.Click += TogglePasswordVisibility;

            rowY += rowGap;
            var confirmLabel = new Label
            {
                Text = "Confirm Password",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            confirmPasswordTextBox = new TextBox
            {
                Location = new Point(inputX, rowY + 24),
                Size = new Size(inputW - 40, DesignTokens.Sizing.InputHeight),
                PasswordChar = '*',
                Font = DesignTokens.Typography.BodyMedium,
            };
            confirmPasswordToggle = new Button
            {
                Text = "\U0001f441",
                Location = new Point(inputX + inputW - 38, rowY + 24),
                Size = new Size(36, DesignTokens.Sizing.InputHeight),
                FlatStyle = FlatStyle.Flat,
                Tag = "Ghost",
            };
            confirmPasswordToggle.FlatAppearance.BorderSize = 0;
            confirmPasswordToggle.Click += ToggleConfirmPasswordVisibility;

            rowY += rowGap;
            var countryLabel = new Label
            {
                Text = "Country",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            countryCombo = new ComboBox
            {
                Location = new Point(inputX, rowY + 24),
                Size = new Size(inputW, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = DesignTokens.Typography.BodyMedium,
            };
            countryCombo.Items.AddRange(new object[] { "Palestine", "Jordan", "Lebanon", "Egypt", "US", "UK" });
            countryCombo.SelectedIndex = 0;

            rowY += rowGap;
            var genderLabel = new Label
            {
                Text = "Gender",
                Location = new Point(labelX, rowY + 2),
                Size = new Size(inputW, DesignTokens.Sizing.LabelHeight),
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Secondary",
            };
            genderGroup = new GroupBox
            {
                Location = new Point(inputX, rowY + 24),
                Size = new Size(inputW, 40),
            };
            maleRadio = new RadioButton { Text = "Male", Location = new Point(12, 10), Size = new Size(80, 22) };
            femaleRadio = new RadioButton { Text = "Female", Location = new Point(inputW / 2 + 10, 10), Size = new Size(90, 22) };
            genderGroup.Controls.Add(maleRadio);
            genderGroup.Controls.Add(femaleRadio);
            maleRadio.Checked = true;

            rowY += rowGap + 20;
            signUpButton = new Button
            {
                Text = "Sign Up",
                Size = new Size(inputW, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(inputX, rowY),
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.ButtonLabel,
                Tag = "Primary",
                Cursor = Cursors.Hand,
            };
            signUpButton.Click += SignUpButton_Click;

            rowY += 75;
            messageLabel = new Label
            {
                Location = new Point(leftX, rowY),
                Size = new Size(formW, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Error",
            };

            this.Controls.AddRange(new Control[] {
                headerLabel, subheadLabel,
                usernameLabel, userNameTextBox,
                passwordLabel, passwordTextBox, passwordToggle,
                confirmLabel, confirmPasswordTextBox, confirmPasswordToggle,
                countryLabel, countryCombo,
                genderLabel, genderGroup,
                signUpButton,
                messageLabel
            });
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            passwordTextBox.PasswordChar = passwordVisible ? '\0' : '*';
            passwordToggle.Text = passwordVisible ? "\U0001f648" : "\U0001f441";
        }

        private void ToggleConfirmPasswordVisibility(object sender, EventArgs e)
        {
            confirmPasswordVisible = !confirmPasswordVisible;
            confirmPasswordTextBox.PasswordChar = confirmPasswordVisible ? '\0' : '*';
            confirmPasswordToggle.Text = confirmPasswordVisible ? "\U0001f648" : "\U0001f441";
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            string username = userNameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string confirm = confirmPasswordTextBox.Text;
            string country = countryCombo.SelectedItem.ToString();
            string gender = maleRadio.Checked ? "Male" : "Female";

            if (string.IsNullOrEmpty(username))
            {
                messageLabel.Text = "Username is required";
                messageLabel.Tag = "Error";
                Theme.Apply(this);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                messageLabel.Text = "Password is required";
                messageLabel.Tag = "Error";
                Theme.Apply(this);
                return;
            }

            if (password.Length < 4)
            {
                messageLabel.Text = "Password must be at least 4 characters";
                messageLabel.Tag = "Error";
                Theme.Apply(this);
                return;
            }

            if (password != confirm)
            {
                messageLabel.Text = "Passwords do not match";
                messageLabel.Tag = "Error";
                Theme.Apply(this);
                return;
            }

            if (userManager.UsernameExists(username))
            {
                messageLabel.Text = "Username already exists";
                messageLabel.Tag = "Error";
                Theme.Apply(this);
                return;
            }

            if (userManager.SignUp(username, password, country, gender))
            {
                messageLabel.Tag = "Success";
                messageLabel.Text = "Account created successfully!";
                Theme.Apply(this);
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                confirmPasswordTextBox.Text = "";
                CurrentUser.Username = username;
                CurrentUser.Country = country;
                CurrentUser.Gender = gender;
                CurrentUser.Score = 0;
                SignUpSuccess?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
