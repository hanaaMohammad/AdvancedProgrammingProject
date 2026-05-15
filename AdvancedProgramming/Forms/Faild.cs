using AdvancedProgramming.ProblemClasses;
using AdvancedProgramming.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AdvancedProgramming.Forms
{
    public class Failed : UserControl
    {
        public event EventHandler BackRequested;

        private PictureBox GameOver;
        private ProblemLoadReadJs load;
        private Timer timer2;
        private Problem problem;
        private List<TestCase> testCases;
        private List<Panel> panels = new List<Panel>();
        private List<int> targetTops = new List<int>();
        private int currentTestCaseInde = 0;
        private Toolbar toolbar;
        private Button backButton;
        private List<CodeRunnerTestResult> testResults;
        private Label labelHeader;

        public Failed(string name) : this(name, null) { }

        public Failed(string name, List<CodeRunnerTestResult> results)
        {
            this.Size = new Size(1100, 800);
            testResults = results;
            try
            {
                InitializeComponent();
                InitalProblem(name);
                DisplayTestCases();
                toolbar.CloseRequested += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            }
            catch { }
        }

        private void InitializeComponent()
        {
            this.GameOver = new PictureBox();
            this.timer2 = new Timer();
            this.SuspendLayout();

            this.AutoScroll = true;

            toolbar = new Toolbar(this, "MiniCamp Puzzle");
            this.Controls.Add(toolbar);

            int cx = this.Width / 2;
            labelHeader = new Label
            {
                Text = "Some tests failed!",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.Red,
                Size = new Size(400, 45),
                Location = new Point(cx - 200, 70),
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
            backButton.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

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
                GameOver.Location = new Point(cx - 240, 420);
                GameOver.Size = new Size(480, 220);
                GameOver.BackColor = Color.Transparent;
            }

            this.Controls.Add(labelHeader);
            this.Controls.Add(GameOver);
            this.Controls.Add(backButton);
            this.Name = "Failed";

            this.ResumeLayout(false);
            this.PerformLayout();

            Theme.Apply(this);
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

            int cx = this.Width / 2;
            int startY = GameOver.Visible ? 350 : 130;

            for (int i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                int targetTop = startY + i * 120;

                Panel panel = new Panel()
                {
                    Width = 620,
                    Height = 110,
                    Left = cx - 310,
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
