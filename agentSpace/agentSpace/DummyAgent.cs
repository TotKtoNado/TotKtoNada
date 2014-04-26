//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace agentSpace
//{
//    class DummyAgent : Agent
//    {
//        private Coordinates aim;
//        private bool haveAim;

//        public DummyAgent()
//            : base()
//        {
//            aim = new Coordinates(0.0f, 0.0f);
//        }

//        public DummyAgent(float x, float y, float speed_)
//            : base(x,y,speed_)
//        {
//            aim = new Coordinates(0.0f, 0.0f);
//        }

//        //individual
//        private void genAim(){
//            aim.x = (float) Agent.randGen.NextDouble();
//            aim.y = (float) Agent.randGen.NextDouble();

//            haveAim = true;
//        }

//        //overloaded
//        public override void doSomething()
//        {
//            Agent me = this;
//            if (!haveAim)
//            {
//                genAim();
//            }
//            Coordinates vec = aim - coord;
//            float perc = vec.norm() / speed;
//            Console.WriteLine("bas");
//            Console.WriteLine(perc);
//            if (server.canTouch(aim,ref me))
//            {
//                haveAim = false;
//                return;
//            }
//            else
//            {
//                perc = vec.norm() / speed;
//                Console.WriteLine("mod");
//                Console.WriteLine(perc);
//                Console.WriteLine("des");
//                Console.WriteLine(vec.norm());
//                server.askStep(vec, perc, ref me);
//            }
//        }
//    }
//}
