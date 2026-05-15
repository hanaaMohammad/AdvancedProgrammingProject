using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Forms;

namespace AdvancedProgramming
{
    public partial class MainForm : Form
    {
        private Panel pageContainer;
        private Stack<Action> backStack;

        public MainForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1100, 800);
            this.MinimumSize = new Size(850, 520);

            backStack = new Stack<Action>();

            pageContainer = new Panel
            {
                Dock = DockStyle.Fill,
            };
            pageContainer.Resize += (s, e) => CenterCurrentPage();
            this.Controls.Add(pageContainer);

            Theme.Apply(this);

            pageContainer.BackColor = Theme.Current.FormBackColor;
            Theme.ThemeChanged += () =>
            {
                if (pageContainer != null)
                    pageContainer.BackColor = Theme.Current.FormBackColor;
            };

            NavigateToStartup();
        }

        private void ShowPage(UserControl page)
        {
            pageContainer.Controls.Clear();
            page.Dock = DockStyle.None;
            page.Anchor = AnchorStyles.None;
            pageContainer.Controls.Add(page);
            CenterCurrentPage();
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

        private void NavigateToStartup(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToStartup(false));
            var page = new StartupForm();
            page.LoginRequested += (s, e) => NavigateToLogin();
            page.SignUpRequested += (s, e) => NavigateToSignUp();
            ShowPage(page);
        }

        private void NavigateToLogin(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToLogin(false));
            var page = new LogInForm();
            page.LoginSuccess += (s, e) => NavigateToHome();
            page.BackRequested += (s, e) => GoBack();
            ShowPage(page);
        }

        private void NavigateToSignUp(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToSignUp(false));
            var page = new SignUpForm();
            page.SignUpSuccess += (s, e) => NavigateToHome();
            page.BackRequested += (s, e) => GoBack();
            ShowPage(page);
        }

        private void NavigateToHome(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToHome(false));
            var page = new HomeForm();
            page.UserRequested += (s, e) => NavigateToUser();
            page.ProblemsRequested += (s, e) => NavigateToLevelProblem();
            ShowPage(page);
        }

        private void NavigateToLevelProblem(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToLevelProblem(false));
            var page = new LevelProblemForm();
            page.ProblemSelected += (s, problem) => NavigateToProblemDisplay(problem);
            page.BackRequested += (s, e) => GoBack();
            ShowPage(page);
        }

        private void NavigateToProblemDisplay(string problemName, bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToProblemDisplay(problemName, false));
            var page = new ProblemDisplayForm(problemName);
            page.SolveRequested += (s, e) => NavigateToSubmit(problemName);
            page.HomeRequested += (s, e) => NavigateToHome();
            page.UserRequested += (s, e) => NavigateToUser();
            page.BackRequested += (s, e) => GoBack();
            ShowPage(page);
        }

        private void NavigateToSubmit(string problemName, bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToSubmit(problemName, false));
            var page = new SubmitForm(problemName);
            page.TestResultsReady += (s, results) => NavigateToResult(problemName, results);
            page.HomeRequested += (s, e) => NavigateToHome();
            page.BackRequested += (s, e) => GoBack();
            ShowPage(page);
        }

        private void NavigateToResult(string problemName, Service.CodeRunnerTestResultList results, bool addToHistory = true)
        {
            if (results.AllPassed)
            {
                if (addToHistory) backStack.Push(() => NavigateToResult(problemName, results, false));
                var page = new AcceptedForm();
                page.HomeRequested += (s, e) => NavigateToHome();
                page.BackRequested += (s, e) => GoBack();
                ShowPage(page);
            }
            else
            {
                if (addToHistory) backStack.Push(() => NavigateToResult(problemName, results, false));
                var page = new Failed(problemName, results.Results);
                page.BackRequested += (s, e) => GoBack();
                ShowPage(page);
            }
        }

        private void NavigateToUser(bool addToHistory = true)
        {
            if (addToHistory) backStack.Push(() => NavigateToUser(false));
            var page = new UserForm();
            page.HomeRequested += (s, e) => NavigateToHome();
            ShowPage(page);
        }

        private void GoBack()
        {
            if (backStack.Count > 0)
            {
                backStack.Pop();
                if (backStack.Count > 0)
                {
                    var prev = backStack.Peek();
                    prev();
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterCurrentPage();
        }
    }
}
