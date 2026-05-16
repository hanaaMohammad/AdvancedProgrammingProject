using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AdvancedProgramming;
using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming.Forms
{
    public class LevelProblemForm : AppForm
    {
        private Toolbar toolbar;
        private FlowLayoutPanel cardPanel;
        private TextBox searchBox;
        private List<Problem> allProblems = new List<Problem>();

        public LevelProblemForm()
        {
            InitializeComponent();
            LoadProblems();
        }

        private void InitializeComponent()
        {
            int centerX = AppSizes.FormWidth / 2;
            int pageTitleY = AppSizes.ContentTop;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button profileBtn = MakeNavButton("Profile", 16, ProfileButton_Click);
            Controls.Add(profileBtn);
            profileBtn.BringToFront();

            var titleLabel = new Label
            {
                Text = "Problem Solving",
                Font = new Font("Segoe UI", 30, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(600, 60),
                Location = new Point(centerX - 300, pageTitleY),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            var subtitleLabel = new Label
            {
                Text = "Sharpen your logic with coding challenges",
                Font = new Font("Segoe UI", 12),
                ForeColor = AppColors.MutedText,
                Size = new Size(500, 30),
                Location = new Point(centerX - 250, pageTitleY + 60),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            searchBox = new TextBox
            {
                Size = new Size(420, 42),
                Location = new Point(centerX - 210, pageTitleY + 110),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(20, 28, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };
            searchBox.TextChanged += SearchBox_TextChanged;

            cardPanel = new FlowLayoutPanel
            {
                Location = new Point(40, pageTitleY + 180),
                Size = new Size(AppSizes.FormWidth - 80, AppSizes.FormHeight - pageTitleY - 200),
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(10),
            };

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(searchBox);
            Controls.Add(cardPanel);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            ShowProblems(searchBox.Text);
        }

        private void LoadProblems()
        {
            try
            {
                allProblems = ProblemLoadReadJs.GetAll();
            }
            catch
            {
                return;
            }
            ShowProblems("");
        }

        private void ShowProblems(string query)
        {
            cardPanel.Controls.Clear();

            List<Problem> list = allProblems;
            if (!string.IsNullOrWhiteSpace(query))
            {
                string q = query.ToLower();
                list = allProblems
                    .Where(p =>
                        (p.title ?? "").ToLower().Contains(q) ||
                        (p.description ?? "").ToLower().Contains(q))
                    .ToList();
            }

            foreach (Problem problem in list)
                cardPanel.Controls.Add(CreateProblemCard(problem));
        }

        private Panel CreateProblemCard(Problem problem)
        {
            bool isAvailable = ProblemCatalog.IsAvailable(problem);
            Color difficultyColor = Theme.GetLevelColor(problem.level);

            Panel card = new Panel
            {
                Width = 340,
                Height = 220,
                Margin = new Padding(16),
                BackColor = Color.FromArgb(17, 24, 39),
                Cursor = isAvailable ? Cursors.Hand : Cursors.Default,
            };

            card.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (GraphicsPath path = AppUi.RoundedRect(rect, 20))
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect, AppColors.CardTop, AppColors.CardBottom, 90f))
                {
                    g.FillPath(brush, path);
                    Color borderColor = isAvailable
                        ? Color.FromArgb(50, difficultyColor)
                        : AppColors.DefaultBorder;
                    using (Pen pen = new Pen(borderColor, 2))
                        g.DrawPath(pen, path);
                }
            };

            Label title = new Label
            {
                Text = problem.title,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = isAvailable ? Color.White : Color.FromArgb(140, 150, 165),
                Location = new Point(20, 20),
                Size = new Size(280, 35),
                BackColor = Color.Transparent,
            };

            Label description = new Label
            {
                Text = TruncateText(problem.description ?? "", 120),
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.MutedText,
                Location = new Point(20, 65),
                Size = new Size(290, 60),
                BackColor = Color.Transparent,
            };

            Panel badge = AppUi.CreateBadge(problem.level);
            badge.Location = new Point(20, 165);

            Label typeLabel = new Label
            {
                Text = problem.type,
                Font = new Font("Segoe UI", 8),
                ForeColor = AppColors.MutedText,
                AutoSize = true,
                Location = new Point(120, 172),
                BackColor = Color.Transparent,
            };

            string actionText = isAvailable ? "Solve →" : "Coming Soon";
            Panel actionPill = AppUi.CreateActionPill(actionText, isAvailable, difficultyColor, null);
            actionPill.Location = new Point(card.Width - actionPill.Width - 20, 168);

            card.Controls.Add(title);
            card.Controls.Add(description);
            card.Controls.Add(badge);
            card.Controls.Add(typeLabel);
            card.Controls.Add(actionPill);

            if (isAvailable)
            {
                string selectedTitle = problem.title;
                EventHandler click = (s, e) => ProblemCard_Click(selectedTitle);
                card.Click += click;
                title.Click += click;
                description.Click += click;
                actionPill.Click += click;
                foreach (Control child in actionPill.Controls)
                    child.Click += click;
            }

            return card;
        }

        private string TruncateText(string text, int maxLen)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLen)
                return text;
            return text.Substring(0, maxLen - 3) + "...";
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            ShowOtherForm(new UserForm());
        }

        private void ProblemCard_Click(string problemTitle)
        {
            if (ProblemCatalog.IsAvailable(problemTitle))
                ShowOtherForm(new ProblemDisplayForm(problemTitle));
        }
    }
}
