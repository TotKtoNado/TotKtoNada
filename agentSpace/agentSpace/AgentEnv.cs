using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace agentSpace
{ 
    public class AgentEnv
    //This class stores properties of agent
    {
        //private Coordinates coord;
        //private float speed;
        //private float viewRadius;

        private IAgentFunctions server;
        private Agent agent;
        private static Random randGen = new Random();
        private AgentInfo info;

     
        public AgentEnv()
        {
            float x = (float) randGen.NextDouble();
            float y = (float) randGen.NextDouble();
            info.coord = new Coordinates(x, y);
            info.speed = 0.02f;
        }

        public AgentEnv(float speed_, float radius, AgentInfo info_)
        {
            info.speed = speed_;
            info.viewRadius = radius;
            info = info_;
        }

        //drawings
        public void draw(System.Windows.Forms.PaintEventArgs e)
        {
            float drawRad = info.agentRadius;
            int x = (int)((info.coord.x - drawRad/2.0f)* e.ClipRectangle.Width ) ;
            int y = (int)((info.coord.y - drawRad / 2.0f) * e.ClipRectangle.Height);
            int w = (Int32)(drawRad * e.ClipRectangle.Width);
            int h = (Int32)(drawRad * e.ClipRectangle.Height);
            Rectangle rec = new Rectangle(x, y, w, h);
            Pen p = new Pen(info.agentColor, 3);
            e.Graphics.DrawEllipse(p, rec);

            //dispose pen and graphics object
            p.Dispose();

        }

        public void drawMatrix(System.Windows.Forms.PaintEventArgs e)
        {
            if (getTypeName() == AgentType.Finder)
            {
                agent.drawMatrix(e);
            }
        }

        #region Setters / getters

        public void setBoard(IAgentFunctions ser)
        {
            server = ser;
        }

        public IAgentFunctions getBoard()
        {
            return server;
        }

        public void setAgent(Agent ag)
        {
            agent = ag;
        }

        public void setCoord(Coordinates coord_)
        {
            info.coord.safeAssign(coord_.x, coord_.y);
        }

        public Coordinates getCoord()
        {
            return info.coord;
        }

        public void setSpeed(float speed_)
        {
            info.speed = speed_;
        }

        public float getSpeed()
        {
            return info.speed;
        }

        public void setViewRadius(float radius_)
        {
            info.viewRadius = radius_;
        }
        public float getViewRadius()
        {
            return info.viewRadius;
        }

        public float getCommRadius()
        {
            return info.communicteRadius;
        }

        public AgentCutaway getCutaway()
        {
            AgentCutaway visit = new AgentCutaway(info.agentType, info.agentID, getCoord(), info.agentState);
            return visit;
        }

        public Int32 getID()
        {
            return info.agentID;
        }

        public AgentType getTypeName()
        {
            return info.agentType;
        }

        public AgentState getState()
        {
            return info.agentState;
        }

        public void setState(AgentState state)
        {
            info.agentState = state;
        }
        
        #endregion

        #region Immage settings


        public void imageSetColor(Color col)
        {
            info.agentColor = col;
        }

        public void imageSetSize(float rad)
        {
            info.agentRadius = rad;
        }


        #endregion
        

        public void doSomething() 
        {
            agent.doSomething();
        }

        public bool getCellMatrix(out SearchCells output)
        {
            return agent.getCellMatrix(out output);
        }

        public void uniteMatrix(SearchCells matrix)
        {
            agent.uniteMatrix(matrix);
        }


    }
}
