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
    public struct AgentInfo
    {
        public Color agentColor;
        public float agentRadius;
        public string agentType;
        public Int32 agentID;
        public string agentState;

        public AgentInfo(Color col, float rad, string agType, Int32 ID, string state)
        {
            agentID = ID;
            agentColor = col;
            agentRadius = rad;
            agentType = agType;
            agentState = state;
        }
        
    }

    public struct AgentCutaway
    {
        public string agentType;
        public Int32 agentID;
        public Coordinates pos;
        public string state;

        public AgentCutaway(string agTy, Int32 ID, Coordinates pos_, string state_)
        {
            agentType = agTy;
            agentID = ID;
            pos = pos_;
            state = state_;
        }
    }


}