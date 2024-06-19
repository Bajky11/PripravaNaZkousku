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
    public partial class Form4 : Form
    {

        private List<Point> polygonPoints;
        private List<Tuple<Point, Point, Point>> triangles;

        public Form4()
        {
            InitializeComponent();

            polygonPoints = new List<Point>
            {
                new Point(50, 150),
                new Point(100, 50),
                new Point(200, 50),
                new Point(250, 150),
                new Point(200, 250),
                new Point(100, 250)
            };
            triangles = new List<Tuple<Point, Point, Point>>();
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Vymazání pozadí
            g.Clear(Color.White);

            // Vykreslení polygonu pomocí černé barvy
            Pen pen = new Pen(Color.Black, 2);
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                // Vykreslení čáry mezi dvěma sousedními body polygonu
                g.DrawLine(pen, polygonPoints[i], polygonPoints[(i + 1) % polygonPoints.Count]);
            }

            // Provést triangulaci polygonu
            TriangulatePolygon();

            // Vykreslení trojúhelníků po triangulaci pomocí červené barvy
            Pen trianglePen = new Pen(Color.Red, 1);
            foreach (var triangle in triangles)
            {
                // Vykreslení čar pro každý trojúhelník
                g.DrawLine(trianglePen, triangle.Item1, triangle.Item2);
                g.DrawLine(trianglePen, triangle.Item2, triangle.Item3);
                g.DrawLine(trianglePen, triangle.Item3, triangle.Item1);
            }
        }

        private void TriangulatePolygon()
        {
            // Vymazání předchozích trojúhelníků
            triangles.Clear();

            // Vytvoření kopie seznamu bodů polygonu pro úpravy během algoritmu
            List<Point> points = new List<Point>(polygonPoints);

            // Dokud zůstává více než tři body, pokračujeme v odstraňování uší
            while (points.Count > 3)
            {
                bool earFound = false;

                // Procházení bodů polygonu pro nalezení a odstranění ucha
                for (int i = 0; i < points.Count; i++)
                {
                    Point prev = points[(i - 1 + points.Count) % points.Count]; // Předchozí bod
                    Point curr = points[i]; // Aktuální bod
                    Point next = points[(i + 1) % points.Count]; // Následující bod

                    // Kontrola, zda je aktuální trojice bodů uchem
                    if (IsEar(prev, curr, next, points))
                    {
                        // Přidání ucha (trojúhelníku) do seznamu trojúhelníků
                        triangles.Add(new Tuple<Point, Point, Point>(prev, curr, next));

                        // Odstranění aktuálního bodu z polygonu
                        points.RemoveAt(i);

                        // Označení, že bylo nalezeno a odstraněno ucho
                        earFound = true;
                        break;
                    }
                }

                // Pokud nebylo nalezeno žádné ucho, přerušení cyklu, aby se zabránilo nekonečnému cyklu
                if (!earFound)
                {
                    break;
                }
            }

            // Pokud zůstávají právě tři body, přidání posledního trojúhelníku
            if (points.Count == 3)
            {
                triangles.Add(new Tuple<Point, Point, Point>(points[0], points[1], points[2]));
            }
        }

        private bool IsEar(Point prev, Point curr, Point next, List<Point> points)
        {
            // Kontrola, zda je trojice bodů (prev, curr, next) konvexní
            if (IsConvex(prev, curr, next))
            {
                // Kontrola, zda žádný jiný bod polygonu není uvnitř tohoto trojúhelníku
                foreach (var point in points)
                {
                    if (point != prev && point != curr && point != next && IsPointInTriangle(point, prev, curr, next))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool IsConvex(Point a, Point b, Point c)
        {
            // Výpočet vektorového součinu pro určení, zda je trojice bodů konvexní
            return ((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)) > 0;
        }

        private bool IsPointInTriangle(Point p, Point a, Point b, Point c)
        {
            // Barycentrické souřadnice pro kontrolu, zda je bod uvnitř trojúhelníku
            float w1 = (a.X * (c.Y - a.Y) + (p.Y - a.Y) * (c.X - a.X) - p.X * (c.Y - a.Y)) /
                       ((b.Y - a.Y) * (c.X - a.X) - (b.X - a.X) * (c.Y - a.Y));
            float w2 = (p.Y - a.Y - w1 * (b.Y - a.Y)) / (c.Y - a.Y);
            return w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1;
        }

    }
}
