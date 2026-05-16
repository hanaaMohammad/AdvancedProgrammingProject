using AdvancedProgramming;
using AdvancedProgramming.Components;
using AdvancedProgramming.Session;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class SignUpForm : AppForm
    {
        private const int CardWidth = 420;

        private Toolbar toolbar;
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
        private UserManagement userManager = new UserManagement();

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Color accent = AppColors.Accent;
            int centerX = AppSizes.FormWidth / 2;

            toolbar = new Toolbar(this, "Sign Up");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button btnBack = MakeNavButton("\u2190 Back", 16, BackButton_Click);
            Button btnHome = MakeNavButton("Home", 104, HomeButton_Click);
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            formCard = AppUi.CreateCard(Color.FromArgb(50, accent), 20);
            Controls.Add(formCard);

            signUpPill = AppUi.CreateActionPill("Sign Up \u2192", true, accent, SignUpButton_Click);
            Controls.Add(signUpPill);

            BuildFormCard();

            int cardY = AppSizes.ContentTop;
            int cardH = 620;
            formCard.SetBounds(centerX - CardWidth / 2, cardY, CardWidth, cardH);
            fieldsScroll.SetBounds(28, 82, CardWidth - 56, cardH - 100);
            signUpPill.Location = new Point(centerX - signUpPill.Width / 2, cardY + cardH + 16);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowAsMainForm(new StartupForm());
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
            });

            formCard.Controls.Add(new Label
            {
                Text = "Join MiniCamp and start solving puzzles",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.MutedText,
                Location = new Point(28, 52),
                Size = new Size(CardWidth - 56, 20),
                BackColor = Color.Transparent,
            });

            fieldsScroll = new Panel
            {
                Location = new Point(28, 82),
                AutoScroll = true,
                BackColor = Color.Transparent,
            };

            int y = 0;
            int w = CardWidth - 56;

            AppUi.AddFormField(fieldsScroll, ref y, "Username", w, out userNameTextBox, 36);
            AppUi.AddPasswordField(fieldsScroll, ref y, "Password", w, out passwordTextBox, out passwordToggle, 36);
            passwordToggle.Click += PasswordToggle_Click;
            AppUi.AddPasswordField(fieldsScroll, ref y, "Confirm Password", w, out confirmPasswordTextBox, out confirmPasswordToggle, 36);
            confirmPasswordToggle.Click += ConfirmPasswordToggle_Click;

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
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
            };
            fieldsScroll.Controls.Add(messageLabel);

            formCard.Controls.Add(fieldsScroll);
        }

        private void PasswordToggle_Click(object sender, EventArgs e)
        {
            AppUi.TogglePasswordVisibility(passwordTextBox, passwordToggle, ref passwordVisible);
        }

        private void ConfirmPasswordToggle_Click(object sender, EventArgs e)
        {
            AppUi.TogglePasswordVisibility(confirmPasswordTextBox, confirmPasswordToggle, ref confirmPasswordVisible);
        }

        private int AddComboField(Panel parent, int y, string label, int width, out ComboBox combo)
        {
            int blockH = 22 + 8 + 36;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                AppUi.PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
            });
            combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(width - 20, 28),
                Location = new Point(10, 30),
                BackColor = AppColors.InsetBack,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
            };
            block.Controls.Add(combo);
            parent.Controls.Add(block);
            return y + blockH + 14;
        }

        private int AddGenderField(Panel parent, int y, int width, out RadioButton male, out RadioButton female)
        {
            int blockH = 22 + 8 + 40;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                AppUi.PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = "Gender",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
            });

            var host = new Panel
            {
                Location = new Point(10, 30),
                Size = new Size(width - 20, 32),
                BackColor = Color.Transparent,
            };

            male = new RadioButton
            {
                Text = "Male",
                Location = new Point(8, 6),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Checked = true,
            };
            female = new RadioButton
            {
                Text = "Female",
                Location = new Point(width / 2, 6),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
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
            messageLabel.ForeColor = AppColors.Error;

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
                messageLabel.ForeColor = AppColors.Success;
                messageLabel.Text = "Account created successfully!";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                confirmPasswordTextBox.Text = "";
                CurrentUser.Username = username;
                CurrentUser.Country = country;
                CurrentUser.Gender = gender;
                CurrentUser.Score = 0;
                ShowAsMainForm(new LevelProblemForm());
            }
        }
    }
}
