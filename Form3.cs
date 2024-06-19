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

    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            CustomButton btn = new CustomButton();
            btn.Text = "CustomButton";
            btn.Size = new Size(150, 50);
            btn.Location = new Point(300, 300);
            this.Controls.Add(btn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            // Nastavení oblastí pro kreslení
            Rectangle rectangle1 = new Rectangle(50, 50, 100, 100);
            Rectangle rectangle2 = new Rectangle(200, 50, 100, 100);
            Rectangle rectangle3 = new Rectangle(50, 200, 100, 100);
            Rectangle rectangle4 = new Rectangle(200, 200, 100, 100);

            // Kreslení okrajů a výplní pomocí ControlPaint
            ControlPaint.DrawBorder(g, rectangle1, Color.Red, ButtonBorderStyle.Solid);
            ControlPaint.DrawBorder(g, rectangle2, Color.Green, ButtonBorderStyle.Dashed);
            ControlPaint.DrawBorder(g, rectangle3, Color.Blue, ButtonBorderStyle.Dotted);
            ControlPaint.DrawBorder(g, rectangle4, Color.Purple, ButtonBorderStyle.Inset);

            // Kreslení zvýraznění
            ControlPaint.DrawFocusRectangle(g, rectangle1);
            ControlPaint.DrawFocusRectangle(g, rectangle2, Color.Yellow, Color.Black);
        }
    }

    internal class CustomButton : Button
    {
        public CustomButton()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.FillRectangle(new HatchBrush(HatchStyle.Horizontal, Color.Black, Color.Transparent), this.ClientRectangle);
            ControlPaint.DrawBorder3D(g, this.ClientRectangle);
            TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, Color.Blue);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }
    }
}
