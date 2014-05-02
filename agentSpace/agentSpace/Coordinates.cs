using System;

namespace agentSpace
{
    public class Coordinates
    {
        public float x { get; set; }
        public float y { get; set; }
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
