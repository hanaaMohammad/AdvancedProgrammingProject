using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Components;


namespace AdvancedProgramming.Forms
{
    public class AcceptedForm : UserControl
    {
        public event EventHandler BackRequested;

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

            var btnBack = PageBackButton.Create((s, e) => BackRequested?.Invoke(this, EventArgs.Empty));

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

            this.Controls.Add(iconLabel);
            this.Controls.Add(titleLabel);
            this.Controls.Add(descLabel);
            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            Theme.StylePage(this);
        }
    }
}
