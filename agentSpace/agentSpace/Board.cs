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
    public class Board
    {
        private PictureBox agentWorld;
        private const float touchDist = 0.01f;

        //old
        private List<Agent> agentList;
        //new
        private List<AgentEnv> agentListm;
        
        //old
        public Board(ref PictureBox pic, ref List<Agent> list)
        {
            agentWorld = pic;
            agentList = list;
            agentListm = new List<AgentEnv>();
        }

        //old
        public void addAgent(ref Agent ag)
        {
            Board me = this;
            agentList.Add (ag);
            ag.setBoard(ref me);
            return; 
        }
        //new
        public void addAgentm(ref AgentEnv ag)
        {
            Board me = this;
            agentListm.Add(ag);
            ag.setBoard(ref me);
            return;
        }

        //Let each agent do 1 action
        public void launchAgents()
        {
            foreach (Agent ag in agentList)
            {
                ag.doSomething();
            }
        }



        public string agentCoord(int agentID)
        {
            return agentList[agentID].getCoord().x.ToString() + " ; " + agentList[agentID].getCoord().y.ToString();
        }


        private bool isPathLegal(Coordinates start, Coordinates finish)
        {
            return (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
        }
                

        //Interface elements for agents
        public bool askStep(Coordinates vec, float speedPercent, ref Agent client)
        {
            Coordinates pos = client.getCoord(), des;
            float perc = Math.Min(Math.Max(0.0f, speedPercent), 1.0f);
            des = pos + (vec.normalize()) * (perc * client.getSpeed());
            if (isPathLegal(pos, des))
            {
                client.setCoord(des);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool canTouch(Coordinates point, ref Agent client)
        {
            Coordinates pos = client.getCoord();
            return (isPathLegal(pos, point) && ((pos - point).norm() < touchDist));
        }

        //agentEnv functions

    }
}
