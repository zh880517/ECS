namespace Logic.Base
{
    public struct AABB
    {
        public Vector Min;
        public Vector Max;

        public AABB(Vector min, Vector max)
        {
            Min = Vector.Min(min, max);
            Max = Vector.Max(min, max);
        }

        public AABB(Vector center, float width, float high)
        {
            Vector v = new Vector(width * 0.5f, high * 0.5f);
            Min = center - v;
            Max = center + v;
        }

        public bool Contains(AABB a)
        {
            return Min.X <= a.Min.X && Min.Y <= a.Min.Y && a.Max.X <= Max.X && a.Max.Y <= Max.Y;
        }

        public bool Overlap(AABB a)
        {
            Vector v1 = a.Min - Max;
            Vector v2 = Min - a.Max;
            if (v1.X > 0 || v1.Y > 0)
                return false;
            if (v2.X > 0 || v2.Y > 0)
                return false;
            return true;
        }

        public static AABB OffSet(AABB a, Vector v)
        {
            return new AABB(a.Min + v, a.Max + v);
        }

        
    }
}
