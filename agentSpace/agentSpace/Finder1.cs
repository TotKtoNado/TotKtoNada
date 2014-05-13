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
            if (cells.setCell(getMyPos()))
            {
                cellsUpdated = true;
            }
            if (workWithObjectsAround())
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

        public override bool getCellMatrix(out SearchCells rez)
        {
            rez = cells;
            return true;
        }

        public override void uniteMatrix(SearchCells matr)
        {
            cells.uniteWith(matr);
            cellsUpdated = false;
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

        private bool workWithObjectsAround()
        {
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            bool communicate = false;
            foreach (AgentCutaway cut in cuts)
            {
                if (cut.state == AgentState.Find_Me)
                {
                    aim = cut.pos;
                    found = grabObject(cut.agentID);
                    return found;
                }
                else if (cut.type == AgentType.Finder && cellsUpdated)
                {
                    sendCellMatrix(cut.agentID);
                    communicate = true;
                }
            }
            if (communicate && cellsUpdated)
            {
                cellsUpdated = false;
            }
            return false;
        }


        public override void drawMatrix(PaintEventArgs e)
        {
            cells.drawMatrix(e);
        }

        
       
        
    }
}
