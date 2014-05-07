using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    public struct Segment
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

        public static bool intersection2(Segment seg1, Segment seg2)
        {
            return seg1.intersects(seg2);
        }

        public static bool intersection(Segment seg1, Segment seg2)
        {
            Coordinates dir1 = seg1.end - seg1.beg;
            Coordinates dir2 = seg2.end - seg2.beg;

            //считаем уравнения прямых проходящих через отрезки
            float a1 = -dir1.y;
            float b1 = +dir1.x;
            float d1 = -(a1 * seg1.beg.x + b1 * seg1.beg.y);

            float a2 = -dir2.y;
            float b2 = +dir2.x;
            float d2 = -(a2 * seg2.beg.x + b2 * seg2.beg.y);

            //подставляем концы отрезков, для выяснения в каких полуплоскотях они
            float seg1_line2_start = a2 * seg1.beg.x + b2 * seg1.beg.y + d2;
            float seg1_line2_end = a2 * seg1.end.x + b2 * seg1.end.y + d2;

            float seg2_line1_start = a1 * seg2.beg.x + b1 * seg2.beg.y + d1;
            float seg2_line1_end = a1 * seg2.end.x + b1 * seg2.end.y + d1;

            if (lit(seg1_line2_end) || lit(seg1_line2_start) ||
                lit(seg2_line1_end) || lit(seg2_line1_start))
            {
                return true;
            }
            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
            {
                return false;
            }
            return true;
        }


        public static Segment randomSeg()
        {
            Coordinates beg = new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble());
            Coordinates end = (new Coordinates((float)randGen.NextDouble(), (float)randGen.NextDouble())).normalize();
            end = (end * 0.1f) + beg;
            return new Segment(beg, end);
        }

        public override string ToString()
        {
            return "Beggining:\n " + beg.ToString() + "\nEnd:\n " + end.ToString();
        }

        public static bool lit(float fl)// Less then epsilon
        {
            return (Math.Abs(fl) < float.Epsilon);
        }
    }
}
