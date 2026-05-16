using System;
using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming.Components;

namespace AdvancedProgramming
{
    public class LoadingOverlay : Panel
    {
        private static readonly string[] SpinnerFrames = { "\u25d0", "\u25d3", "\u25d1", "\u25d2" };

        private readonly Label spinnerLabel;
        private readonly Label statusLabel;
        private readonly Timer spinnerTimer;
        private int spinnerFrame;

        public LoadingOverlay()
        {
            Dock = DockStyle.Fill;
            Visible = false;
            Enabled = false;
            Tag = "NoTheme";
            UpdateOverlayColor();

            var card = new Panel
            {
                Size = new Size(220, 100),
                Tag = "Card",
            };

            spinnerLabel = new Label
            {
                Text = SpinnerFrames[0],
                Font = new Font("Segoe UI", 28F, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(60, 44),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(80, 12),
                BackColor = Color.Transparent,
            };

            statusLabel = new Label
            {
                Text = "Running tests\u2026",
                Font = DesignTokens.Typography.BodyMedium,
                AutoSize = false,
                Size = new Size(200, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 60),
                BackColor = Color.Transparent,
                Tag = "Secondary",
            };

            card.Controls.Add(spinnerLabel);
            card.Controls.Add(statusLabel);
            Controls.Add(card);

            Resize += (s, e) =>
            {
                card.Location = new Point(
                    Math.Max(0, (Width - card.Width) / 2),
                    Math.Max(0, (Height - card.Height) / 2));
            };

            spinnerTimer = new Timer { Interval = 80 };
            spinnerTimer.Tick += (s, e) =>
            {
                spinnerFrame = (spinnerFrame + 1) % SpinnerFrames.Length;
                spinnerLabel.Text = SpinnerFrames[spinnerFrame];
            };
        }

        private void UpdateOverlayColor()
        {
            var baseColor = CatalogUi.PageBack;
            BackColor = Color.FromArgb(200, baseColor.R, baseColor.G, baseColor.B);
        }

        public void Show(string message = "Running tests\u2026")
        {
            UpdateOverlayColor();
            statusLabel.Text = message;
            spinnerFrame = 0;
            spinnerLabel.Text = SpinnerFrames[0];
            spinnerTimer.Start();
            Enabled = true;
            Visible = true;
            BringToFront();
        }

        public void HideOverlay()
        {
            spinnerTimer.Stop();
            Visible = false;
            Enabled = false;
            SendToBack();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                spinnerTimer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
