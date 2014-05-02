﻿using System;
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
        private const float touchDist = 0.002f;
        private List<AgentEnv> agentListm;
        private Int32 generatorID;
        private CheckBox showRadius;

        public Board(ref PictureBox pic, ref CheckBox showRad)
        {
            generatorID = 0;
            agentWorld = pic;
            agentListm = new List<AgentEnv>();
            showRadius = showRad;
        }

        private Int32 generateID()
        {
            generatorID++;
            return generatorID;
        }


        //drawings
        public void drawAll(PaintEventArgs e)
        {
            if (showRadius.Checked)
            {
                drawAllRadius(e);
            }
            foreach (AgentEnv ag in agentListm)
            {
                ag.draw(e);
            }
        }

        private void drawAllRadius(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);
            Rectangle rec = new Rectangle();
            Brush br= new SolidBrush(Color.White);
            foreach (AgentEnv ag in agentListm)
            {
                rec.X = (Int32)((ag.getCoord().x - (ag.getViewRadius())) * (float)e.ClipRectangle.Width);
                rec.Y = (Int32)((ag.getCoord().y - (ag.getViewRadius())) * (float)e.ClipRectangle.Height);
                rec.Width = (Int32)(ag.getViewRadius()*2f * ((float)e.ClipRectangle.Width));
                rec.Height = (Int32)(ag.getViewRadius() *2f* ((float)e.ClipRectangle.Height));
                //Console.WriteLine("x " + rec.X.ToString() + " y " + rec.Y.ToString() + " W " + rec.Width.ToString() + " H " + rec.Height.ToString());
                e.Graphics.FillEllipse(br, rec);
            }
        }

        public void addAgent(ref AgentEnv ag)
        {
            Board me = this;
            agentListm.Add(ag);
            ag.setBoard(ref me);
            return;
        }

        public void removeAgent(ref AgentEnv ag)
        {
            agentListm.Remove(ag);
        }

        public void launchAgents()
        {
            foreach (AgentEnv ag in agentListm)
            {
                ag.doSomething();
            }
        }

        private bool isPathLegal(Coordinates start, Coordinates finish)
        {
            return (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
        }

        private bool canSee (Coordinates start, Coordinates finish) 
        {
            return (finish.x < 1.0f && finish.x > 0.0f && finish.y < 1.0f && finish.y > 0.0f);
        }

        private AgentEnv findAgent ( Int32 id) {
            return agentListm.Find(
                delegate(AgentEnv ag)
                {
                    return (ag.getID() == id);
                });
        }

        private bool canTake(string takerState, string objectState)
        {
            if (takerState == "Searching" && objectState == "Find me")
            {
                return true;
            }
            return false;
        }


        //Interface elements for agents
        public bool askStep(Coordinates vec, float speedPercent, ref AgentEnv client)
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
 
        public bool canTouch(Coordinates point, ref AgentEnv client)
        {
            Coordinates pos = client.getCoord();
            return (isPathLegal(pos, point) && ((pos - point).norm() < touchDist));
        }

        public List<AgentCutaway> objectsInRange(ref AgentEnv agent)
        {//Returns list of Agents Cutaways, that asking-agent can see
            Coordinates obs = agent.getCoord();
            List<AgentCutaway> rez = new List<AgentCutaway>();
            foreach (AgentEnv ag in agentListm)
            {
                if ((ag.getCoord() - obs).norm() < agent.getViewRadius() &&
                      canSee(obs, ag.getCoord()) && 
                      ag.getID()!=agent.getID())
                {
                    //Console.WriteLine("Range = " + (ag.getCoord() - obs).x.ToString() + (ag.getCoord() - obs).y.ToString());
                    rez.Add(ag.getCutaway());
                }
            }
            return rez;
        }
       
        public bool takeObj(Int32 objId, ref AgentEnv agent)
        { //If agent can "take" object and he can touch that object, the object removes from the field
            AgentEnv billy = findAgent(objId);
            //Console.WriteLine("can take = " + canTake(agent.getState(), billy.getState()).ToString());
            //Console.WriteLine("can touch = " + canTouch(billy.getCoord(), ref agent));
            if (canTake(agent.getState(), billy.getState()) &&
                canTouch(billy.getCoord(), ref agent))
            {
                //Console.WriteLine("Little girl = " + billy.getCoord().ToString());
                billy.setState("Found");
                //removeAgent(ref billy);
                billy.imageSetColor(Color.Black);
                //billy.imageSetSize(0.1f);
                //Console.WriteLine("takeObj = true");
                return true;
            }
            else
            {
                //Console.WriteLine("takeObj = false");
                return false;
            }
        }

        public void changeAgentColor(Color col, ref AgentEnv agent)
        {
            agent.imageSetColor(col);
        }

        
        //agentEnv functions


        //Functions for user
        public AgentEnv createDummy1()
        {
            AgentInfo info = new AgentInfo (Color.Green, 0.01f, "Dummy",generateID(), "Searching");
            AgentEnv env = new AgentEnv(0.5f, 0.5f, 0.02f, 0.1f, info);
            Agent bill = new Dummy1(ref env);
            env.setAgent(ref bill);
            addAgent(ref env);
            return env;
        }

        public AgentEnv createLittleGirl1() {
            AgentInfo info = new AgentInfo(Color.Pink, 0.005f, "Little Girl", generateID(), "Find me");
            AgentEnv env = new AgentEnv(0.5f, 0.5f, 0.02f, 0.1f, info);
            Agent nancy = new LittleGirl1(ref env);
            env.setAgent(ref nancy);
            addAgent(ref env);
            return env;
        }
    }
}
