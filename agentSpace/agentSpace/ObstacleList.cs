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

        public List<Wall> wallsAroundPoint(Coordinates center, float radius)
        {
            List<Wall> rez = new List<Wall>();
            Coordinates a = new Coordinates();
            Coordinates b = new Coordinates();
            Segment deleteThis = new Segment();
            foreach (Wall wall in list.Values)
            {
                if (segmentCircleIntersection(wall.seg, center, radius, ref a, ref b))
                {
                    //rez.Add(new Wall(middleSeg(wall.seg.beg, wall.seg.end, a,b), wall.type));
                    deleteThis = middleSeg(wall.seg.beg, wall.seg.end, a, b);
                    rez.Add(new Wall(deleteThis,wall.type));
                    //Console.WriteLine("So");
                    //Console.WriteLine("Center: " + center.ToString() + ", Radius =" + radius.ToString());
                    //Console.WriteLine("Wall:" + wall.seg.ToString());
                    //Console.WriteLine("Result:" +);
                }
            }

            return rez;
        }

        public static bool segmentCircleIntersection(Segment seg, Coordinates center, float radius, ref Coordinates a, ref Coordinates b)
        {
            bool rez = false;
            //shift segment
            Segment newSeg = seg;
            newSeg.beg = newSeg.beg - center;
            newSeg.end = newSeg.end - center;
            //vector
            Coordinates newC = seg.end - seg.beg;
            if (Math.Abs(newC.x) < float.Epsilon)
            {
                rez = segCirInt(1f, 0f, newSeg.beg.x, radius, ref a, ref b);
            }
            else
            {
                newC.x = newC.y/newC.x;
                newC.y = (-1.0f);
                float C = -(newC.x * newSeg.beg.x + newC.y * newSeg.beg.y);
                rez = segCirInt(newC.x, newC.y,C, radius,ref a, ref b);
            }
            if (rez)
            {
                a = a + center;
                b = b + center;
            }
            return rez;
        }


        public static bool segCirInt(float A, float B, float C, float radius,
                                       ref Coordinates first, ref Coordinates second)
        {
            float x0 = -A * C / (A * A + B * B), y0 = -B * C / (A * A + B * B);
            if (C * C >= radius * radius * (A * A + B * B) + float.Epsilon)
            {
                return false;
            }
            else
            {
                float d = radius * radius - C * C / (A * A + B * B);
                float mult = (float)Math.Sqrt(d / (A * A + B * B));
                first.x = x0 + B * mult;
                first.y = y0 - A * mult;
                second.x = x0 - B * mult;
                second.y = y0 + A * mult;
                return true;
            }
        }

        private static Segment middleSeg(Coordinates beg, Coordinates end, Coordinates a, Coordinates b)
        {//Возвращает отрезок образованный двумя средними точками из указанных трёх
            List<Coordinates> list = new List<Coordinates>();
            list.Add(beg);
            list.Add(end);
            list.Add(a);
            list.Add(b);
            if (Math.Abs(beg.x - end.x) < float.Epsilon)
            {
                list.Sort(compareByY);
            }
            else
            {
                list.Sort(compareByX);
            }
            return new Segment(list[1], list[2]);
        }

        private static int compareByX(Coordinates a, Coordinates b)
        {
            if (a.x < b.x)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private static int compareByY(Coordinates a, Coordinates b)
        {
            if (a.y < b.y)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
