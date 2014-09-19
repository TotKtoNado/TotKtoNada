using System;

namespace agentSpace
{
    public class Coordinates
    {
        public float x { get; set; }
        public float y { get; set; }
        
        private static Random rand = new Random();

        public Coordinates(float x_, float y_)
        {
            x = x_;
            y = y_;
        }

        public Coordinates()
        {
            x = 0;
            y = 0;
        }

        public static Coordinates randomCoord()
        {
            Coordinates coord = new Coordinates() ;
            coord.x = (float)rand.NextDouble();
            coord.y = (float)rand.NextDouble();
            return coord;
        }


        public static float area(Coordinates a, Coordinates b, Coordinates c)
        {
            return (b.x - a.x) * (c.y - a.y) * (b.y - a.y) * (c.x - a.x);
        }


        public Coordinates safeAssign(float x_, float y_)
        {
            x = Math.Max(Math.Min(x_, 1.0f), 0.0f);
            y = Math.Max(Math.Min(y_, 1.0f), 0.0f);
            return this;
        }

        public Coordinates toDiaposon()
        {
            x = Math.Max(Math.Min(x, 1.0f), 0.0f);
            y = Math.Max(Math.Min(y, 1.0f), 0.0f);
            return this;
        }


        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.x + b.x, a.y + b.y);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.x - b.x, a.y - b.y);
        }

        public static Coordinates operator *(Coordinates a, float b)
        {
            return new Coordinates(a.x *b, a.y *b);
        }

        public float norm() { //Length of vector
            return ((float)Math.Sqrt( x * x + y * y));
        }

        public Coordinates normalize()
        {
            if (x == 0.0f && y == 0.0f)
            {
                return this;
            }
            float div = (float)Math.Sqrt((x * x) + (y * y));
            x = x / div;
            y = y / div;
            return this;
        }

        public override string ToString()
        {
            return "Coordinates x = " + x.ToString() + ", y = " + y.ToString();
        }
        
           
    }
}
