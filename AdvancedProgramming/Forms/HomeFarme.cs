using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class HomeFarme : Form
    {
        private Panel panelTextNameApp;
        private TextBox nameApp;
        private Panel PanleGridPage;
        private TableLayoutPanel GridLayoutPages;
        private Button Homepage;
        private Button UsrePage;
        private Button Problelms;
        private Button IconUser;
        private Button TotorialPage;

        public HomeFarme()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Home Page";
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1053, 829);
            this.BackColor = Color.FromArgb(15, 15, 25);

            Color accent = Color.FromArgb(108, 99, 255);
            Color accentHover = Color.FromArgb(130, 120, 255);
            Color surface = Color.FromArgb(28, 28, 44);
            Color card = Color.FromArgb(38, 38, 56);
            Color textWhite = Color.White;
            Color textMuted = Color.FromArgb(160, 160, 180);

            this.panelTextNameApp = new Panel();
            this.IconUser = new Button();
            this.nameApp = new TextBox();
            this.PanleGridPage = new Panel();
            this.GridLayoutPages = new TableLayoutPanel();
            this.Problelms = new Button();
            this.Homepage = new Button();
            this.UsrePage = new Button();
            this.TotorialPage = new Button();
            this.panelTextNameApp.SuspendLayout();
            this.GridLayoutPages.SuspendLayout();
            this.SuspendLayout();

            this.panelTextNameApp.BackColor = surface;
            this.panelTextNameApp.Controls.Add(this.IconUser);
            this.panelTextNameApp.Controls.Add(this.nameApp);
            this.panelTextNameApp.Location = new Point(0, 0);
            this.panelTextNameApp.Name = "panelTextNameApp";
            this.panelTextNameApp.Size = new Size(1053, 170);
            this.panelTextNameApp.TabIndex = 0;

            this.IconUser.FlatStyle = FlatStyle.Flat;
            this.IconUser.FlatAppearance.BorderSize = 0;
            this.IconUser.BackColor = accent;
            this.IconUser.ForeColor = Color.White;
            this.IconUser.Font = new Font("Segoe UI", 24F);
            this.IconUser.Location = new Point(930, 35);
            this.IconUser.Name = "IconUser";
            this.IconUser.Size = new Size(90, 90);
            this.IconUser.TabIndex = 1;
            this.IconUser.Text = "👤";
            this.IconUser.UseVisualStyleBackColor = false;
            this.IconUser.Click += (s, e) =>
            {
                var usre = new UsreForm();
                usre.Show();
                this.Hide();
            };

            this.nameApp.BackColor = surface;
            this.nameApp.BorderStyle = BorderStyle.None;
            this.nameApp.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            this.nameApp.ForeColor = textWhite;
            this.nameApp.Location = new Point(40, 40);
            this.nameApp.Name = "nameApp";
            this.nameApp.ReadOnly = true;
            this.nameApp.Size = new Size(480, 80);
            this.nameApp.TabIndex = 0;
            this.nameApp.Text = "MiniCamp Puzzle";

            this.GridLayoutPages.ColumnCount = 4;
            this.GridLayoutPages.ColumnStyles.Clear();
            this.GridLayoutPages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.GridLayoutPages.Controls.Add(this.Homepage, 0, 0);
            this.GridLayoutPages.Controls.Add(this.UsrePage, 1, 0);
            this.GridLayoutPages.Controls.Add(this.TotorialPage, 2, 0);
            this.GridLayoutPages.Controls.Add(this.Problelms, 3, 0);
            this.GridLayoutPages.Location = new Point(40, 185);
            this.GridLayoutPages.Name = "GridLayoutPages";
            this.GridLayoutPages.RowCount = 1;
            this.GridLayoutPages.RowStyles.Clear();
            this.GridLayoutPages.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.GridLayoutPages.Size = new Size(973, 56);
            this.GridLayoutPages.TabIndex = 0;

            this.Homepage.FlatStyle = FlatStyle.Flat;
            this.Homepage.FlatAppearance.BorderSize = 0;
            this.Homepage.FlatAppearance.MouseOverBackColor = accentHover;
            this.Homepage.BackColor = accent;
            this.Homepage.ForeColor = Color.White;
            this.Homepage.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.Homepage.Location = new Point(3, 3);
            this.Homepage.Name = "Homepage";
            this.Homepage.Size = new Size(237, 50);
            this.Homepage.TabIndex = 0;
            this.Homepage.Text = "Home";
            this.Homepage.UseVisualStyleBackColor = false;

            this.UsrePage.FlatStyle = FlatStyle.Flat;
            this.UsrePage.FlatAppearance.BorderSize = 0;
            this.UsrePage.FlatAppearance.MouseOverBackColor = accentHover;
            this.UsrePage.BackColor = card;
            this.UsrePage.ForeColor = textMuted;
            this.UsrePage.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.UsrePage.Location = new Point(246, 3);
            this.UsrePage.Name = "UsrePage";
            this.UsrePage.Size = new Size(237, 50);
            this.UsrePage.TabIndex = 1;
            this.UsrePage.Text = "User Account";
            this.UsrePage.UseVisualStyleBackColor = false;
            this.UsrePage.Click += (s, e) =>
            {
                var usre = new UsreForm();
                usre.Show();
                this.Hide();
            };

            this.TotorialPage.FlatStyle = FlatStyle.Flat;
            this.TotorialPage.FlatAppearance.BorderSize = 0;
            this.TotorialPage.FlatAppearance.MouseOverBackColor = accentHover;
            this.TotorialPage.BackColor = card;
            this.TotorialPage.ForeColor = textMuted;
            this.TotorialPage.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.TotorialPage.Location = new Point(489, 3);
            this.TotorialPage.Name = "TotorialPage";
            this.TotorialPage.Size = new Size(237, 50);
            this.TotorialPage.TabIndex = 2;
            this.TotorialPage.Text = "Tutorial";
            this.TotorialPage.UseVisualStyleBackColor = false;

            this.Problelms.FlatStyle = FlatStyle.Flat;
            this.Problelms.FlatAppearance.BorderSize = 0;
            this.Problelms.FlatAppearance.MouseOverBackColor = accentHover;
            this.Problelms.BackColor = card;
            this.Problelms.ForeColor = textMuted;
            this.Problelms.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.Problelms.Location = new Point(732, 3);
            this.Problelms.Name = "Problelms";
            this.Problelms.Size = new Size(238, 50);
            this.Problelms.TabIndex = 3;
            this.Problelms.Text = "Problems";
            this.Problelms.UseVisualStyleBackColor = false;
            this.Problelms.Click += (s, e) =>
            {
                var problem = new ProblemForm();
                problem.Show();
                this.Hide();
            };

            this.PanleGridPage.BackColor = surface;
            this.PanleGridPage.Location = new Point(40, 255);
            this.PanleGridPage.Name = "PanleGridPage";
            this.PanleGridPage.Size = new Size(973, 540);
            this.PanleGridPage.TabIndex = 1;

            this.Controls.Add(this.panelTextNameApp);
            this.Controls.Add(this.GridLayoutPages);
            this.Controls.Add(this.PanleGridPage);

            this.panelTextNameApp.ResumeLayout(false);
            this.panelTextNameApp.PerformLayout();
            this.GridLayoutPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
