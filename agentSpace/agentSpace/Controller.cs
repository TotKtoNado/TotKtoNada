using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    class Controller : IWalker
    {
        private AgentEnv env;

        
        protected Controller(ref AgentEnv env_)
        {
            env = env_;
        }

        #region AgentActions

        public bool makeStep(Coordinates vec, float speedPerc)
        {
            return env.getBoard().askStep(vec, speedPerc, ref env);
        }

        #endregion

    }
}
