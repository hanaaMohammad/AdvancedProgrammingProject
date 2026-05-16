using AdvancedProgramming;
using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class ProblemDisplayForm : AppForm
    {
        private const int Margin = 40;
        private const int BlockW = 800;
        private const int BodyH = 448;
        private static readonly Font Body = new Font("Segoe UI", 10);
        private static readonly Font Title = new Font("Segoe UI", 11);
        private static readonly Font Code = new Font("Consolas", 10);

        private readonly string problemName;
        private Problem problem;
        private Color accent;
        private bool open;
        private bool showSolution;

        private Panel headerCard;
        private Panel contentCard;
        private Panel statementPanel;
        private Panel examplePanel;
        private Panel tabStatement;
        private Panel tabExample;
        private Panel actionPanel;
        private Panel solutionPanel;
        private Panel solutionPill;
        private RichTextBox solutionBox;

        public ProblemDisplayForm(string problemName)
        {
            this.problemName = problemName;
            problem = ProblemLoadReadJs.GetByName(problemName);
            if (problem == null)
            {
                MessageBox.Show("Problem not found", "MiniCamp Puzzle",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            open = StauationProblem.IsAvailable(problemName);
            accent = Theme.GetLevelColor(problem.level);
            BackColor = AppColors.PageBack;
            Build();
            Layout();
        }

        private void Build()
        {
            var toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            var back = MakeNavButton("\u2190 Back", 16, (s, e) => Close());
            var home = MakeNavButton("Home", 104, (s, e) => ShowAsMainForm(new LevelProblemForm()));
            Controls.Add(back);
            Controls.Add(home);
            back.BringToFront();
            home.BringToFront();

            string title = string.IsNullOrWhiteSpace(problem.title) ? problemName : problem.title;
            headerCard = CreateCard(open ? Color.FromArgb(50, accent) : AppColors.DefaultBorder, 20);
            headerCard.Controls.Add(MkLabel(title, new Font("Segoe UI", 22, FontStyle.Bold), 24, 20, BlockW - 48, 36));
            var badge = CreateBadge(problem.level);
            badge.Location = new Point(24, 64);
            headerCard.Controls.Add(badge);
            var chip = CreateTypeChip(problem.type);
            chip.Location = new Point(116, 70);
            headerCard.Controls.Add(chip);
            headerCard.Controls.Add(MkLabel(
                "Coming soon — only Easy problems are open in the editor right now.",
                new Font("Segoe UI", 9), 24, 96, 520, 22, AppColors.Warning, !open));
            Controls.Add(headerCard);

            contentCard = CreateCard(AppColors.DefaultBorder, 20);
            statementPanel = ScrollPanel();
            examplePanel = ScrollPanel();
            examplePanel.Visible = false;
            BuildTabs();
            FillStatement();
            FillExample();
            contentCard.Controls.Add(statementPanel);
            contentCard.Controls.Add(examplePanel);
            Controls.Add(contentCard);

            actionPanel = new Panel { BackColor = Color.Transparent };
            EventHandler solve = open ? (EventHandler)((s, e) => ShowOtherForm(new SubmitForm(problemName))) : null;
            actionPanel.Controls.Add(CreateActionPill(open ? "Solve \u2192" : "Coming Soon", open, accent, solve));
            if (open && !string.IsNullOrWhiteSpace(problem.solution))
            {
                solutionPill = CreateActionPill("Show Solution", true, accent, ToggleSolution);
                actionPanel.Controls.Add(solutionPill);
            }
            Controls.Add(actionPanel);
        }

        private void BuildTabs()
        {
            var strip = new Panel { Location = new Point(20, 16), Size = new Size(400, 40), BackColor = Color.Transparent };
            tabStatement = CreateTabPill("Statement", true, accent);
            tabStatement.Location = new Point(0, 2);
            WireTabPill(tabStatement, () => PickTab(0));
            tabExample = CreateTabPill("Example", false, accent);
            tabExample.Location = new Point(tabStatement.Width + 10, 2);
            WireTabPill(tabExample, () => PickTab(1));
            strip.Controls.Add(tabStatement);
            strip.Controls.Add(tabExample);
            contentCard.Controls.Add(strip);
        }

        private void PickTab(int i)
        {
            statementPanel.Visible = i == 0;
            examplePanel.Visible = i == 1;
            SetTabSelected(tabStatement, i == 0, accent);
            SetTabSelected(tabExample, i == 1, accent);
        }

        private void FillStatement()
        {
            int y = 8;
            y = Section(statementPanel, ref y, "Description", problem.description, Title);
            y = Section(statementPanel, ref y, "Input format", problem.input, Body);
            y = Section(statementPanel, ref y, "Output format", problem.output, Body);
            Section(statementPanel, ref y, "Constraints", problem.Constraints, Body);
        }

        private void FillExample()
        {
            int y = 8;
            var ex = problem.Example;
            bool stars = string.Equals(problem.type, "pattren", StringComparison.OrdinalIgnoreCase);

            y = Section(examplePanel, ref y, "Sample input", ex?.input ?? "", Code);

            int outH = stars ? 100 : MeasureWrappedText(ex?.output ?? "", Code, BlockW - 72) + 4;
            var outBlock = CreateInsetBlock("Sample output", BlockW - 32, 30 + outH);
            outBlock.Location = new Point(16, y);
            if (stars)
                outBlock.Controls.Add(new PanelStars { Location = new Point(12, 34), Size = new Size(BlockW - 72, 88) });
            else
            {
                var box = CreateReadOnlyBox(ex?.output ?? "", Code, BlockW - 72, outH);
                box.Location = new Point(12, 34);
                outBlock.Controls.Add(box);
            }
            examplePanel.Controls.Add(outBlock);
            y += outBlock.Height + 12;

            y = Section(examplePanel, ref y, "Explanation", ex?.explanation ?? "", Body);

            int solH = SolutionHeight(problem.solution);
            solutionPanel = CreateInsetBlock("Reference solution", BlockW - 32, 42 + solH);
            solutionPanel.Visible = false;
            solutionPanel.Location = new Point(16, y);
            solutionBox = new RichTextBox
            {
                Location = new Point(12, 34),
                Size = new Size(BlockW - 72, solH),
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.InsetBack,
                ReadOnly = true,
                TabStop = false,
            };
            solutionPanel.Controls.Add(solutionBox);
            examplePanel.Controls.Add(solutionPanel);
        }

        private int Section(Panel panel, ref int y, string caption, string text, Font font) =>
            AddReadOnlySection(panel, ref y, caption, text, font, BlockW);

        private void ToggleSolution(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(problem.solution))
            {
                MessageBox.Show("No solution is available for this problem yet.", "Solution",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            showSolution = !showSolution;
            solutionPanel.Visible = showSolution;
            if (solutionPill != null && solutionPill.Controls[0] is Label lbl)
                lbl.Text = showSolution ? "Hide Solution" : "Show Solution";
            if (!showSolution)
                return;

            PickTab(1);
            ShowSolution(solutionBox, problem.solution);
        }

        private static void ShowSolution(RichTextBox box, string code)
        {
            box.Text = NormalizeSolution(code);
            box.Font = Code;
            box.BackColor = AppColors.InsetBack;
            box.ForeColor = AppColors.Text;
        }

        private static string NormalizeSolution(string code)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;
            return code.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t")
                .Replace("\r\n", "\n").Replace("\r", "\n")
                .Replace("\n", Environment.NewLine);
        }

        private static int SolutionHeight(string code, int min = 120, int max = 240)
        {
            code = NormalizeSolution(code);
            if (code.Length == 0)
                return min;
            int lines = 1;
            foreach (char c in code)
                if (c == '\n')
                    lines++;
            return Math.Min(max, Math.Max(min, lines * 18 + 16));
        }

        private void Layout()
        {
            if (headerCard == null)
                return;

            int w = AppSizes.FormWidth - Margin * 2;
            int cx = AppSizes.FormWidth / 2;
            headerCard.SetBounds(Margin, AppSizes.ContentTop, w, open ? 100 : 124);
            if (headerCard.Controls[0] is Label t)
                t.Width = w - 48;

            contentCard.SetBounds(Margin, headerCard.Bottom + 16, w, BodyH);
            int top = 64, pad = 20;
            int sw = w - pad * 2, sh = BodyH - top - 16;
            statementPanel.SetBounds(pad, top, sw, sh);
            examplePanel.SetBounds(pad, top, sw, sh);

            int aw = 0;
            foreach (Control c in actionPanel.Controls)
                aw += c.Width + 12;
            if (aw > 0)
                aw -= 12;
            actionPanel.SetBounds(cx - aw / 2, contentCard.Bottom + 12, aw, 48);
            int x = 0;
            foreach (Control c in actionPanel.Controls)
            {
                c.Location = new Point(x, 4);
                x += c.Width + 12;
            }
        }

        private static Panel ScrollPanel() =>
            new Panel { AutoScroll = true, BackColor = Color.Transparent };

        private static Label MkLabel(string text, Font font, int x, int y, int w, int h,
            Color? fore = null, bool visible = true) =>
            new Label
            {
                Text = text,
                Font = font,
                ForeColor = fore ?? Color.White,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Color.Transparent,
                Visible = visible,
            };
    }
}
