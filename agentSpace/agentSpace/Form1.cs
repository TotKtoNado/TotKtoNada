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
        private Board board;
        AgentEnv bill1;
        public mainForm()
        {
            InitializeComponent();
            board = new Board(ref Field,ref ViewRange);
            Coordinates pos = new Coordinates(0.2f, 0.2f);
            bill1 = board.createDummy1();
            AgentEnv bill2 = board.createDummy1();
            //AgentEnv bill3 = board.createDummy1();
            AgentEnv nancy = board.createLittleGirl1(); 
            bill1.setCoord(pos);
            bill2.setCoord(pos* 1.5f);
            //bill3.setCoord(pos * 0.75f);
            nancy.setCoord(new Coordinates(0.1f, 0.9f));
            //Console.WriteLine(pos.norm().ToString());
            Coordinates a = new Coordinates(0.8f, 0.8f);
            Coordinates b = new Coordinates(0.1f, 0.9f);
            Coordinates c = new Coordinates(0.9f, 0.9f);
            Coordinates d = new Coordinates(0.9f, 0.1f);
            Segment ab = new Segment(a, b);
            Segment ac = new Segment(a, c);
            Segment ad = new Segment(a, d);
            Segment bc = new Segment(b, c);
            Segment bd = new Segment(b, d);
            Segment cd = new Segment(c, d);
            Console.WriteLine("ab  bc = " + ab.intersects(bc).ToString());
            Console.WriteLine("ab  cd = " + ab.intersects(cd).ToString());
            Console.WriteLine("ad  ad = " + ad.intersects(ad).ToString());
            Console.WriteLine("bc  ad = " + bc.intersects(ad).ToString());
            Console.WriteLine("ac  bd = " + ac.intersects(bd).ToString());
        }


        //handlers
        private void Field_Click(object sender, EventArgs e)
        {
            board.launchAgents();
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            //foreach (Agent agent in list)
            //{
            //    agent.draw(e);
            //}
            board.drawAll(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //board.launchAgents();

            label1.Text = "";
            //label1.Text = label1.Text + "\nbill.getCoord() : " + list[0].getCoord().x.ToString() + " ; " + list[0].getCoord().y.ToString();
            //board.drawAll(e);
            Field.Refresh();
        }
    }


    public class Element
    {
        public virtual void draw(PaintEventArgs e)
        {
        }
    }
}





