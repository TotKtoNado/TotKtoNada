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
    class ObstacleList
    {
        private Dictionary<Int16, Segment> list;
        private Int16 counter;

        public ObstacleList()
        {
            list = new Dictionary<Int16, Segment>();
            counter = 0;
        }

        private Int16 genNewKey()
        {
            return counter++;
        }

        public void add (Segment seg) {
            list.Add(genNewKey(), seg);
        }

        public void remove(Int16 id)
        {
            list.Remove(id);
        }

        public bool haveIntersections (Segment path) {
            foreach (Segment wall in list.Values) {
                if (path.intersects(wall)) {
                    return true;
                }
            }
            return false;
        }

        public void drawWalls(System.Windows.Forms.PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black, 2);
            int x1, y1, x2, y2;
            foreach (Segment wall in list.Values)
            {
                x1 = (int)(wall.beg.x * (float)e.ClipRectangle.Width);
                y1 = (int)(wall.beg.y * (float)e.ClipRectangle.Height);
                x2 = (int)(wall.end.x * (float)e.ClipRectangle.Width);
                y2 = (int)(wall.end.y * (float)e.ClipRectangle.Height);
                e.Graphics.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
            }
        }
    }
}
