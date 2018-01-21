using ECS;
using Logic.Base;

namespace Logic
{
    public class RigidBodyComponent : BaseComponent
    {
        private AABB box;
        public RigidBodyComponent(AABB box)
        {
            this.box = box;
        }

        public AABB Box { get { return box; } }
    }
}
