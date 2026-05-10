using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            this.panelTextNameApp = new System.Windows.Forms.Panel();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.IconUser = new System.Windows.Forms.Button();
            this.GridLayoutPages = new System.Windows.Forms.TableLayoutPanel();
            this.Homepage = new System.Windows.Forms.Button();
            this.UsrePage = new System.Windows.Forms.Button();
            this.TotorialPage = new System.Windows.Forms.Button();
            this.Problelms = new System.Windows.Forms.Button();
            this.PanleGridPage = new System.Windows.Forms.Panel();

            this.panelTextNameApp.SuspendLayout();
            this.GridLayoutPages.SuspendLayout();
            this.SuspendLayout();

            // panelTextNameApp
            this.panelTextNameApp.BackColor = surface;
            this.panelTextNameApp.Controls.Add(this.IconUser);
            this.panelTextNameApp.Controls.Add(this.nameApp);
            this.panelTextNameApp.Location = new System.Drawing.Point(0, 0);
            this.panelTextNameApp.Name = "panelTextNameApp";
            this.panelTextNameApp.Size = new System.Drawing.Size(1053, 170);
            this.panelTextNameApp.TabIndex = 0;

            // nameApp
            this.nameApp.BackColor = surface;
            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.nameApp.ForeColor = textWhite;
            this.nameApp.Location = new System.Drawing.Point(40, 40);
            this.nameApp.Name = "nameApp";
            this.nameApp.ReadOnly = true;
            this.nameApp.Size = new System.Drawing.Size(480, 80);
            this.nameApp.TabIndex = 0;
            this.nameApp.Text = "MiniCamp Puzzle";
            this.nameApp.TextChanged += new System.EventHandler(this.nameApp_TextChanged);

            // IconUser
            this.IconUser.FlatStyle = FlatStyle.Flat;
            this.IconUser.FlatAppearance.BorderSize = 0;
            this.IconUser.BackColor = accent;
            this.IconUser.ForeColor = Color.White;
            this.IconUser.Font = new Font("Segoe UI", 24F);
            this.IconUser.Location = new System.Drawing.Point(930, 35);
            this.IconUser.Name = "IconUser";
            this.IconUser.Size = new System.Drawing.Size(90, 90);
            this.IconUser.TabIndex = 1;
            this.IconUser.Text = "👤";
            this.IconUser.UseVisualStyleBackColor = false;
            this.IconUser.Click += (s, e) =>
            {
                var usre = new Usre();
                usre.Show();
                this.Hide();
            };

            // GridLayoutPages
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
            this.GridLayoutPages.Location = new System.Drawing.Point(40, 185);
            this.GridLayoutPages.Name = "GridLayoutPages";
            this.GridLayoutPages.RowCount = 1;
            this.GridLayoutPages.RowStyles.Clear();
            this.GridLayoutPages.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.GridLayoutPages.Size = new System.Drawing.Size(973, 56);
            this.GridLayoutPages.TabIndex = 0;

            // Homepage
            this.Homepage.FlatStyle = FlatStyle.Flat;
            this.Homepage.FlatAppearance.BorderSize = 0;
            this.Homepage.FlatAppearance.MouseOverBackColor = accentHover;
            this.Homepage.BackColor = accent;
            this.Homepage.ForeColor = Color.White;
            this.Homepage.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.Homepage.Location = new System.Drawing.Point(3, 3);
            this.Homepage.Name = "Homepage";
            this.Homepage.Size = new System.Drawing.Size(237, 50);
            this.Homepage.TabIndex = 0;
            this.Homepage.Text = "Home";
            this.Homepage.UseVisualStyleBackColor = false;
            this.Homepage.Click += new System.EventHandler(this.button1_Click);

            // UsrePage
            this.UsrePage.FlatStyle = FlatStyle.Flat;
            this.UsrePage.FlatAppearance.BorderSize = 0;
            this.UsrePage.FlatAppearance.MouseOverBackColor = accentHover;
            this.UsrePage.BackColor = card;
            this.UsrePage.ForeColor = textMuted;
            this.UsrePage.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.UsrePage.Location = new System.Drawing.Point(246, 3);
            this.UsrePage.Name = "UsrePage";
            this.UsrePage.Size = new System.Drawing.Size(237, 50);
            this.UsrePage.TabIndex = 1;
            this.UsrePage.Text = "User Account";
            this.UsrePage.UseVisualStyleBackColor = false;

            // TotorialPage
            this.TotorialPage.FlatStyle = FlatStyle.Flat;
            this.TotorialPage.FlatAppearance.BorderSize = 0;
            this.TotorialPage.FlatAppearance.MouseOverBackColor = accentHover;
            this.TotorialPage.BackColor = card;
            this.TotorialPage.ForeColor = textMuted;
            this.TotorialPage.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.TotorialPage.Location = new System.Drawing.Point(489, 3);
            this.TotorialPage.Name = "TotorialPage";
            this.TotorialPage.Size = new System.Drawing.Size(237, 50);
            this.TotorialPage.TabIndex = 2;
            this.TotorialPage.Text = "Tutorial";
            this.TotorialPage.UseVisualStyleBackColor = false;
            this.TotorialPage.Click += new System.EventHandler(this.button4_Click);

            // Problelms
            this.Problelms.FlatStyle = FlatStyle.Flat;
            this.Problelms.FlatAppearance.BorderSize = 0;
            this.Problelms.FlatAppearance.MouseOverBackColor = accentHover;
            this.Problelms.BackColor = card;
            this.Problelms.ForeColor = textMuted;
            this.Problelms.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.Problelms.Location = new System.Drawing.Point(732, 3);
            this.Problelms.Name = "Problelms";
            this.Problelms.Size = new System.Drawing.Size(238, 50);
            this.Problelms.TabIndex = 3;
            this.Problelms.Text = "Problems";
            this.Problelms.UseVisualStyleBackColor = false;

            // PanleGridPage
            this.PanleGridPage.BackColor = surface;
            this.PanleGridPage.Location = new System.Drawing.Point(40, 255);
            this.PanleGridPage.Name = "PanleGridPage";
            this.PanleGridPage.Size = new System.Drawing.Size(973, 540);
            this.PanleGridPage.TabIndex = 1;

            // HomeFarme
            this.Controls.Add(this.panelTextNameApp);
            this.Controls.Add(this.GridLayoutPages);
            this.Controls.Add(this.PanleGridPage);

            this.panelTextNameApp.ResumeLayout(false);
            this.panelTextNameApp.PerformLayout();
            this.GridLayoutPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void nameApp_TextChanged(object sender, EventArgs e) { }
    }
}
