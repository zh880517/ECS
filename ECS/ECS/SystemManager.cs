using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class SystemManager
    {
        private List<ISystem> systems = new List<ISystem>();
        private Dictionary<Type, int> sysTypes = new Dictionary<Type, int>();
        private EntityManager entityMgr;

        public SystemManager(EntityManager entityMgr)
        {
            this.entityMgr = entityMgr;
        }

        public bool AddSystem<T>() where T : ISystem, new()
        {
            Type t = typeof(T);
            if (sysTypes.ContainsKey(t))
            {
                return false;
            }
            sysTypes.Add(t, systems.Count);
            systems.Add(new T());
            return true;
        }

        public void SetActive<T>(bool bActive) where T:ISystem
        {
            int index = 0;
            if (sysTypes.TryGetValue(typeof(T), out index))
            {
                systems[index].IsActive = bActive;
            }
        }

        public void Update(TimeDelta timeDelta)
        {
            foreach (var sys in systems)
            {
                if (sys.IsActive)
                {
                    sys.Update(entityMgr, timeDelta);
                }
            }
        }
    }
}
