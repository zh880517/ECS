using ECS;
using Logic.Base;

namespace Logic
{
    public class CollideSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {
            entityMgr.ForEach((Entity entity, TransformComponent trans, MovementComonent move, CollideComponent collide) =>
            {
                //collide.HitEntity.Clear();
                AABB curAABB = AABB.OffSet(collide.Box, trans.Position);
                collide.TouchingCells.Clear();

            });

        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
        }
    }
}
