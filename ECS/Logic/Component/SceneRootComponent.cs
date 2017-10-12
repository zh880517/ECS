using ECS;
using Logic.Base;

namespace Logic
{
    public class SceneRootComponent : ISingleComponent
    {
        private SceneRoot root;
        public SceneRootComponent(SceneRoot root)
        {
            this.root = root;
        }

        public SceneRoot Root { get { return root; } }
    }
}
