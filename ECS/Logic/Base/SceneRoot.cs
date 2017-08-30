using System.Collections.Generic;
namespace Logic.Base
{
    public class SceneRoot
    {
        private Dictionary<int, SceneCell> cells = new Dictionary<int, SceneCell>();
        public SceneCell GetSceneCell(int x, int y)
        {
            int key = x | y << 16;
            SceneCell cell = null;
            cells.TryGetValue(key, out cell);
            return cell;
        }
    }
}
