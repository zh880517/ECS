using Logic.Base;

namespace Logic.Utils
{
    public static class UtilsMove
    {
        public static Vector Move(DirectionType type, float distance)
        {
            Vector vOut = Vector.Zero;
            switch (type)
            {
                case DirectionType.Up:
                    vOut.Y = distance;
                    break;
                case DirectionType.Down:
                    vOut.Y = -distance;
                    break;
                case DirectionType.Left:
                    vOut.X = distance;
                    break;
                case DirectionType.Right:
                    vOut.X = -distance;
                    break;
            }
            return vOut;
        }

        public static DirectionType NegativeDirection(DirectionType type)
        {
            switch (type)
            {
                case DirectionType.Up:
                    return DirectionType.Down;
                case DirectionType.Down:
                    return DirectionType.Up;
                case DirectionType.Left:
                    return DirectionType.Right;
                default:
                    return DirectionType.Left;
            }
        }
        
    }
}
