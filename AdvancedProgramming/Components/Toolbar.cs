using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public class Toolbar : Panel
    {
        public event EventHandler CloseRequested;

        private Button buttonMinimize;
        private Button buttonClose;
        private Label titleLabel;
        private Point dragOffset;

        public Toolbar(Control parent, string title)
        {
            Height = AppSizes.ToolbarHeight;
            Dock = DockStyle.Top;
            BackColor = AppColors.ToolbarBack;
            ForeColor = AppColors.Text;

            titleLabel = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = AppColors.Text,
                BackColor = Color.Transparent,
            };

            buttonMinimize = CreateWindowButton("\u2014");
            buttonMinimize.Click += MinimizeButton_Click;

            buttonClose = CreateWindowButton("\u2715");
            buttonClose.Click += CloseButton_Click;

            Controls.Add(titleLabel);
            Controls.Add(buttonMinimize);
            Controls.Add(buttonClose);

          Resize += (s, e) => RepositionControls();

            MouseDown += Toolbar_MouseDown;
            MouseMove += Toolbar_MouseMove;
            titleLabel.MouseDown += Toolbar_MouseDown;
            titleLabel.MouseMove += Toolbar_MouseMove;

            RepositionControls();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            Form form = FindForm();
            if (form != null)
                form.WindowState = FormWindowState.Minimized;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Toolbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                dragOffset = new Point(-e.X, -e.Y);
            // امممممم هون فكرنا عشان الفورم يزيح الزاوية الى تنزاح مش مكان مسكة الماوس بصير مرتب 

        }

        private void Toolbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Form form = FindForm();
                if (form != null)
                {
                    Point mousePos = Control.MousePosition;
                    mousePos.Offset(dragOffset.X, dragOffset.Y);
                    form.Location = mousePos;
                }
            }
        }

        private Button CreateWindowButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(38, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = AppColors.Text,
               
                TabStop = false,
                Font = new Font("Segoe UI", 12F),
            };
           btn.FlatAppearance.BorderSize = 0;
       
            return btn;
        }

        private void RepositionControls()
        {
            titleLabel.Location = new Point(24, (Height - titleLabel.Height) / 2);
            buttonMinimize.Location = new Point(Width - 84, 7);
            buttonClose.Location = new Point(Width - 42, 7);
        }
    }
}
