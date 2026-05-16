using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;

namespace AdvancedProgramming.Forms
{
    public class AcceptedForm : UserControl
    {
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        public AcceptedForm()
        {
            Size = new Size(AppSizes.FormWidth, AppSizes.FormHeight);
            BackColor = AppColors.PageBack;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            int centerX = Width / 2;

            Toolbar toolbar = new Toolbar(this, "MiniCamp Puzzle");
            Controls.Add(toolbar);
            toolbar.CloseRequested += (s, e) => Application.Exit();

            Button buttonBack = new Button
            {
                Text = "\u2190 Back",
                Location = new Point(16, AppSizes.NavTop),
                Size = new Size(80, AppSizes.NavHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = AppColors.Text,
            };
            buttonBack.FlatAppearance.BorderSize = 0;
            buttonBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            Button buttonHome = new Button
            {
                Text = "Home",
                Location = new Point(104, AppSizes.NavTop),
                Size = new Size(80, AppSizes.NavHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = AppColors.Text,
            };
            buttonHome.FlatAppearance.BorderSize = 0;
            buttonHome.Click += (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty);

            Controls.Add(new Label
            {
                Text = "\u2705",
                Font = new Font("Segoe UI", 72),
                Size = new Size(120, 120),
                Location = new Point(centerX - 60, 140),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            });

            Controls.Add(new Label
            {
                Text = "All Tests Passed",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = AppColors.Text,
                Size = new Size(500, 50),
                Location = new Point(centerX - 250, 280),
                TextAlign = ContentAlignment.MiddleCenter,
            });

            Controls.Add(new Label
            {
                Text = "Your solution passed all test cases. Great work!",
                Font = new Font("Segoe UI", 13),
                ForeColor = AppColors.MutedText,
                Size = new Size(500, 30),
                Location = new Point(centerX - 250, 330),
                TextAlign = ContentAlignment.MiddleCenter,
            });

            Controls.Add(buttonBack);
            Controls.Add(buttonHome);
            //buttonBack.BringToFront();
            //buttonHome.BringToFront();
        }
    }
}
