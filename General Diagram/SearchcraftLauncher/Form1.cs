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
    public partial class Form1 : Form
    {
        public Board board;
        public CommonFormVisualizer vis;
        Manager game1;
   

        public Form1()
        {
            InitializeComponent();
            //board = new Board();
            //vis = new CommonFormVisualizer(board, MainScreen);
            //board.Agents.addTestAgent();
            //board.Walls.addWall(new Segment(Coord.rand(), Coord.rand()));
            //vis.showSightAreas = true;
            game1 = new Manager();
            vis = game1.getFormVis(MainScreen);
            game1.addTestAgent();
            SimpleAgent bill = new SimpleAgent();
            AgentController cntrl = game1.addCustomAgent(new Coord(0.5, 0.5), bill, 0.002, 0.01);
            bill.setController(cntrl);
           

        }

        private void MainScreen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                game1.launchAgents();
                MainScreen.Refresh();
            }
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            vis.draw(e);
        }
    }
}
