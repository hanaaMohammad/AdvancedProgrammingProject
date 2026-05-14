using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public class PanelStars : Panel
    {
        private Point[] star;

        public PanelStars()
        {
            this.Size = new Size(490, 150);
            this.BackColor = Color.DarkCyan;
            InitializeStar();
        }

        private void InitializeStar()
        {
            star = new Point[]
            {
                new Point(0, -15),
                new Point(4, -5),
                new Point(15, -5),
                new Point(6, 2),
                new Point(10, 13),
                new Point(0, 7),
                new Point(-10, 13),
                new Point(-6, 2),
                new Point(-15, -5),
                new Point(-4, -5)
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTriangleStars(e.Graphics);
        }

        private void DrawTriangleStars(Graphics g)
        {
            int rows = 5;
            int starSize = 14;
            int startX = this.Width / 2;
            int startY = 4;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int x = startX + (int)((j - i / 2.0) * starSize);
                    int y = startY + i * starSize;

                    Point[] copy = new Point[star.Length];
                    for (int l = 0; l < star.Length; l++)
                    {
                        copy[l] = new Point(
                            (int)(x + star[l].X),
                            (int)(y + star[l].Y)
                        );
                    }
                    g.FillPolygon(Brushes.DimGray, copy);
                    g.DrawPolygon(Pens.DarkCyan, copy);
                }
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            Invalidate();
        }
    }
}
