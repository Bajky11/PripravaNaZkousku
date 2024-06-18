using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PripravaNaZkousku
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int dx = 1;
        int dy = 1;
        float angle = 0;
        String time = DateTime.Now.ToString("HH:mm:ss");

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
           | BindingFlags.Instance | BindingFlags.NonPublic, null,
           this.panel1, new object[] { true });
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Update(g);
            Draw(g);
            panel1.Invalidate();
        }

        private void Update(Graphics g)
        {
            //Upadate rectangle
            if (x < 0)
            {
                dx = 1;
            }
            if (x > panel1.Width)
            {
                dx = -1;
            }

            if (y < 0)
            {
                dy = 1;
            }
            if (y > panel1.Height)
            {
                dy = -1;
            }

            x += dx;
            y += dy;

            //Update time
            time = DateTime.Now.ToString("HH:mm:ss");

            //Update Transformations
            angle++;
        }

        private void Draw(Graphics g)
        {
            DrawRotatingRectangle(g);
            g.DrawString(time, new Font("Arial", 20), Brushes.Black, new Point(10, 10));
            DrawClock(g);
        }

        private void DrawRotatingRectangle(Graphics g)
        {
            g.TranslateTransform(x + 25, y + 25);
            g.RotateTransform(angle);
            g.TranslateTransform(-(x + 25), -(y + 25));
            g.FillRectangle(Brushes.Black, new Rectangle(x, y, 50, 50));
            g.ResetTransform();
        }

        private void DrawClock(Graphics g)
        {
            int smallerWindowSize = Math.Min(panel1.Width, panel1.Height);
            int originPoint = (panel1.Width - smallerWindowSize) / 2;
            int centerCircleWidth = 20;
            int secondsIndicatorLenght = 10;
            int minutesIndicatorLenght = 20;
            int[] numbers = { 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            int numberSize = 20;

            //Draw center circle
            g.FillEllipse(Brushes.Black, new Rectangle(panel1.Width / 2 - centerCircleWidth / 2, panel1.Height / 2 - centerCircleWidth / 2, centerCircleWidth, centerCircleWidth));

            //Draw clock outline
            g.DrawEllipse(Pens.Black, new Rectangle(originPoint, 0, smallerWindowSize, smallerWindowSize));

            //Draw second indicators
            for (int i = 0; i < 60; i++)
            {
                g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
                g.RotateTransform(i * 6);
                g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));
                g.DrawLine(Pens.Black, new Point(panel1.Width / 2, 0), new Point(panel1.Width / 2, secondsIndicatorLenght));
                g.ResetTransform();
            }

            //Draw minutes indicators
            for (int i = 0; i < 12; i++)
            {
                g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
                g.RotateTransform(i * (360/12));
                g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));
                g.DrawLine(new Pen(Color.Black, 5), new Point(panel1.Width / 2, 0), new Point(panel1.Width / 2, minutesIndicatorLenght));
                g.ResetTransform();
            }

            // Draw numbers
            for (int i = 0; i < 12; i++)
            {
                g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
                g.RotateTransform(i * 30);
                g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));

                // Přidání transformace pro narovnání čísla
                g.TranslateTransform(panel1.Width / 2, 50);
                g.RotateTransform(-i * 30);
                g.DrawString(numbers[i].ToString(), new Font("Arial", numberSize), Brushes.Black, new PointF(-numberSize / 2, -numberSize / 2));
                g.ResetTransform();
            }

            //Draw seconds hand
            g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
            g.RotateTransform((360 / 60) * DateTime.Now.Second);
            g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));
            g.DrawLine(Pens.Red, new Point(panel1.Width / 2, panel1.Height / 2), new Point(panel1.Width/2, 100));
            g.ResetTransform();

            // Draw minutes hand
            g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
            g.RotateTransform((360 / 60) * DateTime.Now.Minute);
            g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));
            g.DrawLine(Pens.Black, new Point(panel1.Width / 2, panel1.Height / 2), new Point(panel1.Width / 2, 80));
            g.ResetTransform();

            // Draw hours hand
            g.TranslateTransform(panel1.Width / 2, panel1.Height / 2);
            g.RotateTransform((360 / 12) * DateTime.Now.Hour);
            g.TranslateTransform(-(panel1.Width / 2), -(panel1.Height / 2));
            g.DrawLine(new Pen(Color.Black, 5), new Point(panel1.Width / 2, panel1.Height / 2), new Point(panel1.Width / 2, 80));
            g.ResetTransform();
        }

        private void DrawBasicShapes(Graphics g)
        {
            g.DrawLine(Pens.Black, new Point(0, 0), new Point(100, 100));
            g.DrawRectangle(Pens.Black, new Rectangle(150, 150, 100, 50));
            Point[] points = { new Point(40, 20), new Point(60, 20), new Point(122, 55) };
            g.DrawPolygon(Pens.Black, points);
            g.DrawEllipse(Pens.Black, new Rectangle(300, 200, 100, 100));
            g.DrawString("ZKOUSKA", new Font("Curier New", 20), Brushes.Red, new Point(300, 300));
        }
    }
}
