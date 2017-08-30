using ECS;
using Logic.Base;

namespace Logic
{
    public class CollideSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {
            entityMgr.ForEach((Entity Entity, TransformComponent trans, CollideComponent collide) =>
            {
                collide.HitEntity.Clear();
                AABB curAABB = AABB.OffSet(collide.Box, trans.Position);
            });

        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
        }
    }
}
