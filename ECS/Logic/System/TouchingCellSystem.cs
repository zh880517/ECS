﻿using ECS;

namespace Logic
{
    class TouchingCellSystem : BaseSystem
    {
        public override void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta)
        {

        }

        public override void Update(EntityManager entityMgr, TimeDelta timeDelta)
        {
            SceneRootComponent rootComponent = entityMgr.GetComponent<SceneRootComponent>();
            entityMgr.ForEach((Entity entity, TransformComponent trans, MovementComonent move, CollideComponent collide) =>
            {

            });
        }
    }
}
