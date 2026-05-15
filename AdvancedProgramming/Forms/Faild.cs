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
        private Toolbar toolbar;
        private Button backButton;

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

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            backButton = new Button
            {
                Text = "Back",
                Location = new Point(41, 600),
                Size = new Size(90, 40),
                FlatStyle = FlatStyle.Flat,
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += (s, e) => this.Close();
            this.Controls.Add(backButton);

            // GameOver
            this.GameOver.Location = new System.Drawing.Point(41, 34);
            this.GameOver.Name = "GameOver";
            this.GameOver.Size = new System.Drawing.Size(523, 258);
            this.GameOver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GameOver.TabIndex = 0;
            this.GameOver.TabStop = false;
            this.GameOver.Click += new System.EventHandler(this.GameOver_Click);

            // trackBar2
            this.trackBar2.Location = new System.Drawing.Point(26, 333);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(590, 69);
            this.trackBar2.TabIndex = 1;

            // Faild
            this.ClientSize = new System.Drawing.Size(863, 802);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.GameOver);
            this.Name = "Faild";
            ((System.ComponentModel.ISupportInitialize)(this.GameOver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            Theme.Apply(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            toolbar?.UpdateTheme();
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
                    BackColor = Theme.Current.ControlBackColor,
                    BorderStyle = BorderStyle.FixedSingle
                };TextBox textCase = new TextBox()
                {
                    Multiline = true,
                    ReadOnly = true,
                        BorderStyle = BorderStyle.FixedSingle,
                        Width=560,
                    Height = 70,
                    Left = 15,
                    Top = 10,
                    Font = new Font("Consolas", 11),
                    BackColor = Theme.Current.InputBackColor,
                    ForeColor = Theme.Current.TextColor,
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
