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
        private Dictionary<Int16, Wall> list;
        private Int16 counter;

        public ObstacleList()
        {
            list = new Dictionary<Int16, Wall>();
            counter = 0;
        }

        private Int16 genNewKey()
        {
            return counter++;
        }

        public void addCommonWall (Segment seg) {
            list.Add(genNewKey(), new Wall(seg, WallType.Common_Wall));
        }

        public void remove(Int16 id)
        {
            list.Remove(id);
        }

        public bool haveIntersections (Segment path) {
            foreach (Wall wallStruct in list.Values) {
                if (wallStruct.type == WallType.Common_Wall &&
                    Segment.intersection(path,wallStruct.seg)) 
                {
                    return true;
                }
            }
            //Console.WriteLine("DON'T INTERSECT\n" + path.ToString());
            return false;
        }

        public void drawWalls(System.Windows.Forms.PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black, 2);
            int x1, y1, x2, y2;
            Segment seg;
            foreach (Wall wallStruct in list.Values)
            {
                if (wallStruct.type == WallType.Common_Wall)
                {
                    seg = wallStruct.seg;
                    x1 = (int)(seg.beg.x * (float)e.ClipRectangle.Width);
                    y1 = (int)(seg.beg.y * (float)e.ClipRectangle.Height);
                    x2 = (int)(seg.end.x * (float)e.ClipRectangle.Width);
                    y2 = (int)(seg.end.y * (float)e.ClipRectangle.Height);
                    e.Graphics.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
                }
            }
        }
    }
}
