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
        public float communicteRadius;
        public Coordinates coord;
        public float speed;
        public float viewRadius;

        public AgentInfo(Color colour_, float radius_, AgentType agType_, 
                        Int32 ID_, AgentState state_, float communicateRad_,
                        Coordinates coord_, float speed_, float viewRadius_)
        {
            agentID = ID_;
            agentColor = colour_;
            agentRadius = radius_;
            agentType = agType_;
            agentState = state_;
            communicteRadius = communicateRad_;
            coord = coord_;
            speed = speed_;
            viewRadius = viewRadius_;
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
    public enum AgentState { Searching, Find_Me, Found };
}