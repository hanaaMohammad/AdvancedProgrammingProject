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
    public class SubmitForm : AppForm
    {
        public static string LastSubmittedCode { get; set; }

        private const int SideMargin = 40;
        private const int ContentW = AppSizes.FormWidth - SideMargin * 2;

        private Toolbar toolbar;
        private Panel headerCard;
        private Panel editorCard;
        private Panel runPill;
        private TextBox codeEditor;
        private string problemName;
        private Color levelAccent;

        public SubmitForm(string problemName)
        {
            this.problemName = problemName;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Problem problem = ProblemLoadReadJs.GetByName(problemName);
            levelAccent = problem != null
                ? Theme.GetLevelColor(problem.level)
                : AppColors.Accent;
            string displayTitle = string.IsNullOrWhiteSpace(problem?.title)
                ? problemName
                : problem.title;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            toolbar.CloseRequested += (s, e) => Application.Exit();
            Controls.Add(toolbar);

            Button btnBack = MakeNavButton("\u2190 Back", 16, BackButton_Click);
            Button btnHome = MakeNavButton("Home", 104, HomeButton_Click);
            Controls.Add(btnBack);
            Controls.Add(btnHome);
            btnBack.BringToFront();
            btnHome.BringToFront();

            headerCard = UiHelper.CreateCard(Color.FromArgb(50, levelAccent), 20);
            headerCard.SetBounds(SideMargin, AppSizes.ContentTop, ContentW, 118);
            BuildHeader(displayTitle, problem?.level, problem?.type);
            Controls.Add(headerCard);

            editorCard = UiHelper.CreateCard(AppColors.DefaultBorder, 20);
            editorCard.SetBounds(SideMargin, 234, ContentW, 494);
            BuildEditor();
            Controls.Add(editorCard);

            runPill = UiHelper.CreateActionPill("Run Tests \u2192", true, levelAccent, RunTestsButton_Click);
            runPill.Location = new Point(AppSizes.FormWidth / 2 - runPill.Width / 2, 746);
            Controls.Add(runPill);

            if (LastSubmittedCode != null)
            {
                codeEditor.Text = LastSubmittedCode;
                LastSubmittedCode = null;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowAsMainForm(new LevelProblemForm());
        }

        private void BuildHeader(string displayTitle, string level, string type)
        {
            headerCard.Controls.Add(new Label
            {
                Text = displayTitle,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 18),
                Size = new Size(700, 32),
                BackColor = Color.Transparent,
            });

            int metaX = 24;
            int metaY = 58;

            if (!string.IsNullOrWhiteSpace(level))
            {
                Panel badge = UiHelper.CreateBadge(level);
                badge.Location = new Point(metaX, metaY);
                headerCard.Controls.Add(badge);
                metaX += badge.Width + 12;
            }

            headerCard.Controls.Add(new Label
            {
                Text = "C#",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(metaX, metaY + 4),
                AutoSize = true,
                BackColor = Color.Transparent,
            });
            metaX += 40;

            if (!string.IsNullOrWhiteSpace(type))
            {
                headerCard.Controls.Add(new Label
                {
                    Text = type,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = AppColors.MutedText,
                    Location = new Point(metaX, metaY + 4),
                    AutoSize = true,
                    BackColor = Color.Transparent,
                });
            }

            headerCard.Controls.Add(new Label
            {
                Text = "Write your solution below, then run against all test cases.",
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.MutedText,
                Location = new Point(24, 90),
                Size = new Size(700, 20),
                BackColor = Color.Transparent,
            });
        }

        private void BuildEditor()
        {
            editorCard.Controls.Add(new Label
            {
                Text = "Solution",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(20, 16),
                AutoSize = true,
                BackColor = Color.Transparent,
            });

            var editorHost = new Panel
            {
                Location = new Point(20, 40),
                Size = new Size(ContentW - 40, 442),
                BackColor = Color.Transparent,
            };
            editorHost.Paint += (s, e) =>
                UiHelper.PaintInset(e.Graphics, editorHost.ClientRectangle, 12);

            codeEditor = new TextBox
            {
                Location = new Point(12, 10),
                Size = new Size(editorHost.Width - 24, editorHost.Height - 20),
                Font = new Font("Consolas", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                AcceptsTab = true,
                AcceptsReturn = true,
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.InsetBack,
                ForeColor = Color.White,
                Text = "using System;\r\n\r\nclass Program\r\n{\r\n    static void Main(string[] args)\r\n    {\r\n        \r\n    }\r\n}",
            };

            editorHost.Controls.Add(codeEditor);
            editorCard.Controls.Add(editorHost);
        }

        private async void RunTestsButton_Click(object sender, EventArgs e)
        {
            string code = codeEditor.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter your code.", "Warning");
                return;
            }

            Problem problem = ProblemLoadReadJs.GetByName(problemName);
            if (problem == null || problem.TestCase == null || problem.TestCase.Count == 0)
            {
                MessageBox.Show("No test cases found for this problem.", "Error");
                return;
            }

            runPill.Enabled = false;
            codeEditor.Enabled = false;
            UseWaitCursor = true;

            try
            {
                var results = await Task.Run(() =>
                    new CodeRunner().RunTestCases(code, problem.TestCase));

                int passedCount = results.Count(r => r.Passed);
                if (results.Count > 0 && passedCount == results.Count)
                {
                    var userMgmt = new UserManagement();
                    userMgmt.UpdateScore(CurrentUser.Username, passedCount * 10);
                    CurrentUser.Score = userMgmt.GetScore(CurrentUser.Username);
                }

                LastSubmittedCode = code;
                ShowOtherForm(new FailedForm(problemName, results));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test execution failed: " + ex.Message, "Error");
            }
            finally
            {
                UseWaitCursor = false;
                runPill.Enabled = true;
                codeEditor.Enabled = true;
            }
        }
    }
}
