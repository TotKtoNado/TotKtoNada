using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    public class Segment
    {
        public Coordinates beg;
        public Coordinates end;
        static Random randGen = new Random();


        public Segment(Coordinates beg_, Coordinates end_)
        {
            beg = beg_;
            end = end_;
        }

        public Segment(float begX, float begY, float endX, float endY)
        {
            beg = new Coordinates(begX, begY);
            end = new Coordinates(endX, endY);
        }

        private bool intersect1(float a1, float b1, float c1, float d1)
        {
            float a = Math.Min(a1, b1), b = Math.Max(a1, b1), c = Math.Min(c1, d1), d = Math.Max(c1, d1);
            return Math.Max(a, c) <= Math.Min(b, d);
        }

        public bool intersects(Segment seg)
        {
            Coordinates a = beg, b = end, c = seg.beg, d = seg.end;
            return intersect1(a.x, b.x, c.x, d.x)
                && intersect1(a.y, b.y, c.y, d.y)
                && Coordinates.area(a, b, c) * Coordinates.area(a, b, d) <= float.Epsilon
                && Coordinates.area(c, d, a) * Coordinates.area(c, d, b) <= float.Epsilon;
        }

        public static Segment randomSeg()
        {
            Coordinates beg = new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble());
            Coordinates end = (new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble())).normalize();
            end = (end * 0.1f) + beg;
            return new Segment(beg, end);
        }
    }
}
