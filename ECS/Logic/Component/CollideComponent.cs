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
        
        public SceneCell CenterCell;//中心点所在格子
        public List<SceneCell> TouchingCells = new List<SceneCell>(3);
    }
}
