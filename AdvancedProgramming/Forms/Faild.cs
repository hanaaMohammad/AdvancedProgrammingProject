using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class Faild:Form
    {
        private PictureBox GameOver;
        private ProblemLoadReadJs load;
        private Timer timer2;
        private Problem problem;
        private List<TestCase> testCases;
        private List<Panel> panels= new List<Panel>();
        private List<int> targetTops = new List<int>();
        private int currentTestCaseInde = 0;
        private Toolbar toolbar;
        private Button backButton;
        private List<CodeRunnerTestResult> testResults;
        private Label labelHeader;

        public Faild(string name)
        {
            try
            {
                InitializeComponent();
                InitalProblem(name);
                DisplayTestCases();
            }
            catch { }
        }

        public Faild(string name, List<CodeRunnerTestResult> results)
        {
            try
            {
                testResults = results;
                InitializeComponent();
                InitalProblem(name);
                DisplayTestCases();
            }
            catch { }
        }

        private void InitializeComponent()
        {
            this.GameOver = new PictureBox();
            this.timer2 = new Timer();
            this.SuspendLayout();

            this.AutoScroll = true;

            try
            {
                string bgPath = Path.Combine(Application.StartupPath, "Image", "BackGround.jpg");
                if (!File.Exists(bgPath))
                    bgPath = Path.GetFullPath(@"..\..\..\Image\BackGround.jpg");
                if (File.Exists(bgPath))
                {
                    this.BackgroundImage = Image.FromFile(bgPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            labelHeader = new Label
            {
                Text = "Some tests failed!",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.Red,
                Size = new Size(400, 45),
                Location = new Point(225, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            backButton = new Button
            {
                Text = "Back",
                Location = new Point(20, 70),
                Size = new Size(90, 40),
                FlatStyle = FlatStyle.Flat,
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += (s, e) => this.Close();

            try
            {
                string imgPath = Path.Combine(Application.StartupPath, "Image", "GameOverr.png");
                if (!File.Exists(imgPath))
                    imgPath = Path.GetFullPath(@"..\..\..\Image\GameOverr.png");
                if (File.Exists(imgPath))
                {
                    GameOver.Image = Image.FromFile(imgPath);
                    GameOver.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                    GameOver.Visible = false;
            }
            catch
            {
                GameOver.Visible = false;
            }

            if (GameOver.Visible)
            {
                GameOver.Location = new Point(185, 400);
                GameOver.Size = new Size(480, 220);
                GameOver.BackColor = Color.Transparent;
            }

            this.ClientSize = new Size(850, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(labelHeader);
            this.Controls.Add(GameOver);
            this.Controls.Add(backButton);
            this.Name = "Faild";

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
            problem = load.getProblemByName(name);
            testCases = problem?.TestCase ?? new List<TestCase>();
        }

        private void DisplayTestCases()
        {
            if (testCases == null || testCases.Count == 0)
                return;

            int startY = GameOver.Visible ? 350 : 130;

            for (int i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                int targetTop = startY + i * 120;

                Panel panel = new Panel()
                {
                    Width = 620,
                    Height = 110,
                    Left = 115,
                    Top = -150,
                    BackColor = Theme.Current.ControlBackColor,
                    BorderStyle = BorderStyle.FixedSingle
                };

                string displayText = testCase.ToString();
                Color textColor = Theme.Current.TextColor;
                if (testResults != null && i < testResults.Count)
                {
                    var result = testResults[i];
                    if (result.Passed == true)
                    {
                        displayText = displayText + "\nPASSED";
                        textColor = Color.Green;
                    }
                    else
                    {
                        displayText = displayText + "\nFAILED\nExpected: " + result.ExpectedOutput + "\nActual: " + result.ActualOutput;
                        textColor = Color.Red;
                    }
                }

                TextBox textCase = new TextBox()
                {
                    Multiline = true,
                    ReadOnly = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Width = 590,
                    Height = 90,
                    Left = 12,
                    Top = 8,
                    Font = new Font("Consolas", 11),
                    BackColor = Theme.Current.InputBackColor,
                    ForeColor = textColor,
                    Text = displayText,
                };

                panel.Controls.Add(textCase);
                this.Controls.Add(panel);
                panels.Add(panel);
                targetTops.Add(targetTop);
            }

            timer2.Interval = 18;
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

            Panel panell = panels[currentTestCaseInde];
            int target = targetTops[currentTestCaseInde];

            if (panell.Top < target)
                panell.Top += 8;
            else
                currentTestCaseInde++;
        }
    }
}
