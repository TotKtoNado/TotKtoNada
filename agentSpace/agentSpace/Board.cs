using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace agentSpace
{
    public class Board : IAgentFunctions
    {
        #region Constants

        private const float touchDist = 0.002f;

        #endregion

        private PictureBox picture;
        private List<AgentEnv> agentListm;
        private Int32 generatorID;
        private ObstacleList wallList;
        private CheckBox showRadius;
        private CheckBox showCells;

        #region Constructor
        public Board(ref PictureBox pic, ref CheckBox showRad, ref CheckBox showMatr)
        {
            generatorID = 0;
            picture = pic;
            agentListm = new List<AgentEnv>();
            showRadius = showRad;
            showCells = showMatr;
            wallList = new ObstacleList();
        }
        #endregion


        #region Functions for system

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
            wallList.drawWalls(e);
        }



        public void launchAgents()
        {//Gives each agent an opportunity to make an action
            foreach (AgentEnv ag in agentListm)
            {
                ag.doSomething();
            }
        }
        #endregion

        #region Draw functions

        private void drawAllMatrix(PaintEventArgs e)
        {//Draw matrix of search-map for the second agent
         //TODO: Add options to this function
            agentListm[2].drawMatrix(e);
        }

        private void drawAllRadius(PaintEventArgs e)
        {//Draws all agent view-fields
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

#endregion

        #region Action subfunctions

        private void addAgent(ref AgentEnv ag)
        {
            Board me = this;
            agentListm.Add(ag);
            ag.setBoard(me);
            return;
        }

        private void removeAgent(ref AgentEnv ag)
        {
            agentListm.Remove(ag);
        }

        

        private bool isPathLegal(Coordinates start, Coordinates finish)
        {//Checks if an agent can make step from start to finish postion
            bool noWalls = !wallList.haveIntersections(new Segment(start, finish));
            bool inRange = (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
            return noWalls && inRange;
        }

        private bool canSee (Coordinates start, Coordinates finish) 
        {//Checks if there is line of sight between two points
            bool noWalls = !wallList.haveIntersections(new Segment(start, finish));
            bool inRange = finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f;
            return noWalls && inRange;
        }


        private bool canCommunicate (AgentEnv sender, AgentEnv reciever)
        {//Checks if sender can send messages to reciever
            return (sender.getCoord() - reciever.getCoord()).norm() < sender.getCommRadius();
        }

        private AgentEnv findAgent ( Int32 id) 
        {
            return agentListm.Find(
                delegate(AgentEnv ag)
                {
                    return (ag.getID() == id);
                });
        }

        private bool canTake(AgentState takerState, AgentState objectState)
        {//Checks the compatibility of two agent types, when one trying to pick up another
            if (takerState == AgentState.Searching && objectState == AgentState.Find_Me)
            {
                return true;
            }
            return false;
        }

        bool canTouch1(Coordinates point, ref AgentEnv client)
        {//Checks if one agent is in proper distance to pick up another agent
            Coordinates pos = client.getCoord();
            return (isPathLegal(pos, point) && ((pos - point).norm() < touchDist));
        }

        #endregion


        #region IAgentFunctuins implementation

        bool IAgentFunctions.askStep(Coordinates direction, float speedPercent, ref AgentEnv client)
        {//Try to make step in specific direction. The length = max(speedPercent,1) * maxSpeed
            Coordinates pos = client.getCoord(), des;
            float perc = Math.Min(Math.Max(0.0f, speedPercent), 1.0f);
            des = pos + (direction.normalize()) * (perc * client.getSpeed());
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
        {//Check if the agent can grab object in specific point
            return canTouch1(point, ref client);
        }

        List<AgentCutaway> IAgentFunctions.agentsInRange(ref AgentEnv client)
        {//Returns list of Agents Cutaways of observable agents
            Coordinates obs = client.getCoord();
            List<AgentCutaway> rez = new List<AgentCutaway>();
            foreach (AgentEnv ag in agentListm)
            {
                if (ag.getTypeName() == AgentType.Finder && client.getTypeName() == AgentType.Finder &&
                    canCommunicate(client, ag))
                {
                    rez.Add(ag.getCutaway());
                }

                else if ((ag.getCoord() - obs).norm() < client.getViewRadius() &&
                      canSee(obs, ag.getCoord()) &&
                      ag.getID() != client.getID())
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
                return false;
            }
        }

        void IAgentFunctions.changeAgentColor(Color col, ref AgentEnv agent)
        {//Change own color
            agent.imageSetColor(col);
        }


        List<Wall> IAgentFunctions.wallsAround(ref AgentEnv agent)
        {// Returns List of observable walls
            return wallList.wallsAroundPoint(agent.getCoord(), agent.getViewRadius());
        }

        bool IAgentFunctions.sendCellMatrix(int recieverID, ref AgentEnv sender)
        {// Send own search matrix to another agent
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

        #endregion


        #region Functions for user


        //Initialize and add agents of corresponding types
        public AgentEnv createDummy1()
        {
            AgentInfo info = new AgentInfo(Color.Green, 0.01f, AgentType.Dummy, generateID(), AgentState.Searching, Average.commRadius,
                new Coordinates(0.5f, 0.5f), Average.speed / 5, Average.viewRadius);
            AgentEnv env = new AgentEnv(Average.speed, Average.viewRadius, info);
            Agent bill = new Dummy1(ref env);
            env.setAgent(bill);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createLittleGirl1()
        {
            AgentInfo info = new AgentInfo(Color.Pink, 0.005f, AgentType.Little_Girl, generateID(), AgentState.Find_Me, float.Epsilon,
                new Coordinates(0.5f, 0.5f), Average.speed / 5, Average.viewRadius);
            AgentEnv env = new AgentEnv( Average.speed, Average.viewRadius, info);
            Agent nancy = new LittleGirl1(ref env);
            env.setAgent(nancy);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createFinder1()
        {
            AgentInfo info = new AgentInfo(Color.Magenta, 0.01f, AgentType.Finder, generateID(),
                                            AgentState.Searching,Average.commRadius,
                                            new Coordinates(0.5f, 0.5f), Average.speed/5, Average.viewRadius);
            AgentEnv env = new AgentEnv(Average.speed/5, Average.viewRadius, info); //причесать это и перенести в info
            Agent holmes = new Finder1(ref env);
            env.setAgent(holmes);
            addAgent(ref env);
            return env;
        }


        public void addWall(Segment seg)
        {
            wallList.addCommonWall(seg);
        }
        #endregion


        #region Other functions
        private Int32 generateID()
        {
            generatorID++;
            return generatorID;
        }
        #endregion
    }
}
