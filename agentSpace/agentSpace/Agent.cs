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

    public class Agent
    {
        protected static Random randGen = new Random();
        private AgentEnv env;

        public Agent(ref AgentEnv env_)
        {
            env = env_;
        }



        //Functions that must be written for agents
        virtual public void doSomething () {
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

        protected Coordinates getMyPos()
        {
            return env.getCoord();
        }

    }
}
