using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    public interface IWalker
    {
        bool makeStep(Coordinates vec, float speedPerc);
    }
}
