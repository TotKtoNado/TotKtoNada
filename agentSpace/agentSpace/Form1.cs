using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace agentSpace
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            Agent bill = new Agent(0.1f,0.6f);
            list[0] = bill;
            Board phirexia = new Board(ref Field, ref list);
            label1.Text = label1.Text + "\nphirexia.agentCoord(0) :" + phirexia.agentCoord(0);

            bill.setCoord(0.4f, 0.6f);

            label1.Text =label1.Text + "\nbill.getCoord() : " + bill.getCoord().x.ToString() + " ; " + bill.getCoord().y.ToString();

            label1.Text = label1.Text + "\nphirexia.agentCoord(0) :" + phirexia.agentCoord(0);
           // bill.draw(ref Field);
        }

        Agent[] list = new Agent[1];
        private void Field_Click(object sender, EventArgs e)
        {

        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            list[0].draw(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float x = list[0].getCoord().x;
            float y = list[0].getCoord().y;
            x = (x + 0.01f)%1;
            y = (y + 0.01f)%1;
            list[0].setCoord(x, y);
            Field.Refresh();
        }
    }

    public class Board
    {
        private PictureBox agentWorld;
        private Agent[] agentList;

        public Board(ref PictureBox pic, ref Agent[] list)
        {
            agentWorld = pic;
            agentList = list;
        }

        public string agentCoord(int agentID)
        {
            return agentList[agentID].getCoord().x.ToString() +" ; " + agentList[agentID].getCoord().y.ToString();
        }
    }


    public class Agent :Element
    {
        private Coordinates coord;

        public Agent()
        {
            coord = new Coordinates(0.5f, 0.5f);
        }

        public Agent(float x, float y)
        {
            coord = new Coordinates(x, y);
        }   


        public Coordinates getCoord () {
            return coord;
        }

        public void setCoord(float x, float y)
        {
            coord.x = x;
            coord.y = y;
        }

        public void setCoord(Coordinates cor)
        {
            coord = cor;
        }

        new public void draw(PaintEventArgs e) 
        {
            int x = (int)(coord.x * (610));
            int y = (int)(coord.y * (250));
            Pen p = new Pen(Color.Black,3);
            e.Graphics.DrawEllipse(p, x, y, 5, 5);
            
            //dispose pen and graphics object
            p.Dispose();
        }
    }


    public class Coordinates
    {
        public float x { get; set; }
        public float y { get; set; }
        public Coordinates (float x_, float y_) {
            x = Math.Max(Math.Min(x_, 1.0f), 0.0f);
            y = Math.Max(Math.Min(y_, 1.0f), 0.0f);
        }

    }

    public class Element
    {
        public void draw(PaintEventArgs e)
        {
        }
    }
}





