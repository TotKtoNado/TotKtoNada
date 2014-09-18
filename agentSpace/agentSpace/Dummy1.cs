using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace agentSpace
{
    class Dummy1 : DummyAgent
    {
        protected static Random randGen = new Random();
        private Coordinates aim = new Coordinates ();


        public Dummy1 (ref AgentEnv env) 
            : base (ref env)
        {
            genAim();
        }


        public override void doSomething()
        {
            wallsAround();
            if (!grabObjectsAround())
            {
                if (canTouch(aim))
                {
                    genAim();
                }
                else
                {
                    moveToAim();
                }
            }
        }

        private void setAim(Coordinates coord)
        {
            aim = coord;
        }

        private bool grabObjectsAround() {
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            foreach (AgentCutaway cut in cuts) {
                if (cut.type == AgentType.Little_Girl && cut.state == AgentState.Find_Me)
                {
                    changeMyColor(Color.Blue);
                    setAim(cut.pos);
                    found = grabObject(cut.agentID);
                    return found;
                }
            }
            return false;
        }


        private void genAim() {
            Coordinates coord = new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble());
            setAim(coord);
        }

        private void moveToAim()
        {
            Coordinates vec = aim - getMyPos();
            float perc = vec.norm()/getMySpeed();
            if (makeStep(vec, perc) == false)
            {
                genAim();
            }
        }
    }
}
