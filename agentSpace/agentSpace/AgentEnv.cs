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
        private float viewRadius;
        private float drawRad; // For drawing

        private Board server;
        private Agent agent;
        private static Random randGen = new Random();
        private string agentType;

     
        public AgentEnv()
        {
            float x = (float) randGen.NextDouble();
            float y = (float) randGen.NextDouble();
            coord = new Coordinates(x, y);
            speed = 0.02f;
        }

        public AgentEnv(float x, float y, float speed_, float radius, string agType)
        {
            coord = new Coordinates(x, y);
            speed = speed_;
            viewRadius = radius;
            drawRad = 0.02f;
            agentType = agType;
        }

        //drawings
        public override void draw(System.Windows.Forms.PaintEventArgs e)
        {
            int x = (int)((coord.x - drawRad/2.0f)* e.ClipRectangle.Width ) ;
            int y = (int)((coord.y -  drawRad/2.0f)* e.ClipRectangle.Height );
            int w = (Int32)(drawRad * e.ClipRectangle.Width);
            int h = (Int32)(drawRad * e.ClipRectangle.Height);
            Rectangle rec = new Rectangle(x, y, w, h);
            Pen p = new Pen(Color.Black, 3);
            e.Graphics.DrawEllipse(p, rec);

            //dispose pen and graphics object
            p.Dispose();

        }


        //setters getters
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

        public float getRadius()
        {
            return viewRadius;
        }

        public void doSomething() // дописать
        {
            agent.doSomething();
        }

        


    }
}
