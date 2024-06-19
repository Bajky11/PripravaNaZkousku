using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PripravaNaZkousku
{
    public partial class Form6 : Form
    {
        private List<Point> points = new List<Point>(); // Seznam bodů


        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGeneratePoints_MouseClick(object sender, MouseEventArgs e)
        {
            var rand = new Random();
            points.Clear();
            for (int i = 0; i < 50; i++)
            {
                points.Add(new Point(rand.Next(10, this.ClientSize.Width - 10), rand.Next(10, this.ClientSize.Height - 10)));
            }
            this.Invalidate(); // Překreslí formulář

            if (points.Count < 3) return;
            var convexHull = CalculateConvexHull(points);
            points = convexHull;
            this.Invalidate(); // Překreslí formulář
        }

        // Metoda pro výpočet konvexní obálky pomocí Grahamova scanu
        private List<Point> CalculateConvexHull(List<Point> points)
        {
            if (points.Count <= 1)
                return points;

            var sortedPoints = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList(); // Seřazení bodů
            var lower = new List<Point>();

            // Vypočítá spodní část konvexní obálky
            foreach (var point in sortedPoints)
            {
                while (lower.Count >= 2 && Cross(lower[lower.Count - 2], lower[lower.Count - 1], point) <= 0)
                {
                    lower.RemoveAt(lower.Count - 1);
                }
                lower.Add(point);
            }

            var upper = new List<Point>();

            // Vypočítá horní část konvexní obálky
            for (int i = sortedPoints.Count - 1; i >= 0; i--)
            {
                var point = sortedPoints[i];
                while (upper.Count >= 2 && Cross(upper[upper.Count - 2], upper[upper.Count - 1], point) <= 0)
                {
                    upper.RemoveAt(upper.Count - 1);
                }
                upper.Add(point);
            }

            lower.RemoveAt(lower.Count - 1);
            upper.RemoveAt(upper.Count - 1);

            return lower.Concat(upper).ToList(); // Spojí horní a dolní část
        }

        // Pomocná metoda pro výpočet orientace tří bodů (zda tvoří levou, pravou nebo přímou linii)
        private int Cross(Point o, Point a, Point b)
        {
            return (a.X - o.X) * (b.Y - o.Y) - (a.Y - o.Y) * (b.X - o.X);
        }

        // Metoda pro vykreslení bodů a konvexní obálky
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            if (points.Count > 0)
            {
                g.FillEllipse(Brushes.Blue, points[0].X - 3, points[0].Y - 3, 6, 6); // Vykreslí první bod
                for (int i = 1; i < points.Count; i++)
                {
                    g.FillEllipse(Brushes.Blue, points[i].X - 3, points[i].Y - 3, 6, 6); // Vykreslí ostatní body
                    g.DrawLine(Pens.Black, points[i - 1], points[i]); // Spojí body čarou
                }
                g.DrawLine(Pens.Black, points[points.Count - 1], points[0]); // Spojí poslední bod s prvním
            }
        }



    }
}
