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

        public void setController(IWalker com_) {
            com = com_;
            seted = true;
        }

        public override void makeTurn()
        {
            if (seted)
            {
                System.Console.Write(com.makeStep(new Coord(1.0, 0), 0.9));
            }
        }
    }
}
