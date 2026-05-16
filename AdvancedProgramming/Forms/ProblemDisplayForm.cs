using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class ProblemDisplayForm : AppForm
    {
        private const int SideMargin = 40;
        private const int HeaderTop = CatalogUi.ContentTop;

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel contentCard;
        private Panel statementPanel;
        private Panel examplePanel;
        private Panel tabStatementPill;
        private Panel tabExamplePill;
        private Panel actionPanel;
        private RichTextBox solutionBox;
        private Panel solutionPanel;
        private Panel exampleOutputHost;
        private PanelStars panelStars;
        private Label comingSoonLabel;

        private Problem problemChoice;
        private Color levelAccent;
        private bool isAvailable;
        private bool solutionVisible;
        private int selectedTab;

        private readonly string problemName;

        public ProblemDisplayForm(string problemName)
        {
            this.problemName = problemName;
            InitializeComponent(problemName);
        }

        private void InitializeComponent(string problemName)
        {
            problemChoice = ProblemLoadReadJs.GetByName(problemName);
            if (problemChoice == null)
            {
                MessageBox.Show("Problem not found", "MiniCamp Puzzle",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAvailable = ProblemCatalog.IsAvailable(problemName);
            levelAccent = Theme.GetLevelColor(problemChoice.level);
            string displayTitle = string.IsNullOrWhiteSpace(problemChoice.title)
                ? problemName
                : problemChoice.title;

            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            var (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => GoBack(),
                (s, e) => GoAppHome());
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            Color border = isAvailable
                ? Color.FromArgb(50, levelAccent)
                : CatalogUi.DefaultBorder;

            headerCard = CatalogUi.CreateCard(border, 20);
            BuildHeader(displayTitle);
            Controls.Add(headerCard);

            contentCard = CatalogUi.CreateCard(CatalogUi.DefaultBorder, 20);
            BuildContentCard();
            Controls.Add(contentCard);

            actionPanel = new Panel
            {
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            BuildActions();
            Controls.Add(actionPanel);

            FormAccessibility.SetShortcutHint(btnBack, "Esc", "Go back");
            FormAccessibility.SetShortcutHint(tabStatementPill, "Tab", "Statement and example tabs");

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildHeader(string displayTitle)
        {
            var title = new Label
            {
                Text = displayTitle,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Location = new Point(24, 20),
                Size = new Size(800, 36),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var badge = CatalogUi.CreateBadge(problemChoice.level);
            badge.Location = new Point(24, 64);

            var typeChip = CatalogUi.CreateTypeChip(problemChoice.type);
            typeChip.Location = new Point(116, 70);

            comingSoonLabel = new Label
            {
                Text = "Coming soon — only Easy problems are open in the editor right now.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Theme.Current.WarningColor,
                AutoSize = false,
                Size = new Size(520, 22),
                Location = new Point(24, 96),
                BackColor = Color.Transparent,
                Visible = !isAvailable,
                Tag = "NoTheme",
            };

            headerCard.Controls.Add(title);
            headerCard.Controls.Add(badge);
            headerCard.Controls.Add(typeChip);
            headerCard.Controls.Add(comingSoonLabel);
        }

        private void BuildContentCard()
        {
            var tabStrip = new Panel
            {
                Location = new Point(20, 16),
                Size = new Size(400, 40),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            tabStatementPill = CatalogUi.CreateTabPill("Statement", true, levelAccent);
            tabStatementPill.Location = new Point(0, 2);
            tabStatementPill.Click += (s, e) => SelectTab(0);
            foreach (Control c in tabStatementPill.Controls)
                c.Click += (s, e) => SelectTab(0);

            tabExamplePill = CatalogUi.CreateTabPill("Example", false, levelAccent);
            tabExamplePill.Location = new Point(tabStatementPill.Width + 10, 2);
            tabExamplePill.Click += (s, e) => SelectTab(1);
            foreach (Control c in tabExamplePill.Controls)
                c.Click += (s, e) => SelectTab(1);

            tabStrip.Controls.Add(tabStatementPill);
            tabStrip.Controls.Add(tabExamplePill);

            statementPanel = CreateScrollPanel();
            BuildStatementTab(statementPanel);

            examplePanel = CreateScrollPanel();
            examplePanel.Visible = false;
            BuildExampleTab(examplePanel);

            contentCard.Controls.Add(tabStrip);
            contentCard.Controls.Add(statementPanel);
            contentCard.Controls.Add(examplePanel);
        }

        private static Panel CreateScrollPanel()
        {
            return new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
        }

        private void SelectTab(int index)
        {
            selectedTab = index;
            statementPanel.Visible = index == 0;
            examplePanel.Visible = index == 1;
            CatalogUi.SetTabSelected(tabStatementPill, index == 0, levelAccent);
            CatalogUi.SetTabSelected(tabExamplePill, index == 1, levelAccent);
        }

        private void BuildStatementTab(Panel parent)
        {
            int y = 8;
            int w = 800;
            y = AddSection(parent, ref y, "Description", problemChoice.description, new Font("Segoe UI", 11), w);
            y = AddSection(parent, ref y, "Input format", problemChoice.input, DesignTokens.Typography.BodySmall, w);
            y = AddSection(parent, ref y, "Output format", problemChoice.output, DesignTokens.Typography.BodySmall, w);
            AddSection(parent, ref y, "Constraints", problemChoice.Constraints, DesignTokens.Typography.BodySmall, w);
        }

        private void BuildExampleTab(Panel parent)
        {
            int y = 8;
            int w = 800;
            bool isPattern = string.Equals(problemChoice.type, "pattren", StringComparison.OrdinalIgnoreCase);

            y = AddSection(parent, ref y, "Sample input", problemChoice.Example?.input ?? "", DesignTokens.Typography.Code, w);

            int outputH = isPattern ? 100 : MeasureTextHeight(problemChoice.Example?.output ?? "", DesignTokens.Typography.Code, w - 48) + 16;
            exampleOutputHost = CreateInsetSection("Sample output", w, DesignTokens.Sizing.LabelHeight + 8 + outputH);
            exampleOutputHost.Location = new Point(16, y);

            if (isPattern)
            {
                panelStars = new PanelStars
                {
                    Location = new Point(12, DesignTokens.Sizing.LabelHeight + 12),
                    Size = new Size(w - 72, 88),
                };
                exampleOutputHost.Controls.Add(panelStars);
            }
            else
            {
                var outputBox = CreateInsetTextBox(problemChoice.Example?.output ?? "", DesignTokens.Typography.Code, w - 72, outputH - 8);
                outputBox.Location = new Point(12, DesignTokens.Sizing.LabelHeight + 12);
                exampleOutputHost.Controls.Add(outputBox);
            }

            parent.Controls.Add(exampleOutputHost);
            y += exampleOutputHost.Height + 12;

            y = AddSection(parent, ref y, "Explanation", problemChoice.Example?.explanation ?? "", DesignTokens.Typography.BodySmall, w);

            solutionPanel = CreateInsetSection("Reference solution", w, 200);
            solutionPanel.Visible = false;
            solutionPanel.Location = new Point(16, y);

            int solutionH = GetCodeViewHeight(problemChoice.solution, 120, 240);
            solutionBox = new RichTextBox
            {
                Location = new Point(12, DesignTokens.Sizing.LabelHeight + 12),
                Size = new Size(w - 72, solutionH),
                BorderStyle = BorderStyle.None,
                BackColor = CatalogUi.InsetBack,
                ReadOnly = true,
                TabStop = false,
                Tag = "NoTheme",
            };
            solutionPanel.Height = DesignTokens.Sizing.LabelHeight + 20 + solutionH;
            solutionPanel.Controls.Add(solutionBox);
            parent.Controls.Add(solutionPanel);
        }

        private void BuildActions()
        {
            actionPanel.Controls.Clear();

            string solveText = isAvailable ? "Solve \u2192" : "Coming Soon";
            EventHandler onSolve = isAvailable
                ? (EventHandler)((s, e) => ShowScreen(new SubmitForm(problemName)))
                : null;
            var solvePill = CatalogUi.CreateActionPill(solveText, isAvailable, levelAccent, onSolve);
            solvePill.Name = "solvePill";

            actionPanel.Controls.Add(solvePill);

            if (isAvailable && !string.IsNullOrWhiteSpace(problemChoice.solution))
            {
                var solutionPill = CatalogUi.CreateActionPill(
                    "Show Solution",
                    true,
                    levelAccent,
                    (s, e) => ToggleSolution());
                solutionPill.Name = "solutionPill";
                solutionPill.Location = new Point(solvePill.Right + 12, 0);
                actionPanel.Controls.Add(solutionPill);
            }

            FormAccessibility.SetShortcutHint(
                actionPanel.Controls[0],
                "Enter",
                "Open code editor");
        }

        private Panel CreateInsetSection(string caption, int width, int height)
        {
            var section = new Panel
            {
                Width = width,
                Height = height,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(section);

            int captionH = DesignTokens.Sizing.LabelHeight;
            section.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, captionH + 6, section.Width, section.Height - captionH - 6);
                CatalogUi.PaintInset(e.Graphics, inset);
            };

            section.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, captionH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            return section;
        }

        private static TextBox CreateInsetTextBox(string text, Font font, int width, int height)
        {
            return new TextBox
            {
                Text = text ?? string.Empty,
                ReadOnly = true,
                Multiline = true,
                Font = font,
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None,
                BackColor = CatalogUi.InsetBack,
                ForeColor = Color.White,
                TabStop = false,
                Tag = "NoTheme",
            };
        }

        private int AddSection(Panel parent, ref int y, string caption, string text, Font font, int contentWidth)
        {
            int blockW = contentWidth - 32;
            int textH = Math.Max(48, MeasureTextHeight(text, font, blockW - 24));
            int blockH = DesignTokens.Sizing.LabelHeight + 14 + textH;

            var block = new Panel
            {
                Location = new Point(16, y),
                Size = new Size(blockW, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(block);

            int captionH = DesignTokens.Sizing.LabelHeight;
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, captionH + 6, block.Width, block.Height - captionH - 6);
                CatalogUi.PaintInset(e.Graphics, inset);
            };

            block.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(0, 0),
                Size = new Size(blockW, captionH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });

            var body = CreateInsetTextBox(text, font, blockW - 24, textH);
            body.Location = new Point(12, captionH + 12);
            block.Controls.Add(body);

            parent.Controls.Add(block);
            y += blockH + 12;
            return y;
        }

        private void ToggleSolution()
        {
            if (string.IsNullOrWhiteSpace(problemChoice?.solution))
            {
                MessageBox.Show(
                    "No solution is available for this problem yet.",
                    "Solution",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            solutionVisible = !solutionVisible;
            solutionPanel.Visible = solutionVisible;

            foreach (Control c in actionPanel.Controls)
            {
                if (c.Name == "solutionPill" && c.Controls.Count > 0 && c.Controls[0] is Label lbl)
                    lbl.Text = solutionVisible ? "Hide Solution" : "Show Solution";
            }

            if (solutionVisible)
            {
                SelectTab(1);
                CSharpCodeHighlighter.Apply(solutionBox, problemChoice.solution ?? string.Empty);
            }
        }

        private static int MeasureTextHeight(string text, Font font, int width)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 40;
            var size = TextRenderer.MeasureText(text, font, new Size(width, 0), TextFormatFlags.WordBreak);
            return size.Height + 12;
        }

        private static int GetCodeViewHeight(string code, int minHeight, int maxHeight)
        {
            string normalized = CSharpCodeHighlighter.NormalizeCode(code);
            if (string.IsNullOrEmpty(normalized))
                return minHeight;

            int lines = normalized.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
            return Math.Min(maxHeight, Math.Max(minHeight, lines * 18 + 16));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            if (headerCard == null)
                return;

            int contentW = Math.Max(600, Width - SideMargin * 2);
            int cx = Width / 2;
            int left = cx - contentW / 2;

            int headerH = isAvailable ? 100 : 124;
            headerCard.SetBounds(left, HeaderTop, contentW, headerH);

            if (headerCard.Controls.Count > 0 && headerCard.Controls[0] is Label titleLbl)
                titleLbl.Width = contentW - 48;

            int contentTop = headerCard.Bottom + 16;
            int actionH = 48;
            int contentH = Math.Max(280, Height - contentTop - actionH - 24);

            contentCard.SetBounds(left, contentTop, contentW, contentH);

            int scrollTop = 64;
            int scrollPad = 20;
            statementPanel.SetBounds(scrollPad, scrollTop, contentW - scrollPad * 2, contentH - scrollTop - 16);
            examplePanel.SetBounds(scrollPad, scrollTop, contentW - scrollPad * 2, contentH - scrollTop - 16);

            int totalActionW = 0;
            foreach (Control c in actionPanel.Controls)
                totalActionW += c.Width + 12;
            if (totalActionW > 0)
                totalActionW -= 12;

            actionPanel.SetBounds(cx - totalActionW / 2, contentCard.Bottom + 12, totalActionW, actionH);
            int ax = 0;
            foreach (Control c in actionPanel.Controls)
            {
                c.Location = new Point(ax, 4);
                ax += c.Width + 12;
            }
        }
    }
}
