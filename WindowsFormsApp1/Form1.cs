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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private PointF[] Points = new PointF[4];


        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Imagem.Height, Imagem.Width);
            Imagem.Image = bmp;

        }

        
        private static float calcularX(float u, float X0, float X1, float X2, float X3)
        {
            double resp1 = System.Convert.ToDouble(X0) * Math.Pow((1 - u), 3) + System.Convert.ToDouble(X1) * 3 * u * Math.Pow((1 - u), 2) + System.Convert.ToDouble(X2) * 3 * Math.Pow(u, 2) * (1 - u) + System.Convert.ToDouble(X3) * Math.Pow(u, 3);
            float resp = (float)resp1;
            return resp;
            
        }
        private static float calcularY(float u, float Y0, float Y1, float Y2, float Y3)
        {
            double resp1 = System.Convert.ToDouble(Y0) * Math.Pow((1 - u), 3) + System.Convert.ToDouble(Y1) * 3 * u * Math.Pow((1 - u), 2) + System.Convert.ToDouble(Y2) * 3 * Math.Pow(u, 2) * (1 - u) + System.Convert.ToDouble(Y3) * Math.Pow(u, 3);
            float resp = (float)resp1;
            return resp;

        }

        // Draw the Bezier curve.
        public static void DesenharCurva(Graphics gr, Pen the_pen,float dt, PointF pt0, PointF pt1, PointF pt2, PointF pt3)
        {
           
            // Draw the curve.
            List<PointF> points = new List<PointF>();
            for (float t = 0.0f; t < 1.0; t += dt)
            {
                points.Add(new PointF(
                    calcularX(t, pt0.X, pt1.X, pt2.X, pt3.X),
                    calcularY(t, pt0.Y, pt1.Y, pt2.Y, pt3.Y)));
            }

            // Connect to the final point.
            points.Add(new PointF(
                calcularX(1.0f, pt0.X, pt1.X, pt2.X, pt3.X),
                calcularY(1.0f, pt0.Y, pt1.Y, pt2.Y, pt3.Y)));

            
            gr.DrawLines(the_pen, points.ToArray());
            

        }

        // Draw the currently selected points. 
        // If we have four points, draw the Bezier curve.
        private void Desenhar(object sender, EventArgs f)
        {
            
            Bitmap bmp = new Bitmap(Imagem.Height, Imagem.Width);
            using (Graphics e = Graphics.FromImage(bmp))
            {
                    e.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Clear(Imagem.BackColor);


                    DesenharCurva(e,Pens.Black, 0.01f,
                        Points[0], Points[1], Points[2], Points[3]);
                
                
                // Draw the control points.
                for (int i = 0; i < 4; i++)
                {
                    e.FillRectangle(Brushes.Black,
                        Points[i].X - 3, Points[i].Y - 3, 3, 3);
                    e.DrawRectangle(Pens.Black,
                        Points[i].X - 3, Points[i].Y - 3, 3, 3);
                }
            }
            
            Imagem.Image = bmp;
        }


        private void DesenharPonto(object sender, EventArgs f, int x, int y)
        {

            Bitmap bmp = (Bitmap)Imagem.Image;
            using (Graphics e = Graphics.FromImage(bmp))
            {

             e.FillRectangle(Brushes.Black,
                        x - 3, y - 3, 3, 3);
             e.DrawRectangle(Pens.Black,
                        x - 3, y - 3, 3, 3);
            }

            Imagem.Image = bmp;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        int cont = 0; 
        private void Imagem_Click(object sender, EventArgs e)
        {
            

            MouseEventArgs mouse = (MouseEventArgs)e;

            if (radioButton1.Checked == true)
            {
                Points[0].X = mouse.X;
                Points[0].Y = mouse.Y;
                DesenharPonto(sender, e, mouse.X, mouse.Y);
                cont++;
            }
            else if (radioButton2.Checked == true)
            {
                Points[3].X = mouse.X;
                Points[3].Y = mouse.Y;
                DesenharPonto(sender, e, mouse.X, mouse.Y);
                cont++;
            }
            else if (radioButton3.Checked == true)
            {
                Points[1].X = mouse.X;
                Points[1].Y = mouse.Y;
                DesenharPonto(sender, e, mouse.X, mouse.Y);
                cont++;
            }
            else if (radioButton4.Checked == true)
            {
                Points[2].X = mouse.X;
                Points[2].Y = mouse.Y;
                DesenharPonto(sender, e, mouse.X, mouse.Y);
                cont++;
            }

            if (cont > 3)
            {
                Desenhar(sender, e);
            }

        }
    }
}
