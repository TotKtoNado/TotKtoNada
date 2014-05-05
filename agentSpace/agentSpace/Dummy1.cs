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
            //Console.WriteLine("DoSomething()");
            if (!grabObjectsAround())
            {
                //Console.WriteLine("Can touch aim?");
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
            //Console.WriteLine("Grab objects Around()");
            List<AgentCutaway> cuts = lookAround();
            bool found = false;
            foreach (AgentCutaway cut in cuts) {
                if (cut.agentType == "Little Girl" && cut.state == "Find me")
                {
                   // aim = cut.pos;
                   // Console.WriteLine("Objectname = " + cut.state);
                    changeMyColor(Color.Blue);
                    setAim(cut.pos);
                    found = grabObject(cut.agentID);
                   // Console.WriteLine("Returned true");
                    return found;
                }
            }
            //Console.WriteLine("Returned false");
            return false;
        }


        private void genAim() {
           // Console.WriteLine("GenAim()");
            Coordinates coord = new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble());
            setAim(coord);
        }

        private void moveToAim()
        {
            //Console.WriteLine("MoveToAim()");
            Coordinates vec = aim - getMyPos();
            float perc = vec.norm()/getMySpeed();
            if (makeStep(vec, perc) == false)
            {
                genAim();
            }
        }
    }
}
