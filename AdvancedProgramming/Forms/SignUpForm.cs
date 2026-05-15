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

        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Button signUpButton;
        private Button passwordButtonToggle;
        private Button confirmPasswordButtonToggle;
        private Label messageLabel;
        private bool passwordVisible = false;
        private bool confirmPasswordVisible = false;
        private UserManagement userManager;
        private Toolbar toolbar;
        private ComboBox CountryCombo;
        private GroupBox GroupGender;
        private RadioButton MaleRadio;
        private RadioButton FmaleRadio;

        public SignUpForm()
        {
            this.SuspendLayout();
            this.Size = new Size(1100, 800);
            InitializeSignUpComponents();
            toolbar = new Toolbar(this, "Sign Up");
            this.Controls.Add(toolbar);
            this.ResumeLayout(false);
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            Theme.Apply(this);
            userManager = new UserManagement();
            DatabaseManager.InitializeDatabase();
        }

        private void InitializeSignUpComponents()
        {
            int cx = this.Width / 2;
            int labelX = cx - 200;
            int inputX = cx - 100;
            int inputW = 270;
            int toggleX = inputX + inputW - 35;
            int rowY = 100;
            int rowGap = 48;

            var usernameLabel = new Label { Text = "Username", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            userNameTextBox = new TextBox { Location = new Point(inputX, rowY), Size = new Size(inputW, 24) };

            rowY += rowGap;
            var passwordLabel = new Label { Text = "Password", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            passwordTextBox = new TextBox { Location = new Point(inputX, rowY), Size = new Size(inputW - 35, 24), PasswordChar = '*' };
            passwordButtonToggle = new Button { Text = "\U0001f441", Location = new Point(toggleX, rowY), Size = new Size(30, 24), FlatStyle = FlatStyle.Flat };
            passwordButtonToggle.FlatAppearance.BorderSize = 0;
            passwordButtonToggle.Click += TogglePasswordVisibility;

            rowY += rowGap;
            var confirmLabel = new Label { Text = "Confirm", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            confirmPasswordTextBox = new TextBox { Location = new Point(inputX, rowY), Size = new Size(inputW - 35, 24), PasswordChar = '*' };
            confirmPasswordButtonToggle = new Button { Text = "\U0001f441", Location = new Point(toggleX, rowY), Size = new Size(30, 24), FlatStyle = FlatStyle.Flat };
            confirmPasswordButtonToggle.FlatAppearance.BorderSize = 0;
            confirmPasswordButtonToggle.Click += confirmPasswordVisibilityTaggel;

            rowY += rowGap;
            var countryLabel = new Label { Text = "Country", Location = new Point(labelX, rowY + 4), Size = new Size(90, 20) };
            CountryCombo = new ComboBox { Location = new Point(inputX, rowY), Size = new Size(170, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            CountryCombo.Items.AddRange(new object[] { "Palestine", "Jordan", "Lebanon", "Egypt", "US", "UK" });
            CountryCombo.SelectedIndex = 0;

            rowY += rowGap;
            var genderLabel = new Label { Text = "Gender", Location = new Point(labelX, rowY + 3), Size = new Size(90, 20) };
            GroupGender = new GroupBox { Location = new Point(inputX, rowY), Size = new Size(200, 44) };
            MaleRadio = new RadioButton { Text = "Male", Location = new Point(12, 13), Size = new Size(75, 20) };
            FmaleRadio = new RadioButton { Text = "Female", Location = new Point(100, 13), Size = new Size(85, 20) };
            GroupGender.Controls.Add(MaleRadio);
            GroupGender.Controls.Add(FmaleRadio);
            MaleRadio.Checked = true;

            rowY += rowGap + 30;
            signUpButton = new Button { Text = "Sign Up", Location = new Point(cx - 90, rowY), Size = new Size(180, 50), FlatStyle = FlatStyle.Flat };
            signUpButton.FlatAppearance.BorderSize = 0;
            signUpButton.Click += signUpButton_Click;

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
            messageLabel = new Label { Location = new Point(labelX, rowY), Size = new Size(400, 50), TextAlign = ContentAlignment.MiddleCenter };

            this.Controls.AddRange(new Control[] {
                usernameLabel, userNameTextBox,
                passwordLabel, passwordTextBox, passwordButtonToggle,
                confirmLabel, confirmPasswordTextBox, confirmPasswordButtonToggle,
                countryLabel, CountryCombo,
                genderLabel, GroupGender,
                signUpButton, btnBack,
                messageLabel
            });
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            passwordTextBox.PasswordChar = passwordVisible ? '\0' : '*';
            passwordButtonToggle.Text = passwordVisible ? "\U0001f648" : "\U0001f441";
        }

        private void confirmPasswordVisibilityTaggel(object sender, EventArgs e)
        {
            confirmPasswordVisible = !confirmPasswordVisible;
            confirmPasswordTextBox.PasswordChar = confirmPasswordVisible ? '\0' : '*';
            confirmPasswordButtonToggle.Text = confirmPasswordVisible ? "\U0001f648" : "\U0001f441";
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            string username = userNameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string confirm = confirmPasswordTextBox.Text;
            string country = CountryCombo.SelectedItem.ToString();
            string gender = MaleRadio.Checked ? "Male" : "Female";

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

            if (userManager.SignUp(username, password, CountryCombo.SelectedItem.ToString(), MaleRadio.Checked ? "Male" : "Female"))
            {
                messageLabel.ForeColor = Color.Green;
                messageLabel.Text = "Sign up successful!";
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
