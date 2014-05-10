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
        public AgentType agentType;
        public Int32 agentID;
        public AgentState agentState;

        public AgentInfo(Color col, float rad, AgentType agType, Int32 ID, AgentState state)
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
        public AgentType type;
        public Int32 agentID;
        public Coordinates pos;
        public AgentState state;

        public AgentCutaway(AgentType agTy, Int32 ID, Coordinates pos_, AgentState state_)
        {
            type = agTy;
            agentID = ID;
            pos = pos_;
            state = state_;
        }
    }



    public struct Wall
    {
        public Segment seg;
        public WallType type;

        public Wall(Segment seg_, WallType type_)
        {
            seg = seg_;
            type = type_;
        }
    }

    // Types
    public enum AgentType { Dummy, Little_Girl };
    public enum AgentState { Searching, Find_Me, Found };
}