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
            this.panelTextNameApp = new System.Windows.Forms.Panel();
            this.IconUser = new System.Windows.Forms.Button();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.PanleGridPage = new System.Windows.Forms.Panel();
            this.GridLayoutPages = new System.Windows.Forms.TableLayoutPanel();
            this.Problelms = new System.Windows.Forms.Button();
            this.Homepage = new System.Windows.Forms.Button();
            this.UsrePage = new System.Windows.Forms.Button();
            this.TotorialPage = new System.Windows.Forms.Button();
            this.panelTextNameApp.SuspendLayout();
            this.GridLayoutPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTextNameApp
            // 
            this.panelTextNameApp.BackColor = System.Drawing.Color.Purple;
            this.panelTextNameApp.Controls.Add(this.IconUser);
            this.panelTextNameApp.Controls.Add(this.nameApp);
            this.panelTextNameApp.Location = new System.Drawing.Point(13, 0);
            this.panelTextNameApp.Name = "panelTextNameApp";
            this.panelTextNameApp.Size = new System.Drawing.Size(1016, 215);
            this.panelTextNameApp.TabIndex = 0;
            // 
            // IconUser
            // 
            this.IconUser.Location = new System.Drawing.Point(842, 20);
            this.IconUser.Name = "IconUser";
            this.IconUser.Size = new System.Drawing.Size(135, 134);
            this.IconUser.TabIndex = 1;
            this.IconUser.Text = "Icon ";
            this.IconUser.UseVisualStyleBackColor = true;
            this.IconUser.Click += new System.EventHandler(this.IconUser_Click);
            // 
            // nameApp
            // 
            this.nameApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Segoe Print", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameApp.Location = new System.Drawing.Point(3, 12);
            this.nameApp.Name = "nameApp";
            this.nameApp.ReadOnly = true;
            this.nameApp.Size = new System.Drawing.Size(1148, 142);
            this.nameApp.TabIndex = 0;
            this.nameApp.Text = "MiniCamp Puzzle";
            this.nameApp.TextChanged += new System.EventHandler(this.nameApp_TextChanged);
            // 
            // PanleGridPage
            // 
            this.PanleGridPage.Location = new System.Drawing.Point(43, 323);
            this.PanleGridPage.Name = "PanleGridPage";
            this.PanleGridPage.Size = new System.Drawing.Size(947, 450);
            this.PanleGridPage.TabIndex = 1;
            // 
            // GridLayoutPages
            // 
            this.GridLayoutPages.ColumnCount = 4;
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.82609F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.17391F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.GridLayoutPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
            this.GridLayoutPages.Controls.Add(this.Problelms, 3, 0);
            this.GridLayoutPages.Controls.Add(this.Homepage, 0, 0);
            this.GridLayoutPages.Controls.Add(this.UsrePage, 1, 0);
            this.GridLayoutPages.Controls.Add(this.TotorialPage, 2, 0);
            this.GridLayoutPages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GridLayoutPages.Location = new System.Drawing.Point(52, 221);
            this.GridLayoutPages.Name = "GridLayoutPages";
            this.GridLayoutPages.RowCount = 1;
            this.GridLayoutPages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GridLayoutPages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GridLayoutPages.Size = new System.Drawing.Size(926, 79);
            this.GridLayoutPages.TabIndex = 0;
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
            // HomeFarme
            // 
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1053, 829);
            this.Controls.Add(this.panelTextNameApp);
            this.Controls.Add(this.PanleGridPage);
            this.Controls.Add(this.GridLayoutPages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "HomeFarme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Page ";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.panelTextNameApp.ResumeLayout(false);
            this.panelTextNameApp.PerformLayout();
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

        private void nameApp_TextChanged(object sender, EventArgs e)
        {

        }

        private void IconUser_Click(object sender, EventArgs e)
        {

        }
    }
}
