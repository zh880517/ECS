using ECS;
using Logic.Base;

namespace Logic
{
    public class CollideSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {
            SceneRootComponent root = entityMgr.GetComponent<SceneRootComponent>();
            entityMgr.ForEach((Entity entity, TransformComponent trans, MovementComonent move, CollideComponent collide) =>
            {
                //collide.HitEntity.Clear();
                AABB curAABB = AABB.OffSet(collide.Box, trans.Position);
                collide.TouchingCells.Clear();
                root.Root.GetTouchingCell(curAABB, collide.TouchingCells);
                if (!collide.CenterCell.IsIn(trans.Position))
                {
                    foreach (var cell in collide.TouchingCells)
                    {
                        if (cell.IsIn(trans.Position))
                        {
                            collide.CenterCell = cell;
                            break;
                        }
                    }
                }
            });

        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
        }
    }
}
