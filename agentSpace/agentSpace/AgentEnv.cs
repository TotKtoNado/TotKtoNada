using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace agentSpace
{ 
    public class AgentEnv : Element
    //This class stores properties of agent
    {
        private Coordinates coord;
        private float speed;
        private Board server;
        private Agent agent;
        private static Random randGen = new Random();
        private string agentType;

        
        public override void draw(System.Windows.Forms.PaintEventArgs e)
        {
            int x = (int)(coord.x * (610));
            int y = (int)(coord.y * (250));
            Pen p = new Pen(Color.Black,3);
            e.Graphics.DrawEllipse(p, x, y, 5, 5);
            
           //dispose pen and graphics object
            p.Dispose();
          
        }

        public AgentEnv()
        {
            float x = (float) randGen.NextDouble();
            float y = (float) randGen.NextDouble();
            coord = new Coordinates(x, y);
            speed = 0.02f;
        }

        public AgentEnv(float x, float y, float speed_, string agType)
        {
            coord = new Coordinates(x, y);
            speed = speed_;
            agentType = agType;
        }

        public void setBoard(ref Board ser)
        {
            server = ser;
        }

        public Board getBoard () {
            return server;
        }

        public void setAgent(ref Agent ag)
        {
            agent = ag;
        }

        public void setCoord(Coordinates coord_)
        {
            coord = coord_;
        }

        public Coordinates getCoord()
        {
            return coord;
        }

        public float getSpeed()
        {
            return speed;
        }

        public void doSomething() // дописать
        {
            agent.doSomething();
        }

        


    }
}
