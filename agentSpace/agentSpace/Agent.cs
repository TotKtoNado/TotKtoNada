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
    public enum AgentType { Dummy, Little_Girl, Finder };

    public class Agent
    {
        private AgentEnv env;

        public Agent(ref AgentEnv env_)
        {
            env = env_;
        }



        //Functions that must be written for agents
        virtual public void doSomething () {
        }

        virtual public void drawMatrix(PaintEventArgs e)
        {
        }

        virtual public bool getCellMatrix(out SearchCells output)
        {
            output = new SearchCells();
            return false;
        }

        virtual public void uniteMatrix(SearchCells matr)
        {
        }

        //Interface for Agents

        //make step in direction 'vec', using 'speedPerc' of your speed
        protected bool makeStep(Coordinates vec, float speedPerc)
        {
            return env.getBoard().askStep(vec, speedPerc, ref env);
        }

        protected float getMySpeed()
        {
            return env.getSpeed();
        }

        protected float getMyViewRadius()
        {
            return env.getViewRadius();
        }

        protected bool canTouch(Coordinates pos)
        {
            return env.getBoard().canTouch(pos, ref env);
        }

        protected Coordinates getMyPos()
        {
            return env.getCoord();
        }

        protected List<AgentCutaway> lookAround()
        {
            return env.getBoard().agentsInRange(ref env);
        }

        protected bool grabObject(Int32 objID)
        {
            return env.getBoard().takeObj(objID, ref env);
        }

        protected void changeMyColor(Color col)
        {
            env.getBoard().changeAgentColor(col, ref env);
        }

        protected List<Wall> wallsAround()
        {
            return env.getBoard().wallsAround(ref env);
        }

        protected bool sendCellMatrix(int agentID)
        {
            return env.getBoard().sendCellMatrix(agentID, ref env);
        }

    }
}
