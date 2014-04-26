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
        private AgentEnv env;

        public Agent(ref AgentEnv env_)
        {
            env = env_;
        }

    }
}
