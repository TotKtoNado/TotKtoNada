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

    public class Agent : Element
    {
        protected Coordinates coord;
        protected float speed;
        protected Board server;
        protected static Random  randGen= new Random();


        public Agent()
        {
            float x = (float) randGen.NextDouble();
            float y = (float) randGen.NextDouble();
            coord = new Coordinates(x, y);
            speed = 0.02f;
        }

        public Agent(float x, float y, float speed_)
        {
            coord = new Coordinates(x, y);
            speed = speed_;
        }

        //general agent functions
        public void setBoard (ref Board server_) {
            server = server_;
        }

        public Coordinates getCoord()
        {
            return coord;
        }

        public float getSpeed()
        {
            return speed;
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
        //individual agent fuctions
        new public void draw(PaintEventArgs e)
        {
            int x = (int)(coord.x * (e.ClipRectangle.Width));
            int y = (int)(coord.y * (e.ClipRectangle.Height));
            Pen p = new Pen(Color.Black, 3);
            e.Graphics.DrawEllipse(p, x, y, 5, 5);
             //-----------------------------------------------------
            //dispose pen and graphics object
            p.Dispose();
        }
      
        
        public virtual void doSomething() {}
        
    }
}
