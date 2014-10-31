using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralPackage.Controller;
using GeneralPackage.GameData;
using GeneralPackage.Structures;

namespace SearchcraftLauncher
{
    class SimpleAgent : Eventer
    {
        private IWalker com;
        bool seted = false;

        public void setController(ControllerSet com_) {
            com = com_.iWalker;
            seted = true;
        }

        public override void makeTurn()
        {
            if (seted)
            {
                com.makeStep(new Coord(1.0, 0), 0.9);
            }
        }
    }
}
