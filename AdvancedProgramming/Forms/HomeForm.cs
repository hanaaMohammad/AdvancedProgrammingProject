using System;
using System.Drawing;
using System.Windows.Forms;


namespace AdvancedProgramming.Forms
{
    public class HomeForm : UserControl
    {
        public event EventHandler UserRequested;
        public event EventHandler ProblemsRequested;

        private Button btnUser;
        private Button btnProblems;
        private TableLayoutPanel navGrid;
        private Toolbar toolbar;
        private Label welcomeLabel;
        private Label taglineLabel;
        private Label introLabel;
        private Panel accentLine;
        private Panel featuresCard;
        private Label navHint;

        public HomeForm()
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => Application.Exit();

            int contentX = DesignTokens.Spacing.Xxl;
            int contentW = DesignTokens.FormWidth - contentX * 2;

            welcomeLabel = new Label
            {
                Text = "Welcome to MiniCamp Puzzle",
                Font = DesignTokens.Typography.DisplayMedium,
                AutoSize = false,
                Size = new Size(contentW, 60),
                Location = new Point(contentX, 88),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            accentLine = new Panel
            {
                Size = new Size(72, 4),
                Location = new Point(contentX, 152),
                BackColor = Theme.Current.AccentColor,
            };

            taglineLabel = new Label
            {
                Text = "Solve code challenges. Level up your skills.",
                Font = DesignTokens.Typography.HeadingSmall,
                AutoSize = false,
                Size = new Size(contentW, 32),
                Location = new Point(contentX, 168),
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "Secondary",
            };

            introLabel = new Label
            {
                Text =
                    "MiniCamp Puzzle is a desktop practice environment for coding interviews " +
                    "and algorithm drills. Pick a problem, write your solution in C#, and see " +
                    "how you stack up—right from your workspace.",
                Font = DesignTokens.Typography.BodyLarge,
                AutoSize = false,
                Size = new Size(contentW, 56),
                Location = new Point(contentX, 212),
                TextAlign = ContentAlignment.TopLeft,
                Tag = "Secondary",
            };

            featuresCard = new Panel
            {
                Location = new Point(contentX, 288),
                Size = new Size(contentW, 300),
                Tag = "Card",
            };

            var featuresTitle = new Label
            {
                Text = "What you can do",
                Font = DesignTokens.Typography.HeadingMedium,
                AutoSize = false,
                Size = new Size(contentW - DesignTokens.Spacing.Lg * 2, 36),
                Location = new Point(DesignTokens.Spacing.Lg, DesignTokens.Spacing.Lg),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var featuresBody = new Label
            {
                Text =
                    "• Browse puzzles by difficulty — Easy, Medium, and Hard\n" +
                    "• Read full problem statements with samples and constraints\n" +
                    "• Code in the built-in editor and run tests against your solution\n" +
                    "• Celebrate accepted submissions and learn from failed cases\n" +
                    "• Open your profile to review progress and personal details",
                Font = DesignTokens.Typography.BodyMedium,
                AutoSize = false,
                Size = new Size(contentW - DesignTokens.Spacing.Lg * 2, 220),
                Location = new Point(DesignTokens.Spacing.Lg, 68),
                TextAlign = ContentAlignment.TopLeft,
                Tag = "Secondary",
            };

            featuresCard.Controls.Add(featuresTitle);
            featuresCard.Controls.Add(featuresBody);

            int navW = 520;
            int navX = (DesignTokens.FormWidth - navW) / 2;
            navGrid = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Location = new Point(navX, 640),
                Size = new Size(navW, DesignTokens.Sizing.ButtonHeight + DesignTokens.Spacing.Sm),
            };
            navGrid.ColumnStyles.Clear();
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            btnUser = CreateNavButton("Profile");
            btnProblems = CreateNavButton("Problems");

            navGrid.Controls.Add(btnUser, 0, 0);
            navGrid.Controls.Add(btnProblems, 1, 0);

            btnUser.Click += (s, e) => UserRequested?.Invoke(this, EventArgs.Empty);
            btnProblems.Click += (s, e) => ProblemsRequested?.Invoke(this, EventArgs.Empty);

            navHint = new Label
            {
                Text = "Choose where to go next",
                Font = DesignTokens.Typography.BodySmall,
                AutoSize = false,
                Size = new Size(navW, 22),
                Location = new Point(navX, 612),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Secondary",
            };

            this.Controls.Add(welcomeLabel);
            this.Controls.Add(accentLine);
            this.Controls.Add(taglineLabel);
            this.Controls.Add(introLabel);
            this.Controls.Add(featuresCard);
            this.Controls.Add(navHint);
            this.Controls.Add(navGrid);

            Theme.StylePage(this);
            accentLine.BackColor = Theme.Current.AccentColor;
        }

        private static Button CreateNavButton(string text)
        {
            return new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.ButtonLabel,
                Dock = DockStyle.Fill,
                Margin = new Padding(DesignTokens.Spacing.Sm, 0, DesignTokens.Spacing.Sm, 0),
                Tag = "Primary",
                Cursor = Cursors.Hand,
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (welcomeLabel == null)
                return;

            int contentX = DesignTokens.Spacing.Xxl;
            int contentW = Math.Max(320, this.Width - contentX * 2);
            int navW = Math.Min(520, contentW);

            welcomeLabel.Size = new Size(contentW, 60);
            taglineLabel.Size = new Size(contentW, 32);
            introLabel.Size = new Size(contentW, 56);
            featuresCard.Size = new Size(contentW, featuresCard.Height);
            featuresCard.Location = new Point(contentX, featuresCard.Top);

            int navX = (this.Width - navW) / 2;
            navGrid.Size = new Size(navW, navGrid.Height);
            navGrid.Location = new Point(navX, navGrid.Top);

            if (navHint != null)
            {
                navHint.Size = new Size(navW, 22);
                navHint.Location = new Point(navX, navHint.Top);
            }
        }
    }
}
