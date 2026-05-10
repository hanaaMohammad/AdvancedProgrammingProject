using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AdvancedProgramming.Forms
{
    internal class FirstFrame : Form
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
        private Toolbar toolbar;

        public FirstFrame()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelTextNameApp = new System.Windows.Forms.Panel();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.PanleGridPage = new System.Windows.Forms.Panel();
            this.IconUser = new System.Windows.Forms.Button();
            this.GridLayoutPages = new System.Windows.Forms.TableLayoutPanel();
            this.Homepage = new System.Windows.Forms.Button();
            this.UsrePage = new System.Windows.Forms.Button();
            this.TotorialPage = new System.Windows.Forms.Button();
            this.Problelms = new System.Windows.Forms.Button();
            this.panelTextNameApp.SuspendLayout();
            this.PanleGridPage.SuspendLayout();
            this.GridLayoutPages.SuspendLayout();
            this.SuspendLayout();

            toolbar = new Toolbar(this, "Home Page");
            this.Controls.Add(toolbar);

            this.panelTextNameApp.Controls.Add(this.nameApp);
            this.panelTextNameApp.Location = new System.Drawing.Point(0, 40);
            this.panelTextNameApp.Name = "panelTextNameApp";
            this.panelTextNameApp.Size = new System.Drawing.Size(1053, 170);
            this.panelTextNameApp.TabIndex = 0;

            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.nameApp.Location = new System.Drawing.Point(60, 30);
            this.nameApp.Name = "nameApp";
            this.nameApp.ReadOnly = true;
            this.nameApp.Size = new System.Drawing.Size(500, 106);
            this.nameApp.TabIndex = 0;
            this.nameApp.Text = "MiniCamp Puzzle";

            this.PanleGridPage.Controls.Add(this.IconUser);
            this.PanleGridPage.Controls.Add(this.GridLayoutPages);
            this.PanleGridPage.Location = new System.Drawing.Point(40, 220);
            this.PanleGridPage.Name = "PanleGridPage";
            this.PanleGridPage.Size = new System.Drawing.Size(973, 500);
            this.PanleGridPage.TabIndex = 1;

            this.IconUser.FlatStyle = FlatStyle.Flat;
            this.IconUser.FlatAppearance.BorderSize = 0;
            this.IconUser.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.IconUser.Location = new System.Drawing.Point(850, 35);
            this.IconUser.Name = "IconUser";
            this.IconUser.Size = new System.Drawing.Size(90, 90);
            this.IconUser.TabIndex = 1;
            this.IconUser.Text = "👤";
            this.IconUser.UseVisualStyleBackColor = false;

            this.GridLayoutPages.ColumnCount = 4;
            this.GridLayoutPages.ColumnStyles.Clear();
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.GridLayoutPages.Controls.Add(this.Homepage, 0, 0);
            this.GridLayoutPages.Controls.Add(this.UsrePage, 1, 0);
            this.GridLayoutPages.Controls.Add(this.TotorialPage, 2, 0);
            this.GridLayoutPages.Controls.Add(this.Problelms, 3, 0);
            this.GridLayoutPages.Location = new System.Drawing.Point(14, 16);
            this.GridLayoutPages.Name = "GridLayoutPages";
            this.GridLayoutPages.RowCount = 1;
            this.GridLayoutPages.RowStyles.Clear();
            this.GridLayoutPages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GridLayoutPages.Size = new System.Drawing.Size(926, 79);
            this.GridLayoutPages.TabIndex = 0;

            this.Homepage.FlatStyle = FlatStyle.Flat;
            this.Homepage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.Homepage.Location = new System.Drawing.Point(3, 3);
            this.Homepage.Name = "Homepage";
            this.Homepage.Size = new System.Drawing.Size(225, 73);
            this.Homepage.TabIndex = 0;
            this.Homepage.Text = "Home";
            this.Homepage.UseVisualStyleBackColor = false;

            this.UsrePage.FlatStyle = FlatStyle.Flat;
            this.UsrePage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.UsrePage.Location = new System.Drawing.Point(234, 3);
            this.UsrePage.Name = "UsrePage";
            this.UsrePage.Size = new System.Drawing.Size(225, 73);
            this.UsrePage.TabIndex = 1;
            this.UsrePage.Text = "User Account";
            this.UsrePage.UseVisualStyleBackColor = false;

            this.TotorialPage.FlatStyle = FlatStyle.Flat;
            this.TotorialPage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TotorialPage.Location = new System.Drawing.Point(465, 3);
            this.TotorialPage.Name = "TotorialPage";
            this.TotorialPage.Size = new System.Drawing.Size(225, 73);
            this.TotorialPage.TabIndex = 3;
            this.TotorialPage.Text = "Tutorial";
            this.TotorialPage.UseVisualStyleBackColor = false;

            this.Problelms.FlatStyle = FlatStyle.Flat;
            this.Problelms.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Problelms.Location = new System.Drawing.Point(696, 3);
            this.Problelms.Name = "Problelms";
            this.Problelms.Size = new System.Drawing.Size(227, 73);
            this.Problelms.TabIndex = 2;
            this.Problelms.Text = "Problems";
            this.Problelms.UseVisualStyleBackColor = false;

            this.ClientSize = new System.Drawing.Size(1053, 829);
            this.Controls.Add(this.PanleGridPage);
            this.Controls.Add(this.panelTextNameApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FirstFrame";
            this.Text = "Home Page";

            Theme.Apply(this);
            this.panelTextNameApp.ResumeLayout(false);
            this.panelTextNameApp.PerformLayout();
            this.PanleGridPage.ResumeLayout(false);
            this.GridLayoutPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
        }

        private void InitializeLogInComponents()
        {
            this.Size = new Size(400, 440);
            this.Text= "Home Page";
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;


       







    }

        private void InitializeComponent()
        {
            this.panelTextNameApp = new System.Windows.Forms.Panel();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.PanleGridPage = new System.Windows.Forms.Panel();
            this.IconUser = new System.Windows.Forms.Button();
            this.GridLayoutPages = new System.Windows.Forms.TableLayoutPanel();
            this.Homepage = new System.Windows.Forms.Button();
            this.UsrePage = new System.Windows.Forms.Button();
            this.TotorialPage = new System.Windows.Forms.Button();
            this.Problelms = new System.Windows.Forms.Button();
            this.panelTextNameApp.SuspendLayout();
            this.PanleGridPage.SuspendLayout();
            this.GridLayoutPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTextNameApp
            // 
            this.panelTextNameApp.BackColor = System.Drawing.Color.Purple;
            this.panelTextNameApp.Controls.Add(this.nameApp);
            this.panelTextNameApp.Location = new System.Drawing.Point(27, -1);
            this.panelTextNameApp.Name = "panelTextNameApp";
            this.panelTextNameApp.Size = new System.Drawing.Size(933, 240);
            this.panelTextNameApp.TabIndex = 0;
            // 
            // nameApp
            // 
            this.nameApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Segoe Print", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameApp.Location = new System.Drawing.Point(3, 35);
            this.nameApp.Name = "nameApp";
            this.nameApp.ReadOnly = true;
            this.nameApp.Size = new System.Drawing.Size(936, 170);
            this.nameApp.TabIndex = 0;
            this.nameApp.Text = "MiniCamp Puzzle";
            // 
            // PanleGridPage
            // 
            this.PanleGridPage.Controls.Add(this.IconUser);
            this.PanleGridPage.Controls.Add(this.GridLayoutPages);
            this.PanleGridPage.Location = new System.Drawing.Point(20, 258);
            this.PanleGridPage.Name = "PanleGridPage";
            this.PanleGridPage.Size = new System.Drawing.Size(970, 515);
            this.PanleGridPage.TabIndex = 1;
            // 
            // IconUser
            // 
            this.IconUser.Location = new System.Drawing.Point(405, 275);
            this.IconUser.Name = "IconUser";
            this.IconUser.Size = new System.Drawing.Size(135, 134);
            this.IconUser.TabIndex = 1;
            this.IconUser.Text = "Icon ";
            this.IconUser.UseVisualStyleBackColor = true;
            // 
            // GridLayoutPages
            // 
            this.GridLayoutPages.ColumnCount = 4;
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.82609F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.17391F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
            this.GridLayoutPages.Controls.Add(this.Homepage, 0, 0);
            this.GridLayoutPages.Controls.Add(this.UsrePage, 1, 0);
            this.GridLayoutPages.Controls.Add(this.TotorialPage, 2, 0);
            this.GridLayoutPages.Controls.Add(this.Problelms, 3, 0);
            this.GridLayoutPages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GridLayoutPages.Location = new System.Drawing.Point(14, 16);
            this.GridLayoutPages.Name = "GridLayoutPages";
            this.GridLayoutPages.RowCount = 1;
            this.GridLayoutPages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GridLayoutPages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GridLayoutPages.Size = new System.Drawing.Size(926, 79);
            this.GridLayoutPages.TabIndex = 0;
            // 
            // Homepage
            // 
            this.Homepage.Font = new System.Drawing.Font("Microsoft YaHei", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Homepage.Location = new System.Drawing.Point(3, 3);
            this.Homepage.Name = "Homepage";
            this.Homepage.Size = new System.Drawing.Size(209, 73);
            this.Homepage.TabIndex = 0;
            this.Homepage.Text = "Home";
            this.Homepage.UseVisualStyleBackColor = true;
            this.Homepage.Click += new System.EventHandler(this.button1_Click);
            // 
            // UsrePage
            // 
            this.UsrePage.Location = new System.Drawing.Point(218, 3);
            this.UsrePage.Name = "UsrePage";
            this.UsrePage.Size = new System.Drawing.Size(228, 73);
            this.UsrePage.TabIndex = 1;
            this.UsrePage.Text = "User Account";
            this.UsrePage.UseVisualStyleBackColor = true;
            // 
            // TotorialPage
            // 
            this.TotorialPage.Location = new System.Drawing.Point(452, 3);
            this.TotorialPage.Name = "TotorialPage";
            this.TotorialPage.Size = new System.Drawing.Size(213, 73);
            this.TotorialPage.TabIndex = 3;
            this.TotorialPage.Text = "Toturial";
            this.TotorialPage.UseVisualStyleBackColor = true;
            this.TotorialPage.Click += new System.EventHandler(this.button4_Click);
            // 
            // Problelms
            // 
            this.Problelms.Location = new System.Drawing.Point(671, 3);
            this.Problelms.Name = "Problelms";
            this.Problelms.Size = new System.Drawing.Size(252, 73);
            this.Problelms.TabIndex = 2;
            this.Problelms.Text = "Problems";
            this.Problelms.UseVisualStyleBackColor = true;
            // 
            // FirstFrame
            // 
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1053, 829);
            this.Controls.Add(this.PanleGridPage);
            this.Controls.Add(this.panelTextNameApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FirstFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Page ";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.panelTextNameApp.ResumeLayout(false);
            this.panelTextNameApp.PerformLayout();
            this.PanleGridPage.ResumeLayout(false);
            this.GridLayoutPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
