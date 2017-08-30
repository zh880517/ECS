namespace ECS
{
    public class Entity
    {
        private int id;
        private EntityManager entityMgr;

        public int ID { get { return id; } }

        public Entity(EntityManager entityManager, int id)
        {
            this.id = id;
            this.entityMgr = entityManager;
        }

        public void AddComponent<T>( T val ) where T : class, IComponent
        {
            if (val.Owner != null)
            {
                //TODO:抛异常
            }
            (val as BaseComponent).SetEntity(this);
            entityMgr.AddComponent<T>(this, val);
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            entityMgr.RemoveComponent<T>(this);
        }

        public T GetComponent<T>() where T :class ,IComponent
        {
            return entityMgr.GetComponent<T>(this);
        }

        public bool HasComponent<T>() where T:class,IComponent
        {
            return entityMgr.HasComponent<T>(this);
        }

        public override int GetHashCode()
        {
            return id;
        }

    }
}
