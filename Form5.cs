using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PripravaNaZkousku
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
            //Transformace pomocí matic
            Graphics g = e.Graphics;

            Matrix m = new Matrix();

            m.Translate(50, 50);
            g.Transform = m;
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 50, 50));
            m.Reset();

            m.Translate(100, 100);
            m.Rotate(45);
            g.Transform = m;
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 50, 50));
            m.Reset();

            m.Translate(150, 150);
            m.Scale(2f, 2f);
            g.Transform = m;
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 50, 50));
            m.Reset();

            m.Translate(200, 200);
            m.Shear(2f, 2f);
            g.Transform = m;
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 50, 50));
            m.Reset();

            g.ResetTransform();
            Matrix customMatrix = new Matrix(
                1.0f, 0.0f, // m11 - scale v X (def: 1), m12 - shear v X (def: 0).
                0.0f, 2.0f, // m21 - schear v Y (def: 0), m22: scale v Y (def: 1).      
                50.0f, 50.0f  // dx, dy: posunutí. defaultně 0    Posun o 50px doprava dolu
            );
            g.Transform = customMatrix;
            g.DrawRectangle(Pens.Red, new Rectangle(300, 0, 50, 50));
            g.ResetTransform();


            //Grafická cesta a její transformace
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(new Point(0, 0), new Point(300, 300));
            Point[] points = { new Point(300,300), new Point(242,200), new Point(420, 300) };
            path.AddCurve(points);
            path.AddRectangle(new Rectangle(420, 300, 50, 50));
            path.CloseFigure();

            path.Widen(Pens.PeachPuff);

            Matrix mat = new Matrix();
            mat.Translate(100, 0);
            path.Transform(mat);

            g.DrawPath(Pens.Green, path);

        }
    }
}
