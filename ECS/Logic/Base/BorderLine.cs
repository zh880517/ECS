using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base
{
    public class BorderLine
    {
        private float min;
        private float max;

        public float Min { get { return min; } }
        public float Max { get { return max; } }

        public BorderLine(float min, float max)
        {
            if (min < max)
            {
                this.min = min;
                this.max = max;
            }
            else
            {
                this.min = max;
                this.max = min;
            }
        }
    }
    
    public class VerticalBorderLine : BorderLine
    {
        private float y;
        public float Y { get { return y; } }
        public VerticalBorderLine(float y, float min, float max) :base(min, max)
        {
            this.y = y;
        }
    }

    public class Horizontal : BorderLine
    {
        private float x;
        public float X { get { return x; } }
        public Horizontal(float x, float min, float max) : base(min, max)
        {
            this.x = x;
        }
    }
}
