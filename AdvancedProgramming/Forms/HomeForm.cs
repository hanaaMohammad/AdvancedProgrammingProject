using System;
using System.Drawing;
using System.Windows.Forms;


namespace AdvancedProgramming.Forms
{
    public class HomeForm : UserControl
    {
        public event EventHandler UserRequested;
        public event EventHandler ProblemsRequested;

        private Button btnHome;
        private Button btnUser;
        private Button btnProblems;
        private Button btnTutorial;
        private TableLayoutPanel navGrid;
        private Toolbar toolbar;

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

            navGrid = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 1,
                Location = new Point(DesignTokens.Spacing.Xxl, 220),
                Size = new Size(1036, 60),
            };
            navGrid.ColumnStyles.Clear();
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            navGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            btnHome = CreateNavButton("Home", true);
            btnUser = CreateNavButton("Profile", false);
            btnTutorial = CreateNavButton("Tutorial", false);
            btnProblems = CreateNavButton("Problems", false);

            navGrid.Controls.Add(btnHome, 0, 0);
            navGrid.Controls.Add(btnUser, 1, 0);
            navGrid.Controls.Add(btnTutorial, 2, 0);
            navGrid.Controls.Add(btnProblems, 3, 0);

            btnUser.Click += (s, e) => UserRequested?.Invoke(this, EventArgs.Empty);
            btnProblems.Click += (s, e) => ProblemsRequested?.Invoke(this, EventArgs.Empty);

            var welcomeLabel = new Label
            {
                Text = "Welcome to MiniCamp Puzzle",
                Font = DesignTokens.Typography.DisplayMedium,
                AutoSize = false,
                Size = new Size(700, 60),
                Location = new Point(DesignTokens.Spacing.Xxl, 120),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var subtitleLabel = new Label
            {
                Text = "Choose a section to get started",
                Font = DesignTokens.Typography.BodyLarge,
                AutoSize = false,
                Size = new Size(400, 28),
                Location = new Point(DesignTokens.Spacing.Xxl, 180),
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "Secondary",
            };

            this.Controls.Add(welcomeLabel);
            this.Controls.Add(subtitleLabel);
            this.Controls.Add(navGrid);

            Theme.StylePage(this);
        }

        private Button CreateNavButton(string text, bool isActive)
        {
            return new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = isActive
                    ? new Font("Segoe UI", 12F, FontStyle.Bold)
                    : DesignTokens.Typography.BodyLarge,
                Size = new Size(235, 50),
                Tag = isActive ? "Primary" : "Ghost",
                Cursor = Cursors.Hand,
            };
        }
    }
}
