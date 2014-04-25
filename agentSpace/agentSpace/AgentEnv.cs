using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{ 
    class AgentEnv
    //This class stores properties of agent
    {
        Coordinates coord;
        float speed;
        Board server;
        static Random randGen = new Random();

        public AgentEnv()
        {
            float x = (float) randGen.NextDouble();
            float y = (float) randGen.NextDouble();
            coord = new Coordinates(x, y);
            speed = 0.02f;
        }

        public AgentEnv(float x, float y, float speed_)
        {
            coord = new Coordinates(x, y);
            speed = speed_;
        }
    }
}
