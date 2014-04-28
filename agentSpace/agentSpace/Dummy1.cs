using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                makeStep(new Coordinates(0.5f, 0.5f), 1.0f);
            }
        }

        private bool grabObjectsAround() {
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            foreach (AgentCutaway cut in cuts) {
                if (cut.agentType == "Little Girl")
                {
                    Console.WriteLine("found girl");
                    aim = cut.pos;
                    //found = grabObject(cut.agentID);
                    //Console.WriteLine(found);
                    return found;
                }
            }
            if (canTouch(aim))
            {
                genAim();
            }
            moveToAim();
            return false;
        }


        private void genAim() {
            aim.x = (float)randGen.NextDouble();
            aim.x = (float)randGen.NextDouble();
        }
        private void moveToAim()
        {
            Coordinates vec = aim - getMyPos();
            makeStep(vec.normalize(), 1.0f);
        }
    }
}
