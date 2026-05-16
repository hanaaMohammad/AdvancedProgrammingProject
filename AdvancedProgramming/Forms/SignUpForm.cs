using AdvancedProgramming.Components;
using AdvancedProgramming.Forms;
using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class SignUpForm : AppForm
    {
        private const int CardWidth = 420;

        private Toolbar toolbar;
        private Button btnBack;
        private Button btnHome;
        private Panel formCard;
        private Panel fieldsScroll;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Label passwordToggle;
        private Label confirmPasswordToggle;
        private ComboBox countryCombo;
        private RadioButton maleRadio;
        private RadioButton femaleRadio;
        private Label messageLabel;
        private Panel signUpPill;
        private bool passwordVisible;
        private bool confirmPasswordVisible;
        private readonly UserManagement userManager = new UserManagement();

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var accent = Theme.Current.AccentColor;
            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "Sign Up");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => GoBack(),
                (s, e) => GoStartup());
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            formCard = CatalogUi.CreateCard(Color.FromArgb(50, accent), 20);
            BuildFormCard();
            Controls.Add(formCard);

            signUpPill = CatalogUi.CreateActionPill(
                "Sign Up \u2192",
                true,
                accent,
                (s, e) => SignUpButton_Click(s, e));
            Controls.Add(signUpPill);

            FormAccessibility.SetShortcutHint(btnBack, "Esc", "Go back");

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildFormCard()
        {
            formCard.Controls.Add(new Label
            {
                Text = "Create your account",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(28, 20),
                Size = new Size(CardWidth - 56, 30),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            formCard.Controls.Add(new Label
            {
                Text = "Join MiniCamp and start solving puzzles",
                Font = new Font("Segoe UI", 10),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(28, 52),
                Size = new Size(CardWidth - 56, 20),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            fieldsScroll = new Panel
            {
                Location = new Point(28, 82),
                AutoScroll = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            int y = 0;
            int w = CardWidth - 56;

            CatalogUi.AddFormField(fieldsScroll, ref y, "Username", w, out userNameTextBox);
            CatalogUi.AddPasswordField(fieldsScroll, ref y, "Password", w, out passwordTextBox, out passwordToggle);
            passwordToggle.Click += (s, e) =>
                CatalogUi.TogglePasswordVisibility(passwordTextBox, passwordToggle, ref passwordVisible);
            CatalogUi.AddPasswordField(fieldsScroll, ref y, "Confirm Password", w, out confirmPasswordTextBox, out confirmPasswordToggle);
            confirmPasswordToggle.Click += (s, e) =>
                CatalogUi.TogglePasswordVisibility(confirmPasswordTextBox, confirmPasswordToggle, ref confirmPasswordVisible);

            y = AddComboField(fieldsScroll, y, "Country", w, out countryCombo);
            countryCombo.Items.AddRange(new object[] { "Palestine", "Jordan", "Lebanon", "Egypt", "US", "UK" });
            countryCombo.SelectedIndex = 0;

            y = AddGenderField(fieldsScroll, y, w, out maleRadio, out femaleRadio);

            messageLabel = new Label
            {
                Location = new Point(0, y),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9),
                ForeColor = Theme.Current.ErrorColor,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            fieldsScroll.Controls.Add(messageLabel);

            formCard.Controls.Add(fieldsScroll);
        }

        private static int AddComboField(Panel parent, int y, string label, int width, out ComboBox combo)
        {
            int blockH = 22 + 8 + 36;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(block);
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                CatalogUi.PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(width - 20, 28),
                Location = new Point(10, 30),
                BackColor = CatalogUi.InsetBack,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Tag = "NoTheme",
            };
            block.Controls.Add(combo);
            parent.Controls.Add(block);
            return y + blockH + 14;
        }

        private static int AddGenderField(Panel parent, int y, int width, out RadioButton male, out RadioButton female)
        {
            int blockH = 22 + 8 + 40;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(block);
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                CatalogUi.PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = "Gender",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            var host = new Panel
            {
                Location = new Point(10, 30),
                Size = new Size(width - 20, 32),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            male = new RadioButton
            {
                Text = "Male",
                Location = new Point(8, 6),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Checked = true,
                Tag = "NoTheme",
            };
            female = new RadioButton
            {
                Text = "Female",
                Location = new Point(width / 2, 6),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            host.Controls.Add(male);
            host.Controls.Add(female);
            block.Controls.Add(host);
            parent.Controls.Add(block);

            return y + blockH + 14;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            messageLabel.ForeColor = Theme.Current.ErrorColor;

            string username = userNameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string confirm = confirmPasswordTextBox.Text;
            string country = countryCombo.SelectedItem.ToString();
            string gender = maleRadio.Checked ? "Male" : "Female";

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

            if (userManager.SignUp(username, password, country, gender))
            {
                messageLabel.ForeColor = Theme.Current.SuccessColor;
                messageLabel.Text = "Account created successfully!";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                confirmPasswordTextBox.Text = "";
                CurrentUser.Username = username;
                CurrentUser.Country = country;
                CurrentUser.Gender = gender;
                CurrentUser.Score = 0;
                AfterLogin();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (formCard == null)
                return;

            int cx = Width / 2;
            int cardY = CatalogUi.ContentTop;
            int cardH = Math.Max(420, Height - cardY - 80);
            formCard.SetBounds(cx - CardWidth / 2, cardY, CardWidth, cardH);

            fieldsScroll.SetBounds(28, 82, CardWidth - 56, cardH - 100);
            signUpPill.Location = new Point(cx - signUpPill.Width / 2, formCard.Bottom + 16);
        }
    }
}
