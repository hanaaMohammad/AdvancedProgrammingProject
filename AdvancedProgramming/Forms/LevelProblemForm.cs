using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming.Forms
{
    public class LevelProblemForm : UserControl
    {
        public event EventHandler<string> ProblemSelected;
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private Button easyButton;
        private Button mediumButton;
        private Button hardButton;
        private ComboBox problemCombo;
        private Label comingSoonHint;
        private Toolbar toolbar;

        private static readonly string[] EasyProblems =
        {
            ProblemCatalog.StarterProblemTitle,
            "Program to find if a character is vowel or Consonant",
            "Print Fibonacci Series",
        };

        private static readonly string[] MediumProblems =
        {
            "Floor in a Sorted Array",
            "Move All Zeroes to End",
            "T-primes",
        };

        private static readonly string[] HardProblems =
        {
            "Happy Number",
            "Lucky Numbers in a Matrix",
            "Find the Single Number",
        };

        public LevelProblemForm()
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => Application.Exit();

            PageBackButton.AddTo(this,
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));

            int cx = this.Width / 2;
            int btnW = 220;

            var titleLabel = new Label
            {
                Text = "Problem Solving",
                Font = DesignTokens.Typography.DisplayMedium,
                AutoSize = false,
                Size = new Size(500, 55),
                Location = new Point(cx - 250, 100),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            var subtitleLabel = new Label
            {
                Text = "Choose a difficulty level",
                Font = DesignTokens.Typography.BodyLarge,
                AutoSize = false,
                Size = new Size(400, 26),
                Location = new Point(cx - 200, 155),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Secondary",
            };

            easyButton = new Button
            {
                Text = "Easy",
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.HeadingSmall,
                Size = new Size(btnW, 64),
                Location = new Point(cx - btnW / 2, 210),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            easyButton.FlatAppearance.BorderColor = Color.FromArgb(0, 200, 117);
            easyButton.Click += (s, e) => PopulateProblems(EasyProblems);

            mediumButton = new Button
            {
                Text = "Medium",
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.HeadingSmall,
                Size = new Size(btnW, 64),
                Location = new Point(cx - btnW / 2, 290),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            mediumButton.FlatAppearance.BorderColor = Color.FromArgb(255, 183, 64);
            mediumButton.Click += (s, e) => PopulateProblems(MediumProblems);

            hardButton = new Button
            {
                Text = "Hard",
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.HeadingSmall,
                Size = new Size(btnW, 64),
                Location = new Point(cx - btnW / 2, 370),
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            hardButton.FlatAppearance.BorderColor = Color.FromArgb(255, 82, 82);
            hardButton.Click += (s, e) => PopulateProblems(HardProblems);

            problemCombo = new ComboBox
            {
                Location = new Point(cx - 160, 450),
                Size = new Size(320, 30),
                Font = DesignTokens.Typography.BodyMedium,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false,
            };
            problemCombo.SelectedIndexChanged += ProblemCombo_SelectedIndexChanged;

            comingSoonHint = new Label
            {
                Text = "Only the starter problem is available to solve right now.",
                Font = DesignTokens.Typography.BodySmall,
                AutoSize = false,
                Size = new Size(400, 40),
                Location = new Point(cx - 200, 490),
                TextAlign = ContentAlignment.TopCenter,
                Tag = "Secondary",
                Visible = false,
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(subtitleLabel);
            this.Controls.Add(easyButton);
            this.Controls.Add(mediumButton);
            this.Controls.Add(hardButton);
            this.Controls.Add(problemCombo);
            this.Controls.Add(comingSoonHint);

            FormAccessibility.SetShortcutHint(easyButton, "1", "Easy problems");
            FormAccessibility.SetShortcutHint(mediumButton, "2", "Medium problems");
            FormAccessibility.SetShortcutHint(hardButton, "3", "Hard problems");
            FormAccessibility.SetShortcutHint(problemCombo, "Down", "Select a problem");

            Theme.StylePage(this);
            this.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (easyButton == null) return;
            int cx = this.Width / 2;
            int btnW = 220;
            easyButton.Location = new Point(cx - btnW / 2, 210);
            mediumButton.Location = new Point(cx - btnW / 2, 290);
            hardButton.Location = new Point(cx - btnW / 2, 370);
            problemCombo.Location = new Point(cx - 160, 450);
            comingSoonHint.Location = new Point(cx - 200, 490);
        }

        private void PopulateProblems(string[] problems)
        {
            problemCombo.Items.Clear();
            foreach (string title in problems)
                problemCombo.Items.Add(ProblemCatalog.GetListLabel(title));

            problemCombo.Visible = true;
            comingSoonHint.Visible = true;
            problemCombo.SelectedIndex = -1;
        }

        private void ProblemCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (problemCombo.SelectedItem == null)
                return;

            string title = ProblemCatalog.ParseListLabel(problemCombo.SelectedItem.ToString());

            if (!ProblemCatalog.IsAvailable(title))
            {
                MessageBox.Show(
                    "This problem is coming soon. Try the starter problem: " +
                    ProblemCatalog.StarterProblemTitle + ".",
                    "Coming Soon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                problemCombo.SelectedIndex = -1;
                return;
            }

            ProblemSelected?.Invoke(this, title);
        }
    }
}
