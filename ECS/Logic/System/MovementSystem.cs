using Logic.Base;
using ECS;
using Logic.Utils;

namespace Logic
{
    class MovementSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {
            Update(entityMgr, timeDelta);
        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
            entityMgr.ForEach((Entity entity, TransformComponent trans, MovementComonent move) =>
            {
                if (move.Move.Type == MoveType.Straighton)
                {
                    StraightonMove movement = move.Move as StraightonMove;
                    float distance = movement.Speed * (timeDelta.Time - movement.Time);
                    if (distance <= 0)
                        return;

                    trans.Position = movement.StartPos + UtilsMove.Move(movement.Direction, distance);
                    trans.Rotation = Vector.Direction[(int)movement.Direction];
                    //此处根据原始位置和当前位置获取是否碰撞到不可通过的物体或者区域，如果有，则计算实际能够运动的距离
                    //根据实际能够运动的距离计算出应该返回的时间点（如果不是直接返回则需要添加一个反弹组件，待反弹组件运行结束，再给对象添加一个反向的运动组件）
                }
                else if (move.Move.Type == MoveType.Round)
                {
                    RoundMove movement = move.Move as RoundMove;
                    float angle = (timeDelta.Time - movement.Time) * movement.AngleSpeed;
                    if (angle > 360)
                        angle -= 360;
                    if (angle < -360)
                        angle += 360;
                    trans.Position = Vector.Rotation(movement.StartPos, movement.Center, angle);
                    trans.Rotation = (trans.Position - movement.Center).Vertical(movement.AngleSpeed < 0);
                }
            });
        }
    }
}
