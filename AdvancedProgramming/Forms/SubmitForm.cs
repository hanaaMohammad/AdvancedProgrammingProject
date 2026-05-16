using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedProgramming.Components;
using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;
using AdvancedProgramming.Session;

namespace AdvancedProgramming.Forms
{
    public class SubmitForm : UserControl
    {
        public static string LastSubmittedCode { get; set; }

        public event EventHandler<CodeRunnerTestResultList> TestResultsReady;
        public event EventHandler BackRequested;
        public event EventHandler HomeRequested;

        private const int SideMargin = 40;
        private const int HeaderTop = 72;

        private Toolbar toolbar;
        private Button btnBack;
        private Button btnHome;
        private Panel headerCard;
        private Panel editorCard;
        private Panel runPill;
        private TextBox codeEditor;
        private LoadingOverlay loadingOverlay;
        private string problemName;
        private Color levelAccent;
        private bool isRunning;

        public SubmitForm(string problemName)
        {
            this.problemName = problemName;
            Size = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            CatalogUi.EnableDoubleBuffer(this);
            DoubleBuffered = true;
            InitializeComponent();
            toolbar.CloseRequested += (s, e) => Application.Exit();
        }

        private void InitializeComponent()
        {
            var problem = ProblemLoadReadJs.GetByName(problemName);
            levelAccent = problem != null
                ? Theme.GetLevelColor(problem.level)
                : Theme.Current.AccentColor;
            string displayTitle = string.IsNullOrWhiteSpace(problem?.title)
                ? problemName
                : problem.title;

            BackColor = CatalogUi.PageBack;
            SuspendLayout();

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            Controls.Add(toolbar);

            (btnBack, btnHome) = PageBackButton.Create(
                (s, e) => BackRequested?.Invoke(this, EventArgs.Empty),
                (s, e) => HomeRequested?.Invoke(this, EventArgs.Empty));
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            Color border = Color.FromArgb(50, levelAccent);
            headerCard = CatalogUi.CreateCard(border, 20);
            BuildHeader(displayTitle, problem?.level, problem?.type);
            Controls.Add(headerCard);

            editorCard = CatalogUi.CreateCard(CatalogUi.DefaultBorder, 20);
            BuildEditor();
            Controls.Add(editorCard);

            runPill = CatalogUi.CreateActionPill(
                "Run Tests \u2192",
                true,
                levelAccent,
                (s, e) => ButtonRun_Click(s, e));
            Controls.Add(runPill);

            loadingOverlay = new LoadingOverlay();
            editorCard.Controls.Add(loadingOverlay);

            FormAccessibility.SetShortcutHint(runPill, "Ctrl+Enter", "Run test cases");
            FormAccessibility.SetShortcutHint(btnBack, "Esc", "Go back");
            FormAccessibility.SetShortcutHint(codeEditor, "Tab", "Indent code");

            ResumeLayout(false);
            ApplyLayout();
        }

        private void BuildHeader(string displayTitle, string level, string type)
        {
            var title = new Label
            {
                Text = displayTitle,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 18),
                Size = new Size(700, 32),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            headerCard.Controls.Add(title);

            int metaX = 24;
            int metaY = 58;

            if (!string.IsNullOrWhiteSpace(level))
            {
                var badge = CatalogUi.CreateBadge(level);
                badge.Location = new Point(metaX, metaY);
                headerCard.Controls.Add(badge);
                metaX += badge.Width + 12;
            }

            var langChip = new Label
            {
                Text = "C#",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(metaX, metaY + 4),
                AutoSize = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            headerCard.Controls.Add(langChip);
            metaX += 40;

            if (!string.IsNullOrWhiteSpace(type))
            {
                var typeChip = CatalogUi.CreateTypeChip(type);
                typeChip.Location = new Point(metaX, metaY + 4);
                headerCard.Controls.Add(typeChip);
            }

            headerCard.Controls.Add(new Label
            {
                Text = "Write your solution below, then run against all test cases.",
                Font = new Font("Segoe UI", 9),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(24, 90),
                Size = new Size(700, 20),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
        }

        private void BuildEditor()
        {
            var prompt = new Label
            {
                Text = "Solution",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = CatalogUi.MutedText,
                Location = new Point(20, 16),
                AutoSize = true,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };

            var editorHost = new Panel
            {
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            CatalogUi.EnableDoubleBuffer(editorHost);
            editorHost.Paint += (s, e) =>
            {
                var inset = editorHost.ClientRectangle;
                CatalogUi.PaintInset(e.Graphics, inset, 12);
            };

            codeEditor = new TextBox
            {
                Font = DesignTokens.Typography.Code,
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                AcceptsTab = true,
                AcceptsReturn = true,
                BorderStyle = BorderStyle.None,
                BackColor = CatalogUi.InsetBack,
                ForeColor = Color.White,
                Text = "using System;\r\n\r\nclass Program\r\n{\r\n    static void Main(string[] args)\r\n    {\r\n        \r\n    }\r\n}",
                Tag = "NoTheme",
            };

            editorHost.Controls.Add(codeEditor);
            editorCard.Controls.Add(prompt);
            editorCard.Controls.Add(editorHost);
            editorHost.Name = "editorHost";
        }

        public void RunTests()
        {
            if (!isRunning)
                ButtonRun_Click(null, EventArgs.Empty);
        }

        public void RestoreCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                codeEditor.Text = code;
        }

        private async void ButtonRun_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            string code = codeEditor.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter your code.", "Warning");
                return;
            }

            var problem = ProblemLoadReadJs.GetByName(problemName);

            if (problem == null || problem.TestCase == null || problem.TestCase.Count == 0)
            {
                MessageBox.Show("No test cases found for this problem.", "Error");
                return;
            }

            SetRunningState(true);

            try
            {
                var results = await Task.Run(() =>
                    new CodeRunner().RunTestCases(code, problem.TestCase)
                ).ConfigureAwait(true);

                int passedCount = results.Count(r => r.Passed);

                if (results.Count > 0 && passedCount == results.Count)
                {
                    var userMgmt = new UserManagement();
                    userMgmt.UpdateScore(CurrentUser.Username, passedCount * 10);
                    CurrentUser.Score = userMgmt.GetScore(CurrentUser.Username);
                }

                LastSubmittedCode = code;

                TestResultsReady?.Invoke(this, new CodeRunnerTestResultList
                {
                    Results = results,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test execution failed: " + ex.Message, "Error");
            }
            finally
            {
                SetRunningState(false);
            }
        }

        private void SetRunningState(bool running)
        {
            isRunning = running;
            runPill.Enabled = !running;
            codeEditor.Enabled = !running;
            btnBack.Enabled = !running;
            btnHome.Enabled = !running;

            if (running)
                loadingOverlay.Show();
            else
                loadingOverlay.HideOverlay();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyLayout();
            loadingOverlay?.BringToFront();
        }

        private void ApplyLayout()
        {
            if (headerCard == null)
                return;

            int contentW = Math.Max(600, Width - SideMargin * 2);
            int cx = Width / 2;
            int left = cx - contentW / 2;

            headerCard.SetBounds(left, HeaderTop, contentW, 118);

            int editorTop = headerCard.Bottom + 16;
            int runH = 44;
            int editorH = Math.Max(320, Height - editorTop - runH - 28);
            editorCard.SetBounds(left, editorTop, contentW, editorH);

            var prompt = editorCard.Controls[0];
            var editorHost = editorCard.Controls[1];
            prompt.Location = new Point(20, 16);
            editorHost.SetBounds(20, 40, contentW - 40, editorH - 52);
            codeEditor.SetBounds(12, 10, editorHost.Width - 24, editorHost.Height - 20);

            runPill.Location = new Point(cx - runPill.Width / 2, editorCard.Bottom + 12);
        }
    }
}
