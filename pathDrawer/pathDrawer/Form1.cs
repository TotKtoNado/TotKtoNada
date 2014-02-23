using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pathDrawer
{
    public partial class Form1 : Form
    {
        private Bitmap pic = new Bitmap(1,1);
        game.Agent bill = new game.Agent();
        private double desX = 0.01;
        private double desY = 0.01;

        public Form1()
        {
            InitializeComponent();
            pic = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            drawCircle(ref pic, e.X, e.Y, Color.Green, 2);
            desX = e.X;
            desY = e.Y;
        }

        private void pictureBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            pic = new Bitmap(pic, pictureBox1.Size);

            int x, y;
            x = (int)(bill.getX() * pictureBox1.Size.Width);
            y = (int)(bill.getY() * pictureBox1.Size.Height);
            pic.SetPixel(x, y, Color.Blue);
            //drawCircle(ref pic, x, y, Color.Blue, 2);

            double dirX = (desX / pictureBox1.Size.Width) - bill.getX();
            double dirY = (desY / pictureBox1.Size.Height) - bill.getY();
            double norm = Math.Sqrt(dirX * dirX + dirY * dirY);
            dirX /= norm;
            dirY /= norm;

            bill.move(dirX, dirY);

            x = (int)(bill.getX() * pictureBox1.Size.Width);
            y = (int)(bill.getY() * pictureBox1.Size.Height);
            pic.SetPixel(x, y, Color.Black);
           // drawCircle(ref pic, x, y, Color.Black, 2);

            pictureBox1.BackgroundImage = pic;

            coordShow.Text = desX.ToString() + ";" + desY.ToString();
        }
        double radius;
        private void drawCircle(ref Bitmap pic, int x, int y, Color col, int rad)
        {
            for (int i = Math.Max(1, x - rad); i < Math.Min(pic.Size.Width, x + rad); i++)
            {
                for (int j = Math.Max(1, y - rad); j < Math.Min(pic.Size.Height, y + rad); j++)
                {
                    i -= x;
                    j -= y;

                    radius = Math.Sqrt(i * i + j * j);

                    i += x;
                    j += y;


                    if (radius <= rad)
                    {
                        pic.SetPixel(i, j, col);
                    }
                }
            }
        }
    }
}

namespace game
{
    public class Agent {
        private double x=0.5;
        private double y;
        private double speed;
        
        private double desX;
        private double desY;

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public Agent () {
            x = 0.5;
            y = 0.5;
            speed = 0.01;
        }

        public void setDes(double x_ , double y_) {
            desX = x_;
            desY = y_;
        }
        public bool move(double x_, double y_) {
            x += x_ * speed;
            y += y_ * speed;
            if ((x >= 0.0) && (x <= 1.0) && (y >= 0.0) && (y <= 1.0))
            {
                return true;
            }
            else
            {
                x -= x_ * speed;
                y -= y_ * speed;
                return false;
            }
        }
    }
}