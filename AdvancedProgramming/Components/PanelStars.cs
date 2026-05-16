using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public class PanelStars : Panel
    {
        private static readonly Point[] StarShape =
        {
            new Point(0, -15), new Point(4, -5), new Point(15, -5), new Point(6, 2),
            new Point(10, 13), new Point(0, 7), new Point(-10, 13), new Point(-6, 2),
            new Point(-15, -5), new Point(-4, -5)
        };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(AppColors.InsetBack);

            int startX = Width / 2;
            int startY = 10;
            const int starSize = 20;

            using (var fill = new SolidBrush(AppColors.Accent))
            using (var outline = new Pen(AppColors.Accent))
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col <= row; col++)
                    {
                        int x = startX + (int)((col - row / 2.0) * starSize);
                        int y = startY + row * starSize;
                        var points = new Point[StarShape.Length];
                        for (int i = 0; i < StarShape.Length; i++)
                            points[i] = new Point(x + StarShape[i].X, y + StarShape[i].Y);
                        e.Graphics.FillPolygon(fill, points);
                        e.Graphics.DrawPolygon(outline, points);
                    }
                }
            }
        }
    }
}
