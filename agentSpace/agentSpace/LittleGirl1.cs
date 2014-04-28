using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    class LittleGirl1 : LittleGirl
    {
        public LittleGirl1(ref AgentEnv env)
            : base(ref env)
        {
        }

        public override void doSomething()
        {
            //makeStep(new Coordinates(-0.5f,0.5f), 1.0f);
        }
    }
}
