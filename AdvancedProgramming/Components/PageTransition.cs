using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public static class PageTransition
    {
        public static void AnimateIn(UserControl page, Panel container, Action centerPage, Action onComplete = null)
        {
            if (page == null || container == null)
            {
                onComplete?.Invoke();
                return;
            }

            centerPage?.Invoke();

            int targetX = page.Location.X;
            int targetY = page.Location.Y;
            int startX = targetX + 48;
            page.Location = new Point(startX, targetY);

            int step = 0;
            int totalSteps = DesignTokens.Animation.NormalMs / DesignTokens.Animation.TimerInterval;

            var timer = new Timer { Interval = DesignTokens.Animation.TimerInterval };
            timer.Tick += (s, e) =>
            {
                step++;
                float t = Math.Min(1f, step / (float)totalSteps);
                float eased = 1f - (1f - t) * (1f - t);
                int x = startX + (int)((targetX - startX) * eased);
                page.Location = new Point(x, targetY);

                if (step >= totalSteps)
                {
                    timer.Stop();
                    timer.Dispose();
                    page.Location = new Point(targetX, targetY);
                    onComplete?.Invoke();
                }
            };
            timer.Start();
        }
    }
}
