using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming.Forms
{
    public class LevelProblemForm : UserControl
    {
        public event EventHandler<string> ProblemSelected;
        public event EventHandler ProfileRequested;

        private Toolbar toolbar;
        private FlowLayoutPanel cardPanel;
        private TextBox searchBox;

        private List<Problem> allProblems = new List<Problem>();

        public LevelProblemForm()
        {
            this.Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            this.DoubleBuffered = true;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(7, 11, 20);

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            toolbar.CloseRequested += (s, e) => Application.Exit();

            PageBackButton.AddProfile(this,
                (s, e) => ProfileRequested?.Invoke(this, EventArgs.Empty));

            int cx = this.Width / 2;

            Label titleLabel = new Label
            {
                Text = "Problem Solving",
                Font = new Font("Segoe UI", 30, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(600, 60),
                Location = new Point(cx - 300, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            Label subtitleLabel = new Label
            {
                Text = "Sharpen your logic with coding challenges",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(170, 170, 170),
                AutoSize = false,
                Size = new Size(500, 30),
                Location = new Point(cx - 250, 130),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            searchBox = new TextBox
            {
                Size = new Size(420, 42),
                Location = new Point(cx - 210, 180),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(20, 28, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };

            searchBox.TextChanged += (s, e) =>
            {
                RenderProblems(searchBox.Text);
            };

            cardPanel = new FlowLayoutPanel
            {
                Location = new Point(40, 250),
                Size = new Size(this.Width - 80, this.Height - 270),
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(10),
            };

            cardPanel.Resize += (s, e) =>
            {
                ArrangeCards();
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(subtitleLabel);
            this.Controls.Add(searchBox);
            this.Controls.Add(cardPanel);

            LoadProblems();

            Theme.StylePage(this);
            Theme.Apply(this);

            this.ResumeLayout(false);
        }

        private void LoadProblems()
        {
            try
            {
                var loader = new ProblemLoadReadJs();
                allProblems = loader.GetAllProblems();
            }
            catch
            {
                return;
            }

            RenderProblems("");
        }

        private void RenderProblems(string query)
        {
            cardPanel.Controls.Clear();

            var filtered = allProblems;

            if (!string.IsNullOrWhiteSpace(query))
            {
                filtered = allProblems
                    .Where(p =>
                        (p.title ?? "").ToLower().Contains(query.ToLower()) ||
                        (p.description ?? "").ToLower().Contains(query.ToLower()))
                    .ToList();
            }

            foreach (var problem in filtered)
            {
                cardPanel.Controls.Add(CreateModernCard(problem));
            }

            ArrangeCards();
        }

        private void ArrangeCards()
        {
            int spacing = 20;

            int availableWidth = cardPanel.ClientSize.Width;

            int cardWidth =
                (availableWidth - (spacing * 4)) / 3;

            cardWidth = Math.Max(300, cardWidth);

            foreach (Control ctrl in cardPanel.Controls)
            {
                if (ctrl is Panel card)
                {
                    card.Width = cardWidth;
                    card.Height = 220;
                    card.Margin = new Padding(10);
                }
            }
        }

        private Panel CreateModernCard(Problem problem)
        {
            bool isAvailable = ProblemCatalog.IsAvailable(problem);
            Color difficultyColor = GetDifficultyColor(problem.level);

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

                using (GraphicsPath path = RoundedRect(rect, 20))
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        rect,
                        Color.FromArgb(20, 28, 45),
                        Color.FromArgb(17, 24, 39),
                        90f))
                    {
                        g.FillPath(brush, path);
                    }

                    Color borderColor = isAvailable
                        ? Color.FromArgb(50, difficultyColor)
                        : Color.FromArgb(40, 55, 75);
                    using (Pen pen = new Pen(borderColor, 2))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            };

            Label title = new Label
            {
                Text = problem.title,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = isAvailable ? Color.White : Color.FromArgb(140, 150, 165),
                Location = new Point(20, 20),
                Size = new Size(280, 35),
                BackColor = Color.Transparent
            };

            Label description = new Label
            {
                Text = TruncateText(problem.description ?? "", 120),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(180, 180, 180),
                Location = new Point(20, 65),
                Size = new Size(290, 60),
                BackColor = Color.Transparent
            };

            Panel badge = CreateDifficultyBadge(problem.level);
            badge.Location = new Point(20, 165);

            Label typeLabel = new Label
            {
                Text = problem.type,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(180, 180, 180),
                AutoSize = true,
                Location = new Point(120, 172),
                BackColor = Color.Transparent
            };

            string actionText = isAvailable ? "Solve →" : "Coming Soon";
            Panel actionPill = CreateCardActionPill(actionText, isAvailable, difficultyColor);
            actionPill.Location = new Point(card.Width - actionPill.Width - 20, 168);
            actionPill.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

            string selectedTitle = problem.title;

            if (isAvailable)
            {
                EventHandler clickHandler = (s, e) =>
                {
                    ProblemSelected?.Invoke(this, selectedTitle);
                };

                card.Click += clickHandler;
                title.Click += clickHandler;
                description.Click += clickHandler;
                actionPill.Click += clickHandler;
                foreach (Control child in actionPill.Controls)
                    child.Click += clickHandler;

                AddHoverEffect(card);
            }

            card.Controls.Add(title);
            card.Controls.Add(description);
            card.Controls.Add(badge);
            card.Controls.Add(typeLabel);
            card.Controls.Add(actionPill);

            return card;
        }

        private sealed class PillVisual
        {
            public Color Fill;
            public Color Border;
        }

        private Panel CreateCardActionPill(string text, bool isAvailable, Color accentColor)
        {
            const int pillHeight = 28;
            const int pillRadius = 14;
            var font = new Font("Segoe UI", 8, FontStyle.Bold);

            int pillWidth = Math.Max(88, TextRenderer.MeasureText(text, font).Width + 28);

            var normal = new PillVisual
            {
                Fill = isAvailable ? Color.FromArgb(18, 42, 34) : Color.FromArgb(22, 30, 48),
                Border = isAvailable ? Color.FromArgb(80, accentColor) : Color.FromArgb(45, 58, 80),
            };
            var hover = new PillVisual
            {
                Fill = Color.FromArgb(24, 58, 46),
                Border = Color.FromArgb(120, accentColor),
            };

            var pill = new Panel
            {
                Size = new Size(pillWidth, pillHeight),
                BackColor = Color.Transparent,
                Cursor = isAvailable ? Cursors.Hand : Cursors.Default,
                Tag = normal,
            };

            pill.Paint += (s, e) => PaintCardActionPill(e.Graphics, pill, (PillVisual)pill.Tag, pillRadius);

            var label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = font,
                ForeColor = isAvailable ? Color.FromArgb(220, 255, 240) : Color.FromArgb(120, 135, 155),
                BackColor = Color.Transparent,
                Cursor = pill.Cursor,
            };

            pill.Controls.Add(label);

            if (isAvailable)
            {
                EventHandler onEnter = (s, e) =>
                {
                    pill.Tag = hover;
                    pill.Invalidate();
                };
                EventHandler onLeave = (s, e) =>
                {
                    pill.Tag = normal;
                    pill.Invalidate();
                };

                pill.MouseEnter += onEnter;
                pill.MouseLeave += onLeave;
                label.MouseEnter += onEnter;
                label.MouseLeave += onLeave;
            }

            return pill;
        }

        private void PaintCardActionPill(Graphics g, Panel pill, PillVisual style, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
            using (GraphicsPath path = RoundedRect(rect, radius))
            {
                using (SolidBrush brush = new SolidBrush(style.Fill))
                    g.FillPath(brush, path);

                using (Pen pen = new Pen(style.Border, 1.5f))
                    g.DrawPath(pen, path);
            }
        }

        private Panel CreateDifficultyBadge(string level)
        {
            Color color = GetDifficultyColor(level);

            Panel badge = new Panel
            {
                Size = new Size(80, 28),
                BackColor = color
            };

            badge.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;

                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, badge.Width - 1, badge.Height - 1);

                using (GraphicsPath path = RoundedRect(rect, 14))
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        g.FillPath(brush, path);
                    }
                }
            };

            Label text = new Label
            {
                Text = GetLevelLabel(level),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            badge.Controls.Add(text);

            return badge;
        }

        private void AddHoverEffect(Panel card)
        {
            Color original = card.BackColor;

            card.MouseEnter += (s, e) =>
            {
                card.BackColor = Color.FromArgb(28, 38, 58);
                card.Padding = new Padding(2);
                card.Invalidate();
            };

            card.MouseLeave += (s, e) =>
            {
                card.BackColor = original;
                card.Padding = new Padding(0);
                card.Invalidate();
            };
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;

            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }

        private Color GetDifficultyColor(string level)
        {
            switch (level?.Trim().ToLower())
            {
                case "easy":
                    return Color.FromArgb(0, 200, 117);

                case "medium":
                    return Color.FromArgb(255, 183, 64);

                case "hard":
                    return Color.FromArgb(255, 82, 82);

                default:
                    return Color.Gray;
            }
        }

        private static string GetLevelLabel(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
                return "Practice";

            return char.ToUpper(level[0]) + level.Substring(1).ToLower();
        }

        private static string TruncateText(string text, int maxLen)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLen)
                return text;

            return text.Substring(0, maxLen - 3) + "...";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (cardPanel != null)
            {
                cardPanel.Size = new Size(this.Width - 80, this.Height - 270);

                ArrangeCards();
            }
        }
    }
}