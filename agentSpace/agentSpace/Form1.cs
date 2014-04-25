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
            Board phirexia = new Board(ref Field, ref list);
            Agent bill1 = new DummyAgent(0.5f,0.5f,0.01f);
            Agent bill2 = new DummyAgent(0.5f,0.5f,0.005f);
            Coordinates s = new Coordinates(0.2f, 0.3f) - new Coordinates(0.1f, 085f);
            Console.WriteLine(s.x);
            Console.WriteLine(s.y);
            phirexia.addAgent(ref bill1);
            phirexia.addAgent(ref bill2);
        }


        //handlers
        List<Agent> list = new List<Agent>();
        private void Field_Click(object sender, EventArgs e)
        {

        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            foreach (Agent agent in list)
            {
                agent.draw(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Agent ag in list)
            {
                ag.doSomething();
            }
            label1.Text = "";
            label1.Text = label1.Text + "\nbill.getCoord() : " + list[0].getCoord().x.ToString() + " ; " + list[0].getCoord().y.ToString();

            Field.Refresh();
        }
    }


    public class Element
    {
        public void draw(PaintEventArgs e)
        {
        }
    }
}





