﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    public class DummyAgent : Agent
    {
        private Coordinates aim;

        public DummyAgent(ref AgentEnv env)
            : base(ref env)
        {
        }

    }
}
