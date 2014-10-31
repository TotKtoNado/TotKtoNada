using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralPackage.Structures;

namespace GeneralPackage.Controller
{
    public interface ISensor
    {
        AgentCutaway[] agentsAround();
        //public Segment[] wallsAround();
    }
}
