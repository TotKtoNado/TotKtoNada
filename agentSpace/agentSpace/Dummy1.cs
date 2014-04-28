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
        private Coordinates aim = new Coordinates (0.5f,0.5f);


        public Dummy1 (ref AgentEnv env) 
            : base (ref env)
        {
        }


        public override void doSomething()
        {
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

        private bool grabObjectsAround() {
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            foreach (AgentCutaway cut in cuts) {
                Console.WriteLine(cuts.Count.ToString());
                if (cut.agentType == "Little Girl")
                {
                   // aim = cut.pos;
                    changeMyColor(Color.Blue);
                    aim = getMyPos(); 
                    found = grabObject(cut.agentID);
                    return found;
                }
            }
            return false;
        }


        private void genAim() {
            aim.x = (float)randGen.NextDouble();
            aim.y = (float)randGen.NextDouble();
        }
        private void moveToAim()
        {
            Coordinates vec = aim - getMyPos();
            makeStep(vec, 1.0f);
            //makeStep(new Coordinates(1.0f,1.0f),1.0f);
        }
    }
}
