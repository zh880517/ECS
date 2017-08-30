using System;
namespace Logic.Base
{

    public enum DirectionType
    {
        Down,
        Up,
        Left,
        Right,
    }
    public struct Vector
    {
        public const float kEpsilon = 1E-05F;
        public static readonly Vector Zero = new Vector(0, 0);
        public static readonly Vector One = new Vector(1, 1);
        public static readonly Vector Down = new Vector(0, -1);
        public static readonly Vector Up = new Vector(0, 1);
        public static readonly Vector Left = new Vector(-1, 0);
        public static readonly Vector Right = new Vector(1, 0);
        public static readonly Vector[] Direction = new Vector[] { Down, Up, Left, Right};

        public float X;
        public float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public  float Dot(Vector vec)
        {
            return X * vec.X + Y * vec.Y;
        }

        public void Normalize()
        {
            float magnitude = Magnitude;
            if (magnitude > 1E-05f)
            {
                this /= magnitude;
            }
            else
            {
                this = Zero;
            }
        }
        
        public float Magnitude { get { return (float)Math.Sqrt(X * X + Y * Y); } }
        public float SqrMagnitude { get { return X * X + Y * Y; } }
        public Vector Normalized
        {
            get
            {
                Vector result = new Vector(X, Y);
                result.Normalize();
                return result;
            }
        }

        public Vector ClampMagnitude(float maxLength)
        {
            Vector result;
            if (SqrMagnitude > maxLength * maxLength)
            {
                result = Normalized * maxLength;
            }
            else
            {
                result = this;
            }
            return result;
        }

        public float Distance(Vector a)
        {
            return (a - this).Magnitude;
        }

        public float Angle(Vector to)
        {
            return (float)Math.Acos(Clamp(Normalized.Dot(to.Normalized), -1f, 1f)) * 57.29578f;
        }

        public float SignedAngle()
        {
            float angle = (float)Math.Acos(Clamp(Normalized.Dot(Right), -1f, 1f)) * 57.29578f; ;
            if (Y < 0)
                angle = -angle;
            return angle;
        }
        
        //求垂直向量
        public Vector Vertical(bool isClockwise)
        {
            if (isClockwise)
            {
                return new Vector(-Y, X);
            } 
            else
            {
                return new Vector(Y, -X);
            }
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        public static Vector Lerp(Vector a, Vector b, float t)
        {
            if (t < 0)
                t = 0;
            else if (t > 1)
                t = 1;

            return new Vector(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        //一个点绕另外一个点逆时针旋转一定角度后新的位置
        public static Vector Rotation(Vector from, Vector center, float angle)
        {
            Vector vOut = Zero;
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            vOut.X = (from.X - center.X) * cosA - (from.Y - center.Y) * sinA + center.X;
            vOut.Y = (from.Y - center.Y) * cosA + (from.X - center.X) * sinA + center.Y;
            return vOut;
        }

        public static Vector LerpUnclamped(Vector a, Vector b, float t)
        {
            return new Vector(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }
        public static Vector MoveTowards(Vector current, Vector target, float maxDistanceDelta)
        {
            Vector a = target - current;
            float magnitude = a.Magnitude;
            Vector result;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                result = target;
            }
            else
            {
                result = current + a / magnitude * maxDistanceDelta;
            }
            return result;
        }

        public static Vector Min(Vector lhs, Vector rhs)
        {
            return new Vector(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y));
        }

        public static Vector Max(Vector lhs, Vector rhs)
        {
            return new Vector(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y));
        }

        public override bool Equals(object other)
        {
            bool result;
            if (!(other is Vector))
            {
                result = false;
            }
            else
            {
                Vector vector = (Vector)other;
                result = (X.Equals(vector.X) && Y.Equals(vector.Y));
            }
            return result;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        #region 重载操作符
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator /(Vector a, float d)
        {
            return new Vector(a.X / d, a.Y / d);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.X, -a.Y);
        }

        public static Vector operator *(Vector a, float d)
        {
            return new Vector(a.X * d, a.Y * d);
        }

        public static Vector operator *(float d, Vector a)
        {
            return new Vector(a.X * d, a.Y * d);
        }

        public static bool operator ==(Vector lhs, Vector rhs)
        {
            return (lhs - rhs).SqrMagnitude < 9.99999944E-11f;
        }

        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }


}
