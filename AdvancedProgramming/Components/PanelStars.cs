using System.Drawing;
using System.Windows.Forms;
using AdvancedProgramming;

namespace AdvancedProgramming.Components
{
    public class PanelStars : Panel
    {
        private static readonly int[] X = { 0, 4, 15, 6, 10, 0, -10, -6, -15, -4 };
        private static readonly int[] Y = { -15, -5, -5, 2, 13, 7, 13, 2, -5, -5 };

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(AppColors.InsetBack);

            int mid = Width / 2, top = 10, gap = 20;
            using (var brush = new SolidBrush(AppColors.Accent))
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col <= row; col++)
                    {
                        int ox = mid + (int)((col - row / 2.0) * gap);
                        int oy = top + row * gap;
                        var pts = new Point[10];
                        for (int i = 0; i < 10; i++)
                            pts[i] = new Point(ox + X[i], oy + Y[i]);
                        g.FillPolygon(brush, pts);
                    }
                }
            }
        }
    }
}
