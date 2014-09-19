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
            board = new Board(ref Field,ref ViewRange, ref showMatrix);
            Coordinates pos = new Coordinates(0.2f, 0.2f);
            //bill1 = board.createDummy1();
            AgentEnv bill2 = board.createFinder1();
            AgentEnv nancy;
            for (int i = 0; i < 10; i++)
            {
                nancy = board.createLittleGirl1();
                nancy.setCoord(Coordinates.randomCoord());
            }
            //bill1.setCoord(pos);
            bill2.setCoord(pos* 4.9f);
            AgentEnv iggy;
            //Console.WriteLine(pos.norm().ToString());
            for (int i = 0; i < 5; i++)
            {
                //board.addWall(Segment.randomSeg());
                iggy = board.createFinder1();
                iggy.setCoord(new Coordinates(0.6f, 0.4f));
            }
            board.addWall(new Segment(0f, 0.5f, 0.3f, 0.5f));
            board.addWall(new Segment(0.5f, 1f, 0.7f, 0.3f));
        }

        #region Mouse handlers

        private void Field_Click(object sender, EventArgs e)
        {
            board.launchAgents();
        } 

        #endregion

        #region Drawings

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            board.drawAll(e);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region Timer

        private void timer1_Tick(object sender, EventArgs e)
        {

            board.launchAgents();

            label1.Text = "";
            Field.Refresh();
        }

        #endregion

        
    }
}





