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
    public class Board : IAgentFunctions
    {
        private PictureBox agentWorld;
        private const float touchDist = 0.002f;
        private List<AgentEnv> agentListm;
        private Int32 generatorID;
        private CheckBox showRadius;
        private ObstacleList walls;

        public Board(ref PictureBox pic, ref CheckBox showRad)
        {
            generatorID = 0;
            agentWorld = pic;
            agentListm = new List<AgentEnv>();
            showRadius = showRad;
            walls = new ObstacleList();
        }

        private Int32 generateID()
        {
            generatorID++;
            return generatorID;
        }


        //drawings
        public void drawAll(PaintEventArgs e)
        {
            if (showRadius.Checked)
            {
                drawAllRadius(e);
            }
            foreach (AgentEnv ag in agentListm)
            {
                ag.draw(e);
            }
            walls.drawWalls(e);

        }

        private void drawAllRadius(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);
            Rectangle rec = new Rectangle();
            Brush br= new SolidBrush(Color.White);
            foreach (AgentEnv ag in agentListm)
            {
                rec.X = (Int32)((ag.getCoord().x - (ag.getViewRadius())) * (float)e.ClipRectangle.Width);
                rec.Y = (Int32)((ag.getCoord().y - (ag.getViewRadius())) * (float)e.ClipRectangle.Height);
                rec.Width = (Int32)(ag.getViewRadius()*2f * ((float)e.ClipRectangle.Width));
                rec.Height = (Int32)(ag.getViewRadius() *2f* ((float)e.ClipRectangle.Height));
                //Console.WriteLine("x " + rec.X.ToString() + " y " + rec.Y.ToString() + " W " + rec.Width.ToString() + " H " + rec.Height.ToString());
                e.Graphics.FillEllipse(br, rec);
            }
        }

        public void addAgent(ref AgentEnv ag)
        {
            Board me = this;
            agentListm.Add(ag);
            ag.setBoard(me);
            return;
        }

        public void removeAgent(ref AgentEnv ag)
        {
            agentListm.Remove(ag);
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
            bool noWalls = !walls.haveIntersections(new Segment(start, finish));
            bool inRange = (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
            return noWalls && inRange;
        }

        private bool canSee (Coordinates start, Coordinates finish) 
        {
            bool noWalls = !walls.haveIntersections(new Segment(start, finish));
            bool inRange = finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f;
            return noWalls && inRange;
        }

        private AgentEnv findAgent ( Int32 id) {
            return agentListm.Find(
                delegate(AgentEnv ag)
                {
                    return (ag.getID() == id);
                });
        }

        private bool canTake(AgentState takerState, AgentState objectState)
        {
            if (takerState == AgentState.Searching && objectState == AgentState.Find_Me)
            {
                return true;
            }
            return false;
        }

        bool canTouch1(Coordinates point, ref AgentEnv client)
        {
            Coordinates pos = client.getCoord();
            return (isPathLegal(pos, point) && ((pos - point).norm() < touchDist));
        }


        //Interface elements for agents
        bool IAgentFunctions.askStep(Coordinates vec, float speedPercent, ref AgentEnv client)
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

        bool IAgentFunctions.canTouch(Coordinates point, ref AgentEnv client)
        {
            return canTouch1(point, ref client);
        }

        List<AgentCutaway> IAgentFunctions.agentsInRange(ref AgentEnv agent)
        {//Returns list of Agents Cutaways, that asking-agent can see
            Coordinates obs = agent.getCoord();
            List<AgentCutaway> rez = new List<AgentCutaway>();
            foreach (AgentEnv ag in agentListm)
            {
                if ((ag.getCoord() - obs).norm() < agent.getViewRadius() &&
                      canSee(obs, ag.getCoord()) && 
                      ag.getID()!=agent.getID())
                {
                    //Console.WriteLine("Range = " + (ag.getCoord() - obs).x.ToString() + (ag.getCoord() - obs).y.ToString());
                    rez.Add(ag.getCutaway());
                }
            }
            return rez;
        }

        bool IAgentFunctions.takeObj(Int32 objId, ref AgentEnv agent)
        { //If agent can "take" object and he can touch that object, the object removes from the field
            AgentEnv billy = findAgent(objId);
            //Console.WriteLine("can take = " + canTake(agent.getState(), billy.getState()).ToString());
            //Console.WriteLine("can touch = " + canTouch(billy.getCoord(), ref agent));
            if (canTake(agent.getState(), billy.getState()) &&
                canTouch1(billy.getCoord(), ref agent))
            {
                //Console.WriteLine("Little girl = " + billy.getCoord().ToString());
                billy.setState(AgentState.Found);
                //removeAgent(ref billy);
                billy.imageSetColor(Color.Black);
                //billy.imageSetSize(0.1f);
                //Console.WriteLine("takeObj = true");
                return true;
            }
            else
            {
                //Console.WriteLine("takeObj = false");
                return false;
            }
        }

        void IAgentFunctions.changeAgentColor(Color col, ref AgentEnv agent)
        {
            agent.imageSetColor(col);
        }

        
        //agentEnv functions


        //Functions for user
        public AgentEnv createDummy1()
        {
            AgentInfo info = new AgentInfo (Color.Green, 0.01f, AgentType.Dummy,generateID(), AgentState.Searching);
            AgentEnv env = new AgentEnv(0.5f, 0.5f, 0.02f, 0.1f, info);
            Agent bill = new Dummy1(ref env);
            env.setAgent(bill);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createLittleGirl1() {
            AgentInfo info = new AgentInfo(Color.Pink, 0.005f, AgentType.Little_Girl, generateID(), AgentState.Find_Me);
            AgentEnv env = new AgentEnv(0.5f, 0.5f, 0.02f, 0.1f, info);
            Agent nancy = new LittleGirl1(ref env);
            env.setAgent(nancy);
            addAgent(ref env);
            return env;
        }

        
        // WALLS functions
        public void addWall(Segment seg)
        {
            walls.add(seg);
        }
    }
}
