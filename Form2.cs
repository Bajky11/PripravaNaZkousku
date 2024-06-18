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

    public partial class Form2 : Form
    {
        Bitmap img = new Bitmap("spring.jpg");

        public Form2()
        {
            InitializeComponent();
            imgBox.Image = img;
            imgBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //DrawImageGrayScale(img,g);
            //DrawImageEdgeDetection(img, g);
            //DrawGraphicsPath(g);
            //DrawImageNegative(img, g);
            //DrawImageWithClip(g);
        }

        void DrawGraphicsPath(Graphics g)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(30, 30, 100, 30);
            path.AddArc(new Rectangle(100, -20, 100, 100), 180, -90);
            path.AddLine(150, 80, 150, 150);
            path.CloseFigure();

            g.DrawPath(Pens.Black, path);

            g.TranslateTransform(0, 150);

            g.FillPath(Brushes.Black, path);

            g.ResetTransform();
        }

        void DrawImageWithClip(Graphics g)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            path.AddRectangle(new Rectangle(0, 0, 100, 100));
            path.AddRectangle(new Rectangle(80, 80, 100, 100));
            path.AddEllipse(new Rectangle(200, 300, 200, 120));

            g.SetClip(path);
            g.DrawImage(img, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }

        void DrawImageGrayScale(Bitmap original, Graphics g)
        {
            Bitmap grayScaleCopy = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // získám aktuální pixel
                    Color pixelColor = original.GetPixel(x, y);

                    // převedu na černobílý
                    int grayValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // vykreslím pixel
                    Color newPixelColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayScaleCopy.SetPixel(x, y, newPixelColor);
                }
            }
            g.DrawImage(grayScaleCopy, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }

        void DrawImageNegative(Bitmap original, Graphics g)
        {
            Bitmap copy = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // získám aktuální pixel
                    Color current = original.GetPixel(x, y);

                    // invertuji barvy
                    int invertedR = 255 - current.R;
                    int invertedG = 255 - current.G;
                    int invertedB = 255 - current.B;

                    // vykreslím pixel
                    Color newColor = Color.FromArgb(invertedR, invertedG, invertedB);
                    copy.SetPixel(x, y, newColor);
                }
            }

            g.DrawImage(copy, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }

        void DrawImageEdgeDetection(Bitmap original, Graphics g)
        {
            Bitmap copy = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height - 1; y++)
            {
                for (int x = 0; x < original.Width - 1; x++)
                {
                    // získam pixely (aktuální, pravý, spodní)
                    Color current = original.GetPixel(x, y);
                    Color right = original.GetPixel(x + 1, y);
                    Color bottom = original.GetPixel(x, y + 1);

                    // převedu je na černobílé
                    int currentGray = (current.R + current.G + current.B) / 3;
                    int rightGray = (right.R + right.G + right.B) / 3;
                    int bottomGray = (bottom.R + bottom.G + bottom.B) / 3;

                    // vypočítám hranu
                    int edgeValue = Math.Abs(currentGray - rightGray) + Math.Abs(currentGray - bottomGray);
                    edgeValue = edgeValue < 0 ? 0 : edgeValue > 255 ? 255 : edgeValue;

                    // vykreslím pixel
                    Color newPixelColor = Color.FromArgb(edgeValue, edgeValue, edgeValue);
                    copy.SetPixel(x, y, newPixelColor);
                }
            }
            g.DrawImage(copy, new Rectangle(0, 0, canvas.Width, canvas.Height));

        }
    }
}
