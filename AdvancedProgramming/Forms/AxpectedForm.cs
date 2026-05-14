
using System.Windows.Forms;
namespace AdvancedProgramming.Forms
{
    public class AxpectedForm : Form
    {
        private Panel panel1;
        private Label labelSumbb;
        private Button buttonHome;
        private Label label1;
        private PictureBox pictureBox1trusoliotion;

        public AxpectedForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSumbb = new System.Windows.Forms.Label();
            this.pictureBox1trusoliotion = new System.Windows.Forms.PictureBox();
            this.buttonHome = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1trusoliotion)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Size = new System.Drawing.Size(1882, 340);
            this.panel1.TabIndex = 0;
            this.panel1.Controls.Add(this.labelSumbb);
            this.panel1.Controls.Add(this.pictureBox1trusoliotion);
            // 
            // labelSumbb
            // 
            this.labelSumbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSumbb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelSumbb.Location = new System.Drawing.Point(246, 24);
            this.labelSumbb.Size = new System.Drawing.Size(529, 81);
            this.labelSumbb.TabIndex = 1;
            this.labelSumbb.Text = "-------Submmit✅🎉------";
            this.labelSumbb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1trusoliotion
            // 
            this.pictureBox1trusoliotion.BackgroundImage = global::AdvancedProgramming.Properties.Resources.pngtree_checkmark_or_tick_mark_approval_choice_selection_acceptance_right_correct_positive_png_image_38063721;
            this.pictureBox1trusoliotion.Location = new System.Drawing.Point(340, 59);
            this.pictureBox1trusoliotion.Size = new System.Drawing.Size(367, 281);
            this.pictureBox1trusoliotion.TabIndex = 2;
            this.pictureBox1trusoliotion.TabStop = false;
            this.pictureBox1trusoliotion.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttonHome
            // 
            this.buttonHome.Location = new System.Drawing.Point(356, 571);
            this.buttonHome.Size = new System.Drawing.Size(316, 84);
            this.buttonHome.TabIndex = 1;
            this.buttonHome.Text = "button1";
            this.buttonHome.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(26, 362);
            this.label1.Size = new System.Drawing.Size(646, 139);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code executed successfully!" +
                " Your program has run without any errors." +
                " All operatins were completed as expected.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AxpectedForm
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1882, 1055);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonHome);
            this.Controls.Add(this.panel1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1trusoliotion)).EndInit();
            this.ResumeLayout(false);
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
