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
    public partial class mainForm : Form
    {
        private Board board;
        AgentEnv bill1;
        public mainForm()
        {
            InitializeComponent();
            board = new Board(ref Field);
            Coordinates pos = new Coordinates(0.3f, 0.4f);
            bill1 = board.createDummy1();
            //AgentEnv bill2 = board.createDummy1();
            //AgentEnv bill3 = board.createDummy1();
            AgentEnv nancy = board.createLittleGirl1();
            bill1.setCoord(pos);
            //bill2.setCoord(pos* 2.0f);
            //bill3.setCoord(pos * 0.75f);
            nancy.setCoord(new Coordinates(0.9f, 0.8f));
        }


        //handlers
        private void Field_Click(object sender, EventArgs e)
        {
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            //foreach (Agent agent in list)
            //{
            //    agent.draw(e);
            //}
            board.drawAll(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            board.launchAgents();

            label1.Text = "";
            //label1.Text = label1.Text + "\nbill.getCoord() : " + list[0].getCoord().x.ToString() + " ; " + list[0].getCoord().y.ToString();
            //board.drawAll(e);
            Field.Refresh();
        }
    }


    public class Element
    {
        public virtual void draw(PaintEventArgs e)
        {
        }
    }
}





