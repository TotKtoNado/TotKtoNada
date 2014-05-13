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
        private CheckBox showCells;
        private ObstacleList walls;

        public Board(ref PictureBox pic, ref CheckBox showRad, ref CheckBox showMatr)
        {
            generatorID = 0;
            agentWorld = pic;
            agentListm = new List<AgentEnv>();
            showRadius = showRad;
            showCells = showMatr;
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
            if (showCells.Checked)
            {
                drawAllMatrix(e);
            }
            foreach (AgentEnv ag in agentListm)
            {
                ag.draw(e);
            }
            walls.drawWalls(e);

        }

        public void drawAllMatrix(PaintEventArgs e)
        {
            //foreach (AgentEnv ag in agentListm)
            //{
            //    ag.drawMatrix(e);
            //}
            agentListm[2].drawMatrix(e);
        }

        private void drawAllRadius(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);
            Rectangle rec = new Rectangle();
            Brush br= new SolidBrush(Color.White);
            foreach (AgentEnv ag in agentListm)
            {
                //draw sight radius
                rec.X = (Int32)((ag.getCoord().x - (ag.getViewRadius())) * (float)e.ClipRectangle.Width);
                rec.Y = (Int32)((ag.getCoord().y - (ag.getViewRadius())) * (float)e.ClipRectangle.Height);
                rec.Width = (Int32)(ag.getViewRadius()*2f * ((float)e.ClipRectangle.Width));
                rec.Height = (Int32)(ag.getViewRadius() *2f* ((float)e.ClipRectangle.Height));
                e.Graphics.FillEllipse(br, rec);
                //draw communicate radius
                rec.X = (Int32)((ag.getCoord().x - (ag.getCommRadius())) * (float)e.ClipRectangle.Width);
                rec.Y = (Int32)((ag.getCoord().y - (ag.getCommRadius())) * (float)e.ClipRectangle.Height);
                rec.Width = (Int32)(ag.getCommRadius() * 2f * ((float)e.ClipRectangle.Width));
                rec.Height = (Int32)(ag.getCommRadius() * 2f * ((float)e.ClipRectangle.Height));
                e.Graphics.DrawEllipse(new Pen(Color.Red, 1f), rec);
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


        private bool canCommunicate (AgentEnv sender, AgentEnv reciever){
            return (sender.getCoord() - reciever.getCoord()).norm() < sender.getCommRadius();
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
                if (ag.getTypeName() == AgentType.Finder && agent.getTypeName() == AgentType.Finder &&
                    canCommunicate(agent, ag))
                {
                    rez.Add(ag.getCutaway());
                }

                else if ((ag.getCoord() - obs).norm() < agent.getViewRadius() &&
                      canSee(obs, ag.getCoord()) &&
                      ag.getID() != agent.getID())
                {
                    rez.Add(ag.getCutaway());
                }
            }
            return rez;
        }

        bool IAgentFunctions.takeObj(Int32 objId, ref AgentEnv agent)
        { //If agent can "take" object and he can touch that object, the object removes from the field
            AgentEnv billy = findAgent(objId);
            if (canTake(agent.getState(), billy.getState()) &&
                canTouch1(billy.getCoord(), ref agent))
            {
                billy.setState(AgentState.Found);
                billy.imageSetColor(Color.Black);
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


        List<Wall> IAgentFunctions.wallsAround(ref AgentEnv agent)
        {
            return walls.wallsAroundPoint(agent.getCoord(), agent.getViewRadius());
        }

        bool IAgentFunctions.sendCellMatrix(int recieverID, ref AgentEnv sender)
        {
            AgentEnv reciever = findAgent(recieverID);
            if (canCommunicate(sender, reciever) &&
                sender.getTypeName() == AgentType.Finder &&
                reciever.getTypeName() == AgentType.Finder)
            {
                SearchCells temp;
                sender.getCellMatrix(out temp);
                reciever.uniteMatrix(temp);
                return true;
            }
            return false;
        }
                

        
        //agentEnv functions


        //Functions for user
        public AgentEnv createDummy1()
        {
            AgentInfo info = new AgentInfo(Color.Green, 0.01f, AgentType.Dummy, generateID(), AgentState.Searching, Average.commRadius);
            AgentEnv env = new AgentEnv(0.5f, 0.5f, Average.speed, Average.viewRadius, info);
            Agent bill = new Dummy1(ref env);
            env.setAgent(bill);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createLittleGirl1() {
            AgentInfo info = new AgentInfo(Color.Pink, 0.005f, AgentType.Little_Girl, generateID(), AgentState.Find_Me, Average.commRadius);
            AgentEnv env = new AgentEnv(0.5f, 0.5f, Average.speed, Average.viewRadius, info);
            Agent nancy = new LittleGirl1(ref env);
            env.setAgent(nancy);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createFinder1()
        {
            AgentInfo info = new AgentInfo(Color.Magenta, 0.01f, AgentType.Finder, generateID(), AgentState.Searching,Average.commRadius);
            
            AgentEnv env = new AgentEnv(0.5f, 0.5f, Average.speed, Average.viewRadius, info); //причесать это и перенести в info
            Agent holmes = new Finder1(ref env);
            env.setAgent(holmes);
            addAgent(ref env);
            return env;
        }

        
        // WALLS functions
        public void addWall(Segment seg)
        {
            walls.addCommonWall(seg);
        }
    }
}
