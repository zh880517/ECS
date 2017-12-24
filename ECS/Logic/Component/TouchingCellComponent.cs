using System.Collections.Generic;
using ECS;
using Logic.Base;

namespace Logic
{
    
    public class TouchingCellComponent : BaseComponent
    {
        //碰触到的场景格子
        public List<SceneCell> TouchingCells = new List<SceneCell>(3);
    }
}
