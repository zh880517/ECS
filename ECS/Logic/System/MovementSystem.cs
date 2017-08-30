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
            entityMgr.ForEach((Entity Entity, TransformComponent trans, MovementComonent move) =>
            {
                if (move.Move.Type == MoveType.Straighton)
                {
                    StraightonMove movement = move.Move as StraightonMove;
                    float distance = movement.Speed * (timeDelta.Time - movement.Time);
                    if (distance <= 0)
                        return;

                    trans.Position = movement.StartPos + UtilsMove.Move(movement.Direction, distance);
                    trans.Rotation = Vector.Direction[(int)movement.Direction];
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
