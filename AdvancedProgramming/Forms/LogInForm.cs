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

        private const int CardWidth = 400;

        private Toolbar toolbar;
        private Button btnBack;
        private Button btnHome;
        private Panel authCard;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Label passwordToggle;
        private Label messageLabel;
        private Panel loginPill;
        private bool passwordVisible;

        public LogInForm()
        {
            Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            CatalogUi.EnableDoubleBuffer(this);
            DoubleBuffered = true;
            InitializeComponent();
        }

        public void SubmitLogin()
        {
            LogInButton_Click(null, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            var accent = Theme.Current.AccentColor;
            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "Log In");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            authCard = CatalogUi.CreateCard(Color.FromArgb(50, accent), 20);
            BuildAuthCard(accent);
            Controls.Add(authCard);

            loginPill = CatalogUi.CreateActionPill(
                "Log In \u2192",
                true,
                accent,
                (s, e) => LogInButton_Click(s, e));
            Controls.Add(loginPill);

            FormAccessibility.SetShortcutHint(loginPill, "Enter", "Sign in");
            FormAccessibility.SetShortcutHint(usernameTextBox, "Tab", "Username");
            FormAccessibility.SetShortcutHint(btnBack, "Esc", "Go back");

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildAuthCard(Color accent)
        {
            authCard.Controls.Add(new Label
            {
                Text = "Welcome back",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(28, 24),
                Size = new Size(CardWidth - 56, 32),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            authCard.Controls.Add(new Label
            {
                Text = "Sign in to continue solving challenges",
                Font = new Font("Segoe UI", 10),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(28, 58),
                Size = new Size(CardWidth - 56, 22),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            var fields = new Panel
            {
                Location = new Point(28, 96),
                Size = new Size(CardWidth - 56, 200),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            int y = 0;
            int w = CardWidth - 56;
            CatalogUi.AddFormField(fields, ref y, "Username", w, out usernameTextBox);
            CatalogUi.AddPasswordField(fields, ref y, "Password", w, out passwordTextBox, out passwordToggle);
            passwordToggle.Click += (s, e) =>
                CatalogUi.TogglePasswordVisibility(passwordTextBox, passwordToggle, ref passwordVisible);

            messageLabel = new Label
            {
                Text = "",
                Location = new Point(0, y),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9),
                ForeColor = Theme.Current.ErrorColor,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            fields.Controls.Add(messageLabel);

            authCard.Controls.Add(fields);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            var user = new UserManagement();

            messageLabel.Text = "";
            messageLabel.ForeColor = Theme.Current.ErrorColor;

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
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (authCard == null)
                return;

            int cx = Width / 2;
            int cardH = 340;
            int cardY = Math.Max(100, (Height - cardH - 70) / 2);

            authCard.SetBounds(cx - CardWidth / 2, cardY, CardWidth, cardH);
            loginPill.Location = new Point(cx - loginPill.Width / 2, authCard.Bottom + 20);
        }
    }
}
