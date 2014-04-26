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

        public AgentInfo(Color col, float rad, string agType)
        {
            agentColor = col;
            agentRadius = rad;
            agentType = agType;
        }
        
    }


}