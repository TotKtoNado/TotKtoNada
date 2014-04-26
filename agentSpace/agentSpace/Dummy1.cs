using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    class Dummy1 : DummyAgent
    {
        public Dummy1 (ref AgentEnv env) : base (ref env)
        {
        }

        public override void doSomething()
        {
            Coordinates vec = new Coordinates(0.5f,0.5f);
            //makeStep(vec, 1.0f);
        }
    }
}
