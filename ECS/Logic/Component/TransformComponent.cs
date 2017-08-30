using ECS;
using Logic.Base;

namespace Logic
{
    
    public class TransformComponent : BaseComponent
    {
        public Vector Position { get; set; }//位置
        public Vector Rotation { get; set; }

        public Vector Scale = Vector.One;
    }
}
