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
    public class Finder1 : DummyAgent
    {


        private static Random randGen = new Random();

        private SearchCells cells;
        private Coordinates aim = new Coordinates();

        private bool cellsUpdated = false;

        public Finder1(ref AgentEnv env) : base(ref env) {
            cells = new SearchCells(getMyViewRadius());
            genAim();
        }

        public override void doSomething()
        {
            cells.setCell(getMyPos(), CellState.Discovered);
            if (grabObjectsAround())
            {
                return;
            }
            else if (aimReached())
            {
                if (!genAim())
                {//если цели больше не генирируются, можно ничего не делать
                    return;
                }
            }
            else if (!moveToAim())
            {
                //если не смог сделать шаг, генирурем новую цель
                if (!genAim())
                {
                    return;
                }
            }
        }

        private bool genAim()
        {
            aim = cells.getUndiscoveredCell();
            return true; //TODO: Delete this comment
        }

        private bool moveToAim()
        {
            Coordinates vec = aim - getMyPos();
            float perc = vec.norm() / getMySpeed();
            return makeStep(vec, perc);
        }

        private bool aimReached()
        {
            return (aim - getMyPos()).norm() < float.Epsilon;
        }

        private bool grabObjectsAround()
        {
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            foreach (AgentCutaway cut in cuts)
            {
                if (cut.type == AgentType.Little_Girl && cut.state == AgentState.Find_Me)
                {
                    aim = cut.pos;
                    found = grabObject(cut.agentID);
                    return found;
                }
            }
            return false;
        }

        public override void drawMatrix(PaintEventArgs e)
        {
            cells.drawMatrix(e);
        }
        
    }
}
