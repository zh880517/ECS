using System;

namespace Logic.Base
{
    public enum MoveType
    {
        Straighton,
        Round,
    }

    public class Movement
    {
        //移动的方式
        protected MoveType type;

        //起始位置
        public Vector StartPos { get; set; }

        //移动速度
        public float Speed { get; set; } 

        //开始这个状态的时间
        public float Time { get; set; }
        //移动类型
        public MoveType Type { get { return type; } }
    }
    
    public class StraightonMove : Movement
    {
        public StraightonMove()
        {
            type = MoveType.Straighton;
        }

        public DirectionType Direction { get; set; }
    }

    public class RoundMove : Movement
    {
        public RoundMove()
        {
            type = MoveType.Round;
        }
        //围绕的中心
        public Vector Center { get; set; }
        //角度速度，正数为顺时针方向，负数为逆时针方向
        public float AngleSpeed { get; set; }

        public static RoundMove Make(Vector start, Vector center, float time, float speed, bool isClockWise)
        {
            RoundMove move = new RoundMove();
            float fRadius = start.Distance(center);
            float fDistance = fRadius * (float)Math.PI * 2;
            move.AngleSpeed = fDistance / (speed * 360);
            if (isClockWise)
                move.AngleSpeed = -move.AngleSpeed;
            move.Center = center;
            move.StartPos = start;
            move.Time = time;
            move.Speed = speed;

            return move;
        }
    }
    
}
