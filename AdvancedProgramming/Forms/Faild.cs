using System.Windows.Forms;
using System.Drawing;
using AdvancedProgramming.ProblemClasses;
using System.Collections.Generic;
using System;

namespace AdvancedProgramming.Forms
{
    public class Faild:Form
    {
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private TrackBar trackBar1;
        private PictureBox GameOver;
        private ProblemLoadReadJs load;
        private Timer timer2;
        private TrackBar trackBar2;
        private Problem problem;
        private List<TestCase> testCases;
        private List<Panel> panels= new List<Panel>();
        private int currentTestCaseInde = 0;
        public Faild(string name)
        {
            
            InitializeComponent();
            InitalProblem(name);


        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameOver = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.GameOver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // GameOver
            // 
            this.GameOver.Location = new System.Drawing.Point(41, 34);
            this.GameOver.Name = "GameOver";
            this.GameOver.Size = new System.Drawing.Size(523, 258);
            this.GameOver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GameOver.TabIndex = 0;
            this.GameOver.TabStop = false;
            this.GameOver.Click += new System.EventHandler(this.GameOver_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(26, 333);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(590, 69);
            this.trackBar2.TabIndex = 1;
            // 
            // Faild
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(863, 802);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.GameOver);
            this.Name = "Faild";
            ((System.ComponentModel.ISupportInitialize)(this.GameOver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void InitalProblem(string name)

        {
            load = new ProblemLoadReadJs();
            this.problem = load.getProblemByName(name);
            this.testCases = problem.TestCase;

        }
        private void DisplayTestCases()
        {
            int startX = 120;

            foreach (var testCase in testCases) {
                Panel panel = new Panel()
                {
                    Width = 600,
                    Height = 90,
                    Left = startX,
                    Top = -150,
                    BackColor = Color.FromArgb(20, 20, 20),
                    BorderStyle = BorderStyle.Fixed3D
                };TextBox textCase = new TextBox()
                {
                    Multiline = true,
                    ReadOnly = true,
                        BorderStyle = BorderStyle.Fixed3D,
                        Width=560,
                    Height = 70,
                    Left = 15,
                    Top = 10,
                    Font = new Font("Consolas", 11),
                    BackColor = Color.FromArgb(20, 20, 20),
                    ForeColor = Color.Lime,
                    Text=testCase.ToString(),


                };
                panel.Controls.Add(textCase);
                this.Controls.Add(panel);
                panels.Add(panel);

              

            }
            timer2.Interval = 10;
            timer2.Tick += Animat;
            timer2.Start();
        }

        private void Animat(Object sender, EventArgs e)
        {
            if (currentTestCaseInde >= panels.Count)
            {
                timer2.Stop();
                return;




            }
            
         
            Panel panell= panels[currentTestCaseInde];
            if (panell.Top < 100)
                panell.Top += 5;

            else
                currentTestCaseInde++;



        }

        private void GameOver_Click(object sender, System.EventArgs e)
        {

        }
    }
}
