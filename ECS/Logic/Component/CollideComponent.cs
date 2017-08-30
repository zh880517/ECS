using ECS;
using Logic.Base;
using System.Collections.Generic;

namespace Logic
{
    
    //碰撞组件
    public class CollideComponent : BaseComponent
    {
        private AABB box;

        public CollideComponent(AABB box)
        {
            this.box = box;
        }

        public AABB Box { get { return box; } }

        public HashSet<Entity> HitEntity = new HashSet<Entity>();
    }
}
