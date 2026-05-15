using System;
using System.Drawing;
using System.Windows.Forms;


namespace AdvancedProgramming.Forms
{
    public class AcceptedForm : UserControl
    {
        public event EventHandler HomeRequested;
        public event EventHandler BackRequested;

        private Button btnHome;
        private Button btnBack;
        private Toolbar toolbar;

        public AcceptedForm()
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            InitializeComponent();
            toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            int cx = this.Width / 2;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            var iconLabel = new Label
            {
                Text = "\u2705",
                Font = new Font("Segoe UI", 72F, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(120, 120),
                Location = new Point(cx - 60, 140),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            var titleLabel = new Label
            {
                Text = "All Tests Passed",
                Font = DesignTokens.Typography.DisplaySmall,
                AutoSize = false,
                Size = new Size(500, 50),
                Location = new Point(cx - 250, 280),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            var descLabel = new Label
            {
                Text = "Your solution passed all test cases. Great work!",
                Font = DesignTokens.Typography.BodyLarge,
                AutoSize = false,
                Size = new Size(500, 30),
                Location = new Point(cx - 250, 330),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "Secondary",
            };

            btnHome = new Button
            {
                Text = "Home",
                Font = DesignTokens.Typography.ButtonLabel,
                Size = new Size(DesignTokens.Sizing.ButtonWidthMd, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx + 10, 420),
                FlatStyle = FlatStyle.Flat,
                Tag = "Primary",
                Cursor = Cursors.Hand,
            };
            btnHome.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);

            btnBack = new Button
            {
                Text = "Back",
                Font = DesignTokens.Typography.BodyMedium,
                Size = new Size(DesignTokens.Sizing.ButtonWidthMd, DesignTokens.Sizing.ButtonHeight),
                Location = new Point(cx - 190, 420),
                FlatStyle = FlatStyle.Flat,
                Tag = "Secondary",
                Cursor = Cursors.Hand,
            };
            btnBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            this.Controls.Add(iconLabel);
            this.Controls.Add(titleLabel);
            this.Controls.Add(descLabel);
            this.Controls.Add(btnHome);
            this.Controls.Add(btnBack);

            Theme.StylePage(this);
        }
    }
}
