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
        Bitmap carImg = new Bitmap("car.jpg");

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
            //DrawWithHatchBrush(g);
            //DrawWithTextureBrush(g);
            //DrawCurves(g);
            //DrawWithPrahovani(img, g, 127);
            //RemoveNoiseWithMedianFilter(carImg, g);
            //RemoveNoiseWithMeanFilter(carImg, g);
        }

        void RemoveNoiseWithMedianFilter(Bitmap original, Graphics g)
        {
            imgBox.Image = carImg;
            imgBox.Invalidate();

            // Vytvoření nového bitmapového objektu pro uložení vyčištěného obrázku
            Bitmap denoisedImage = new Bitmap(original.Width, original.Height);

            int kernelSize = 3; // Velikost čtvercového okolí, např. 3x3
            int offset = 1; // Poloměr okolí, což je kernelSize / 2

            // Procházíme každý pixel obrázku, s výjimkou okrajových pixelů
            for (int y = offset; y < original.Height - offset; y++)
            {
                for (int x = offset; x < original.Width - offset; x++)
                {
                    List<int> rValues = new List<int>();
                    List<int> gValues = new List<int>();
                    List<int> bValues = new List<int>();

                    // Procházíme okolí pixelu
                    for (int ky = -offset; ky <= offset; ky++) // ky = -1, 1, 1
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            // Získáme barvu aktuálního pixelu v okolí
                            Color pixelColor = original.GetPixel(x + kx, y + ky); // Tady k hodnotě x,y přiítáme kx, ky. Y = 5, ky = -1, tzn že se podíváme do pixelu 5-1 tzn 4, paK 5, pak 6...

                            // Přidáme hodnoty jednotlivých kanálů do seznamů
                            rValues.Add(pixelColor.R);
                            gValues.Add(pixelColor.G);
                            bValues.Add(pixelColor.B);
                        }
                    }

                    // Seřadíme hodnoty kanálů, aby bylo možné získat medián
                    rValues.Sort();
                    gValues.Sort();
                    bValues.Sort();

                    // Získáme mediánovou hodnotu z každého kanálu
                    Color medianColor = Color.FromArgb(
                        rValues[rValues.Count / 2],
                        gValues[gValues.Count / 2],
                        bValues[bValues.Count / 2]);

                    // Nastavíme mediánovou barvu na aktuální pixel vyčištěného obrázku
                    denoisedImage.SetPixel(x, y, medianColor);
                }
            }

            // Vykreslíme vyčištěný obrázek na grafický objekt
            g.DrawImage(denoisedImage, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }

        void RemoveNoiseWithMeanFilter(Bitmap original, Graphics g)
        {

            imgBox.Image = carImg;
            imgBox.Invalidate();

            // Vytvoření nového bitmapového objektu pro uložení vyčištěného obrázku
            Bitmap denoisedImage = new Bitmap(original.Width, original.Height);

            int kernelSize = 3; // Velikost čtvercového okolí, např. 3x3
            int offset = kernelSize / 2; // Poloměr okolí, což je kernelSize / 2

            // Procházíme každý pixel obrázku, s výjimkou okrajových pixelů
            for (int y = offset; y < original.Height - offset; y++)
            {
                for (int x = offset; x < original.Width - offset; x++)
                {
                    int rSum = 0;
                    int gSum = 0;
                    int bSum = 0;
                    int count = 0;

                    // Procházíme okolí pixelu
                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            // Získáme barvu aktuálního pixelu v okolí
                            Color pixelColor = original.GetPixel(x + kx, y + ky);

                            // Přidáme hodnoty jednotlivých kanálů k sumě
                            rSum += pixelColor.R;
                            gSum += pixelColor.G;
                            bSum += pixelColor.B;
                            count++;
                        }
                    }

                    // Vypočítáme průměrnou hodnotu každého kanálu
                    int rMean = rSum / count;
                    int gMean = gSum / count;
                    int bMean = bSum / count;

                    // Vytvoříme novou barvu s průměrnými hodnotami
                    Color meanColor = Color.FromArgb(rMean, gMean, bMean);

                    // Nastavíme průměrnou barvu na aktuální pixel vyčištěného obrázku
                    denoisedImage.SetPixel(x, y, meanColor);
                }
            }

            // Vykreslíme vyčištěný obrázek na grafický objekt
            g.DrawImage(denoisedImage, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }



        void DrawWithPrahovani(Bitmap original, Graphics g, int threshold)
        {
            Bitmap binaryCopy = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // získám aktuální pixel
                    Color pixelColor = original.GetPixel(x, y);

                    // převedu na černobílý na základě prahové hodnoty
                    int grayValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color newPixelColor = grayValue >= threshold ? Color.White : Color.Black;

                    // vykreslím pixel
                    binaryCopy.SetPixel(x, y, newPixelColor);
                }
            }
            g.DrawImage(binaryCopy, new Rectangle(0, 0, canvas.Width, canvas.Height));
        }

        void DrawCurves(Graphics g)
        {
            g.DrawArc(Pens.Black, new Rectangle(0, 0, 300, 300), 0, 90);

            Point[] points = { new Point(0, 0), new Point(100, 100), new Point(100, -50), new Point(200, 200) };
            g.DrawCurve(Pens.Black, points);

            g.DrawBezier(Pens.Red, new Point(100, 300), new Point(200, 200), new Point(300, 500), new Point(400, 200));

            g.DrawClosedCurve(Pens.Purple, points);
        }

        void DrawWithHatchBrush(Graphics g)
        {
            // Normální hatch
            g.FillRectangle(
                new HatchBrush(HatchStyle.Horizontal, Color.Purple, Color.Transparent),
                new Rectangle(200, 200, 100, 100)
                );

            // Hatch po úhlem
            int size = 100;
            Bitmap bitmap = new Bitmap(size, size);
            using (Graphics tempGraphics = Graphics.FromImage(bitmap))
            {
                HatchBrush hatchBrush = new HatchBrush(HatchStyle.Horizontal, Color.Purple, Color.Transparent);
                tempGraphics.FillRectangle(hatchBrush, new Rectangle(0, 0, size, size));
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);

            textureBrush.RotateTransform(45);
            g.FillRectangle(textureBrush, new Rectangle(50, 200, size, size));


        }

        void DrawWithTextureBrush(Graphics g)
        {
            Bitmap bitmap = new Bitmap("bee.png");
            TextureBrush textureBrush = new TextureBrush(bitmap);
            //textureBrush.RotateTransform(45);
            g.FillRectangle(textureBrush, 50, 50, 200, 200);
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
