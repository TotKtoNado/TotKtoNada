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
            ViewRange.Checked = true;
            board = new Board(ref Field,ref ViewRange);
            Coordinates pos = new Coordinates(0.2f, 0.2f);
            bill1 = board.createDummy1();
            AgentEnv bill2 = board.createDummy1();
            AgentEnv nancy = board.createLittleGirl1(); 
            bill1.setCoord(pos);
            bill2.setCoord(pos* 1.5f);
            AgentEnv iggy;
            nancy.setCoord(new Coordinates(0.49f, 0.51f));
            //Console.WriteLine(pos.norm().ToString());
            for (int i = 0; i < 5; i++)
            {
                //board.addWall(Segment.randomSeg());
                iggy = board.createDummy1();
                iggy.setCoord(new Coordinates(0.6f, 0.4f));
            }
            board.addWall(new Segment(0f, 0.5f, 0.51f, 0.5f));
            board.addWall(new Segment(0.5f, 1f, 0.5f, 0.49f));
        }


        //handlers
        private void Field_Click(object sender, EventArgs e)
        {
            board.launchAgents();
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            board.drawAll(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            board.launchAgents();

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





