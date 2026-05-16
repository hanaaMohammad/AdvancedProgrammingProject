using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AdvancedProgramming.Components;

namespace AdvancedProgramming.Forms
{
    public class AppForm : Form
    {
        internal static ApplicationContext AppContext { get; set; }

        public bool AnimateOnShow { get; set; } = true;

        protected AppForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(DesignTokens.FormWidth, DesignTokens.FormHeight);
            MinimumSize = new Size(DesignTokens.MinFormWidth, DesignTokens.MinFormHeight);
            KeyPreview = true;
            CatalogUi.EnableDoubleBuffer(this);
            DoubleBuffered = true;
            Theme.Apply(this);
        }

        /// <summary>Opens another screen and returns here when it closes (Back).</summary>
        protected void ShowScreen(Form next)
        {
            var owner = this;
            next.FormClosed += (_, __) =>
            {
                if (!owner.IsDisposed)
                {
                    AppContext.MainForm = owner;
                    owner.Show();
                }
            };

            AppContext.MainForm = next;
            owner.Hide();
            next.Show();
        }

        /// <summary>Closes every open screen and shows one new screen (login success, Home button).</summary>
        protected static void ShowOnly(Form next)
        {
            AppContext.MainForm = next;

            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (form != next)
                    form.Close();
            }

            if (!next.Visible)
                next.Show();
        }

        protected void GoBack() => Close();

        protected void GoStartup() => ShowOnly(new StartupForm { AnimateOnShow = false });

        protected void GoAppHome() => ShowOnly(new LevelProblemForm());

        protected void AfterLogin() => ShowOnly(new LevelProblemForm());

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (AnimateOnShow)
                PageTransition.AnimateIn(this);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                GoBack();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
