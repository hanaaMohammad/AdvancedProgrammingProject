using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AdvancedProgramming.Forms;
using AdvancedProgramming.ProblemClasses;

namespace AdvancedProgramming
{
    public partial class MainForm : Form
    {
        private Panel pageContainer;
        private Stack<Action> backStack;
        private UserControl currentPage;
        private bool isTransitioning;

        public MainForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            this.MinimumSize = new Size(DesignTokens.MinFormWidth, DesignTokens.MinFormHeight);
            this.KeyPreview = true;

            backStack = new Stack<Action>();

            pageContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Current.FormBackColor,
            };
            pageContainer.Resize += (s, e) => CenterCurrentPage();
            this.Controls.Add(pageContainer);

            Theme.Apply(this);

            NavigateToStartup();
        }

        private void ShowPage(UserControl page, bool animate = true)
        {
            if (isTransitioning)
                animate = false;

            currentPage = page;
            pageContainer.Controls.Clear();
            page.Dock = DockStyle.None;
            page.Anchor = AnchorStyles.None;
            page.Size = new Size(
                Math.Min(pageContainer.ClientSize.Width, DesignTokens.FormWidth),
                Math.Min(pageContainer.ClientSize.Height, DesignTokens.FormHeight)
            );
            pageContainer.Controls.Add(page);
            CenterCurrentPage();

            if (animate)
            {
                isTransitioning = true;
                PageTransition.AnimateIn(page, pageContainer, CenterCurrentPage, () =>
                {
                    isTransitioning = false;
                    FocusCurrentPage();
                });
            }
            else
            {
                FocusCurrentPage();
            }
        }

        private void FocusCurrentPage()
        {
            if (currentPage == null) return;

            currentPage.Select();

            switch (currentPage)
            {
                case LogInForm _:
                case SignUpForm _:
                    FormAccessibility.FocusFirstInput(currentPage);
                    break;
                case SubmitForm _:
                    currentPage.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Multiline)?.Focus();
                    break;
                case StartupForm _:
                    FormAccessibility.FocusPrimaryAction(currentPage);
                    break;
            }
        }

        private void CenterCurrentPage()
        {
            if (pageContainer == null || pageContainer.Controls.Count == 0) return;
            var page = pageContainer.Controls[0];
            page.Location = new Point(
                Math.Max(0, (pageContainer.ClientSize.Width - page.Width) / 2),
                Math.Max(0, (pageContainer.ClientSize.Height - page.Height) / 2)
            );
        }

        private void NavigateToStartup()
        {
            backStack.Clear();
            backStack.Push(() => NavigateToStartup());
            var page = new StartupForm();
            page.LoginRequested += (s, e) => NavigateToLogin();
            page.SignUpRequested += (s, e) => NavigateToSignUp();
            ShowPage(page, animate: false);
        }

        private void NavigateToLogin()
        {
            backStack.Push(() => NavigateToLogin());
            var page = new LogInForm();
            page.LoginSuccess += (s, e) => NavigateToHome();
            page.BackRequested += (s, e) => GoBack();
            page.HomeRequested += (s, e) => NavigateToStartup();
            ShowPage(page);
        }

        private void NavigateToSignUp()
        {
            backStack.Push(() => NavigateToSignUp());
            var page = new SignUpForm();
            page.SignUpSuccess += (s, e) => NavigateToHome();
            page.BackRequested += (s, e) => GoBack();
            page.HomeRequested += (s, e) => NavigateToStartup();
            ShowPage(page);
        }

        private void NavigateToHome()
        {
            backStack.Push(() => NavigateToHome());
            var page = new LevelProblemForm();
            page.ProblemSelected += (s, problem) =>
            {
                if (ProblemCatalog.IsAvailable(problem))
                    NavigateToProblemDisplay(problem);
            };
            page.ProfileRequested += (s, e) => NavigateToUser();
            ShowPage(page);
        }

        private void NavigateToProblemDisplay(string problemName)
        {
            backStack.Push(() => NavigateToProblemDisplay(problemName));
            var page = new ProblemDisplayForm(problemName);
            page.SolveRequested += (s, e) => NavigateToSubmit(problemName);
            page.BackRequested += (s, e) => GoBack();
            page.HomeRequested += (s, e) => NavigateToHome();
            ShowPage(page);
        }

        private void NavigateToSubmit(string problemName)
        {
            backStack.Push(() => NavigateToSubmit(problemName));
            var page = new SubmitForm(problemName);
            page.TestResultsReady += (s, results) => NavigateToResult(problemName, results);
            page.BackRequested += (s, e) => GoBack();
            page.HomeRequested += (s, e) => NavigateToHome();
            if (SubmitForm.LastSubmittedCode != null)
            {
                page.RestoreCode(SubmitForm.LastSubmittedCode);
                SubmitForm.LastSubmittedCode = null;
            }
            ShowPage(page);
        }

        private void NavigateToResult(string problemName, Service.CodeRunnerTestResultList results)
        {
            backStack.Push(() => NavigateToResult(problemName, results));
            var page = new FailedForm(problemName, results.Results);
            page.BackRequested += (s, e) => GoBack();
            page.HomeRequested += (s, e) => NavigateToHome();
            ShowPage(page);
        }

        private void NavigateToUser()
        {
            backStack.Push(() => NavigateToUser());
            var page = new UserForm();
            page.HomeRequested += (s, e) => NavigateToHome();
            ShowPage(page);
        }

        private void GoBack()
        {
            if (isTransitioning) return;

            if (backStack.Count > 1)
            {
                backStack.Pop();
                var prev = backStack.Pop();
                prev();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterCurrentPage();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                GoBack();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Enter) && currentPage is SubmitForm submitPage)
            {
                submitPage.RunTests();
                return true;
            }

            if (keyData == Keys.Enter && currentPage is LogInForm loginPage)
            {
                loginPage.SubmitLogin();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
