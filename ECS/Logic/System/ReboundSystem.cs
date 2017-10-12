using ECS;
namespace Logic
{
    public class ReboundSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {
            
        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
            SceneRootComponent rootComponent = entityMgr.GetComponent<SceneRootComponent>();
            entityMgr.ForEach((Entity entity, TransformComponent trans, MovementComonent move ,CollideComponent collide, ReboundComponent rebound) =>
            {
                if (move.Move.Type == Base.MoveType.Straighton)
                {
                }
            });
        }
    }
}
