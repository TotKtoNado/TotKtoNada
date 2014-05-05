using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace agentSpace
{
    public interface IAgentFunctions
    {
        bool askStep(Coordinates vec, float speedPercent, ref AgentEnv client);

        bool canTouch(Coordinates point, ref AgentEnv client);

        List<AgentCutaway> agentsInRange(ref AgentEnv agent);

        bool takeObj(Int32 objId, ref AgentEnv agent);

        void changeAgentColor(Color col, ref AgentEnv agent);
    }
}
