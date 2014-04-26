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
    public class Board
    {
        private PictureBox agentWorld;
        private const float touchDist = 0.01f;
        private List<AgentEnv> agentListm;
        
        //old
        public Board(ref PictureBox pic)
        {
            agentWorld = pic;
            agentListm = new List<AgentEnv>();
        }



        //drawings
        public void drawAll(PaintEventArgs e)
        {
            drawAllRadius(e);
            foreach (AgentEnv ag in agentListm)
            {
                ag.draw(e);
            }
        }

        private void drawAllRadius(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Gray);
            Rectangle rec = new Rectangle();
            Brush br= new SolidBrush(Color.White);
            foreach (AgentEnv ag in agentListm)
            {
                rec.X = (Int32)((ag.getCoord().x - (ag.getRadius()/2.0f)) * (float)e.ClipRectangle.Width);
                rec.Y = (Int32)((ag.getCoord().y - (ag.getRadius() / 2.0f)) * (float)e.ClipRectangle.Height);
                rec.Width = (Int32)(ag.getRadius() * ((float)e.ClipRectangle.Width));
                rec.Height = (Int32)(ag.getRadius() * ((float)e.ClipRectangle.Height));
                Console.WriteLine("x " + rec.X.ToString() + " y " + rec.Y.ToString() + " W " + rec.Width.ToString() + " H " + rec.Height.ToString());
                e.Graphics.FillEllipse(br, rec);
            }
        }

        public void addAgent(ref AgentEnv ag)
        {
            Board me = this;
            agentListm.Add(ag);
            ag.setBoard(ref me);
            return;
        }

        public void launchAgents()
        {
            foreach (AgentEnv ag in agentListm)
            {
                ag.doSomething();
            }
        }

        private bool isPathLegal(Coordinates start, Coordinates finish)
        {
            return (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
        }
                

        //Interface elements for agents
        public bool askStep(Coordinates vec, float speedPercent, ref AgentEnv client)
        {
            Coordinates pos = client.getCoord(), des;
            float perc = Math.Min(Math.Max(0.0f, speedPercent), 1.0f);
            des = pos + (vec.normalize()) * (perc * client.getSpeed());
            if (isPathLegal(pos, des))
            {
                client.setCoord(des);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool canTouch(Coordinates point, ref AgentEnv client)
        {
            Coordinates pos = client.getCoord();
            return (isPathLegal(pos, point) && ((pos - point).norm() < touchDist));
        }

        //agentEnv functions


        //Functions for user
        public AgentEnv createDummy1()
        {
            AgentEnv env = new AgentEnv(0.5f, 0.5f, 0.02f, 0.1f, "Dummy");
            Agent bill = new Dummy1(ref env);
            env.setAgent(ref bill);
            addAgent(ref env);
            return env;
        }
    }
}
