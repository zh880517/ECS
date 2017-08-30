using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public interface IPool
    {
        int Count { get; }
        void AddComponent(int id, IComponent val);
        IComponent GetComponent(int id);
        void RemoveComponent(int id);
        void ReSize(int size);
    }
    
    public class Pool<T> : IPool where T : class, IComponent
    {
        private List<T> components = new List<T>(100);
        private HashSet<T> vaildComponents = new HashSet<T>();
        public int Count
        {
            get { return vaildComponents.Count; }
        }

        public HashSet<T> VaildComponents { get { return vaildComponents; } }
        public void AddComponent(int id, IComponent val)
        {
            if(val != null)
            {
                if (components[id] != default(T))
                    RemoveComponent(id);
                components[id] = val as T;
                vaildComponents.Add(val as T);
            }
        }

        public IComponent GetComponent(int id)
        {
            return components[id];
        }

        public void RemoveComponent(int id)
        {
            vaildComponents.Remove(components[id]);
            components[id] = default(T);
        }

        public void ReSize(int size)
        {
            while (components.Count < size)
                components.Add(default(T));
        }
        
    }
}
