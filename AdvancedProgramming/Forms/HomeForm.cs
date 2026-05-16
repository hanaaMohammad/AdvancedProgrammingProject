using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;

namespace AdvancedProgramming.Forms
{
    // Home screen (UserControl) — kept for the project; same layout as before.
    public class HomeForm : UserControl
    {
        public event EventHandler UserRequested;
        public event EventHandler ProblemsRequested;

        private Button buttonUser;
        private Button buttonProblems;
        private Toolbar toolbar;

        public HomeForm()
        {
            Size = new Size(AppSizes.FormWidth, AppSizes.FormHeight);
            BackColor = AppColors.PageBack;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => Application.Exit();

            int contentX = 48;
            int contentW = AppSizes.FormWidth - contentX * 2;

            Controls.Add(new Label
            {
                Text = "Welcome to MiniCamp Puzzle",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = AppColors.Text,
                Size = new Size(contentW, 60),
                Location = new Point(contentX, 88),
            });

            var accentLine = new Panel
            {
                Size = new Size(72, 4),
                Location = new Point(contentX, 152),
                BackColor = AppColors.Accent,
            };
            Controls.Add(accentLine);

            Controls.Add(new Label
            {
                Text = "Solve code challenges. Level up your skills.",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Size = new Size(contentW, 32),
                Location = new Point(contentX, 168),
            });

            Controls.Add(new Label
            {
                Text =
                    "MiniCamp Puzzle is a desktop practice environment for coding interviews " +
                    "and algorithm drills. Pick a problem, write your solution in C#, and see " +
                    "how you stack up—right from your workspace.",
                Font = new Font("Segoe UI", 13),
                ForeColor = AppColors.MutedText,
                Size = new Size(contentW, 56),
                Location = new Point(contentX, 212),
            });

            var featuresCard = new Panel
            {
                Location = new Point(contentX, 288),
                Size = new Size(contentW, 300),
                BackColor = AppColors.CardBottom,
            };
            featuresCard.Controls.Add(new Label
            {
                Text = "What you can do",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = AppColors.Text,
                Location = new Point(24, 24),
                AutoSize = true,
            });
            featuresCard.Controls.Add(new Label
            {
                Text =
                    "• Browse puzzles by difficulty — Easy, Medium, and Hard\n" +
                    "• Read full problem statements with samples and constraints\n" +
                    "• Code in the built-in editor and run tests against your solution\n" +
                    "• Celebrate accepted submissions and learn from failed cases\n" +
                    "• Open your profile to review progress and personal details",
                Font = new Font("Segoe UI", 12),
                ForeColor = AppColors.MutedText,
                Location = new Point(24, 68),
                Size = new Size(contentW - 48, 220),
            });
            Controls.Add(featuresCard);

            int navW = 520;
            int navX = (AppSizes.FormWidth - navW) / 2;

            Controls.Add(new Label
            {
                Text = "Choose where to go next",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.MutedText,
                Size = new Size(navW, 22),
                Location = new Point(navX, 612),
                TextAlign = ContentAlignment.MiddleCenter,
            });

            buttonUser = MakeNavButton("Profile", navX);
            buttonProblems = MakeNavButton("Problems", navX + navW / 2 + 8);
            buttonUser.Location = new Point(navX, 640);
            buttonProblems.Location = new Point(navX + navW / 2 + 8, 640);

            buttonUser.Click += (s, e) => UserRequested?.Invoke(this, EventArgs.Empty);
            buttonProblems.Click += (s, e) => ProblemsRequested?.Invoke(this, EventArgs.Empty);

            Controls.Add(buttonUser);
            Controls.Add(buttonProblems);
        }

        private Button MakeNavButton(string text, int x)
        {
            return new Button
            {
                Text = text,
                Size = new Size(240, 48),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = AppColors.Accent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
            };
        }
    }
}
