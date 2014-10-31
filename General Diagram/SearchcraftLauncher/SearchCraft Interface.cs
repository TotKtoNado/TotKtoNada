using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneralPackage.GameData;
using GeneralPackage.Visualizer;
using GeneralPackage.Structures;
using GeneralPackage.Manager;
using GeneralPackage.Controller;


namespace SearchcraftLauncher
{
    public partial class UserInterface : Form
    {
        public CommonFormVisualizer vis;
        Manager game1;
   

        public UserInterface()
        {
            InitializeComponent();

            game1 = new Manager();
            vis = game1.getFormVis(MainScreen);
            SimpleAgent agent;
            ControllerSet temp;
            //for (int i = 0; i < 30; i++) { game1.board.Walls.addWall(new Segment(Coord.rand(), Coord.rand())); }
            //for (int i = 0; i < 1; i++)
            //{
            //    agent = new SimpleAgent();
            //    temp = game1.addCustomAgent(Coord.rand(), agent, 0.008, 0.08);
            //    agent.setController(temp);
            //}
           // game1.board.Walls.addWall(new Segment(0.4, 0.4, 0.5, 0.5));
           // game1.board.Walls.addWall(new Segment(0.2, 0.45, 0.99, 0.45));
            game1.board.Walls.addWall(new Segment(0.79, 0.01, 0.79, 0.99));
            agent = new SimpleAgent();
            temp = game1.addCustomAgent(new Coord(0.51,0.51), agent, 0.008, 0.08);
            agent.setController(temp);

            turnTime.Enabled = true;

        }

        private void MainScreen_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    //game1.launchAgents();
            //    // MainScreen.Refresh();
            //}
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            vis.draw(e);
            //vis.drawCustomWalls(e, game1.wallsAround(1));
        }

        private void turnTime_Tick(object sender, EventArgs e)
        {
            game1.launchAgents();
            MainScreen.Refresh();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void showSightRange_CheckedChanged(object sender, EventArgs e)
        {
            vis.showSightAreas = showSightRange.Checked;
            MainScreen.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (turnTime.Enabled)
            {
                turnTime.Enabled = false;
                stopButton.Text = "Go";
            }
            else
            {
                turnTime.Enabled = true;
                stopButton.Text = "Stop";
            }
        }
                                          
        
    }
}
