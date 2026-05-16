using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    // Base form: fixed size, dark background, simple navigation helpers.
    public class AppForm : Form
    {
        // Set once in Program.cs so hiding a form does not close the app.
        public static ApplicationContext AppContext { get; set; }

        public AppForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(AppSizes.FormWidth, AppSizes.FormHeight);
            BackColor = AppColors.PageBack;
        }

        // Open another screen; when it closes, show this one again.
        protected void ShowOtherForm(Form nextForm)
        {
            if (AppContext != null)
                AppContext.MainForm = nextForm;

            Hide();
            nextForm.FormClosed += ChildFormClosed;
            nextForm.Show();
        }

        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            Form child = (Form)sender;
            child.FormClosed -= ChildFormClosed;

            if (IsDisposed)
                return;

            // After login, another form became main — do not bring this form back.
            if (AppContext != null && AppContext.MainForm != null &&
                AppContext.MainForm != this && AppContext.MainForm != child)
                return;

            if (AppContext != null)
                AppContext.MainForm = this;

            Show();
        }

        // Close every open window and show one main screen (after login, etc.).
        protected void ShowAsMainForm(Form mainForm)
        {
            if (AppContext != null)
                AppContext.MainForm = mainForm;

            foreach (Form open in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (open != mainForm && !open.IsDisposed)
                    open.Close();
            }

            mainForm.Show();
        }

        protected Button MakeNavButton(string text, int x, EventHandler click)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, AppSizes.NavTop),
                Size = new Size(80, AppSizes.NavHeight),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.Transparent,
                ForeColor = AppColors.Text,
                Cursor = Cursors.Hand,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += click;
            return btn;
        }
    }
}
