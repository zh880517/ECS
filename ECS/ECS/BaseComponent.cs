namespace ECS
{
    public interface ISingleComponent
    {
    }

    public interface IComponent
    {
        Entity Owner { get;}
    }

    public class BaseComponent : IComponent
    {
        private Entity _Owner;
        public Entity Owner { get { return _Owner; } }

        public void SetEntity(Entity entity)
        {
            _Owner = entity;
        }
    }
}
