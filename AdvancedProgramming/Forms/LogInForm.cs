using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;
using AdvancedProgramming.Components;
using AdvancedProgramming.Session;

namespace AdvancedProgramming.Forms
{
    public class LogInForm : AppForm
    {
        private const int CardWidth = 400;

        private Toolbar toolbar;
        private Panel authCard;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Label passwordToggle;
        private Label messageLabel;
        private Panel loginPill;
        private bool passwordVisible;

        public LogInForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Color accent = AppColors.Accent;
            int centerX = AppSizes.FormWidth / 2;

            toolbar = new Toolbar(this, "Log In");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button btnBack = MakeNavButton("\u2190 Back", 16, BackButton_Click);
            Button btnHome = MakeNavButton("Home", 104, HomeButton_Click);
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            authCard = AppUi.CreateCard(Color.FromArgb(50, accent), 20);
            authCard.SetBounds(centerX - CardWidth / 2, 195, CardWidth, 340);
            BuildAuthCard();
            Controls.Add(authCard);

            loginPill = AppUi.CreateActionPill("Log In \u2192", true, accent, LogInButton_Click);
            loginPill.Location = new Point(centerX - loginPill.Width / 2, 555);
            Controls.Add(loginPill);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowAsMainForm(new StartupForm());
        }

        private void BuildAuthCard()
        {
            authCard.Controls.Add(new Label
            {
                Text = "Welcome back",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(28, 24),
                Size = new Size(CardWidth - 56, 32),
                BackColor = Color.Transparent,
            });

            authCard.Controls.Add(new Label
            {
                Text = "Sign in to continue solving challenges",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.MutedText,
                Location = new Point(28, 58),
                Size = new Size(CardWidth - 56, 22),
                BackColor = Color.Transparent,
            });

            var fields = new Panel
            {
                Location = new Point(28, 96),
                Size = new Size(CardWidth - 56, 200),
                BackColor = Color.Transparent,
            };

            int y = 0;
            int w = CardWidth - 56;
            AppUi.AddFormField(fields, ref y, "Username", w, out usernameTextBox, 36);
            AppUi.AddPasswordField(fields, ref y, "Password", w, out passwordTextBox, out passwordToggle, 36);
            passwordToggle.Click += PasswordToggle_Click;

            messageLabel = new Label
            {
                Text = "",
                Location = new Point(0, y),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
            };
            fields.Controls.Add(messageLabel);
            authCard.Controls.Add(fields);
        }

        private void PasswordToggle_Click(object sender, EventArgs e)
        {
            AppUi.TogglePasswordVisibility(passwordTextBox, passwordToggle, ref passwordVisible);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            var user = new UserManagement();

            messageLabel.Text = "";
            messageLabel.ForeColor = AppColors.Error;

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

            if (user.SignIn(username, password))
            {
                CurrentUser.Username = username;
                var details = user.GetUserDetails(username);
                CurrentUser.Country = details.Country;
                CurrentUser.Gender = details.Gender;
                CurrentUser.Score = user.GetScore(username);
                ShowAsMainForm(new LevelProblemForm());
            }
            else
            {
                messageLabel.Text = "Invalid username or password";
            }
        }
    }
}
