using System;
using System.Collections.Generic;

namespace ECS
{

    public class EntityManager
    {
        private List<HashSet<Type>> entityComponentMask = new List<HashSet<Type>>();
        private Dictionary<Type, IPool> componentPool = new Dictionary<Type, IPool>();
        private Dictionary<Type, ISingleComponent> singleComponent = new Dictionary<Type, ISingleComponent>();
        private Queue<int> idleId = new Queue<int>();


        public Entity CreateEntity()
        {
            if (idleId.Count > 0)
            {
                int id = idleId.Dequeue();
                entityComponentMask[id] = new HashSet<Type>();
                return new Entity(this, id);
            }
            entityComponentMask.Add(new HashSet<Type>());
            return new Entity(this, entityComponentMask.Count - 1);
        }

        public void RemoveEntity(Entity entity)
        {
            entityComponentMask[entity.ID] = null;
            idleId.Enqueue(entity.ID);
            foreach (var pool in componentPool)
            {
                pool.Value.RemoveComponent(entity.ID);
            }
        }

        public void AddComponent<T>(Entity entity, T val) where T : class, IComponent
        {
            Type t = typeof(T);
            IPool pool = null;
            if (!componentPool.TryGetValue(t, out pool))
            {
                pool = new Pool<T>();
                componentPool.Add(t, pool);
            }
            entityComponentMask[entity.ID].Add(t);
            pool.AddComponent(entity.ID, val);
        }

        public bool HasComponent<T>(Entity entity)
        {
            return entityComponentMask[entity.ID].Contains(typeof(T));
        }

        public T GetComponent<T>(Entity entity) where T : class, IComponent
        {
            Type t = typeof(T);
            if (componentPool.TryGetValue(t, out IPool pool))
                return pool.GetComponent(entity.ID) as T;
            return null;
        }

        public void RemoveComponent<T>(Entity entity)
        {
            Type t = typeof(T);
            entityComponentMask[entity.ID].Remove(t);
            if (componentPool.TryGetValue(t, out IPool pool))
                pool.RemoveComponent(entity.ID);
        }

        #region Foreach


        public void Foreach<C>(Action<C> func) where C : class, IComponent
        {
            IPool pool = null;
            if (!componentPool.TryGetValue(typeof(C), out pool))
                return;
            foreach (var compt in (pool as Pool<C>).VaildComponents)
                func(compt);
        }

        private const int cacheNum = 10;
        private List<Type> typeCache = new List<Type>(cacheNum);
        private List<IPool> poolCache = new List<IPool>(cacheNum);
        private List<IComponent> comptCache = new List<IComponent>(cacheNum);

        private bool FillPoolCache()
        {
            foreach (var t in typeCache)
            {
                if (!componentPool.TryGetValue(t, out IPool pool))
                    return false;
                poolCache.Add(pool);
            }
            poolCache.Sort((IPool t1, IPool t2) => { return t1.Count - t2.Count; });
            return true;
        }

        public void ForEach<C1, C2>(Action<Entity, C1, C2> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                if (compt2 == null)
                    continue;
                func(entity, compt1, compt2);
            }
        }

        public void ForEach<C1, C2, C3>(Action<Entity, C1, C2, C3> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                if (compt2 == null || compt3 == null)
                    continue;
                func(entity, compt1, compt2, compt3);
            }
        }

        public void ForEach<C1, C2, C3, C4>(Action<Entity, C1, C2, C3, C4> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                if (compt2 == null || compt3 == null || compt4 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5>(Action<Entity, C1, C2, C3, C4, C5> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5, C6>(Action<Entity, C1, C2, C3, C4, C5, C6> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
            where C6 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            typeCache.Add(typeof(C6));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                var compt6 = poolCache[5].GetComponent(entity.ID) as C6;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null || compt6 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5, compt6);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5, C6, C7>(Action<Entity, C1, C2, C3, C4, C5, C6, C7> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
            where C6 : class, IComponent
            where C7 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            typeCache.Add(typeof(C6));
            typeCache.Add(typeof(C7));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                var compt6 = poolCache[5].GetComponent(entity.ID) as C6;
                var compt7 = poolCache[6].GetComponent(entity.ID) as C7;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null || compt6 == null || compt7 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5, compt6, compt7);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5, C6, C7, C8>(Action<Entity, C1, C2, C3, C4, C5, C6, C7, C8> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
            where C6 : class, IComponent
            where C7 : class, IComponent
            where C8 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            typeCache.Add(typeof(C6));
            typeCache.Add(typeof(C7));
            typeCache.Add(typeof(C8));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                var compt6 = poolCache[5].GetComponent(entity.ID) as C6;
                var compt7 = poolCache[6].GetComponent(entity.ID) as C7;
                var compt8 = poolCache[7].GetComponent(entity.ID) as C8;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null || compt6 == null || compt7 == null || compt8 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5, compt6, compt7, compt8);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5, C6, C7, C8, C9>(Action<Entity, C1, C2, C3, C4, C5, C6, C7, C8, C9> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
            where C6 : class, IComponent
            where C7 : class, IComponent
            where C8 : class, IComponent
            where C9 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            typeCache.Add(typeof(C6));
            typeCache.Add(typeof(C7));
            typeCache.Add(typeof(C8));
            typeCache.Add(typeof(C9));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                var compt6 = poolCache[5].GetComponent(entity.ID) as C6;
                var compt7 = poolCache[6].GetComponent(entity.ID) as C7;
                var compt8 = poolCache[7].GetComponent(entity.ID) as C8;
                var compt9 = poolCache[8].GetComponent(entity.ID) as C9;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null 
                    || compt6 == null || compt7 == null || compt8 == null || compt9 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5, compt6, compt7, compt8, compt9);
            }
        }

        public void ForEach<C1, C2, C3, C4, C5, C6, C7, C8, C9, C10>(Action<Entity, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10> func)
            where C1 : class, IComponent
            where C2 : class, IComponent
            where C3 : class, IComponent
            where C4 : class, IComponent
            where C5 : class, IComponent
            where C6 : class, IComponent
            where C7 : class, IComponent
            where C8 : class, IComponent
            where C9 : class, IComponent
            where C10 : class, IComponent
        {
            typeCache.Clear();
            typeCache.Add(typeof(C1));
            typeCache.Add(typeof(C2));
            typeCache.Add(typeof(C3));
            typeCache.Add(typeof(C4));
            typeCache.Add(typeof(C5));
            typeCache.Add(typeof(C6));
            typeCache.Add(typeof(C7));
            typeCache.Add(typeof(C8));
            typeCache.Add(typeof(C9));
            typeCache.Add(typeof(C10));
            if (!FillPoolCache())
                return;
            foreach (var compt1 in (poolCache[0] as Pool<C1>).VaildComponents)
            {
                var entity = compt1.Owner;
                var compt2 = poolCache[1].GetComponent(entity.ID) as C2;
                var compt3 = poolCache[2].GetComponent(entity.ID) as C3;
                var compt4 = poolCache[3].GetComponent(entity.ID) as C4;
                var compt5 = poolCache[4].GetComponent(entity.ID) as C5;
                var compt6 = poolCache[5].GetComponent(entity.ID) as C6;
                var compt7 = poolCache[6].GetComponent(entity.ID) as C7;
                var compt8 = poolCache[7].GetComponent(entity.ID) as C8;
                var compt9 = poolCache[8].GetComponent(entity.ID) as C9;
                var compt10 = poolCache[9].GetComponent(entity.ID) as C10;
                if (compt2 == null || compt3 == null || compt4 == null || compt5 == null
                    || compt6 == null || compt7 == null || compt8 == null || compt9 == null || compt10 == null)
                    continue;
                func(entity, compt1, compt2, compt3, compt4, compt5, compt6, compt7, compt8, compt9, compt10);
            }
        }

        #endregion

        #region SingleComponent
        public bool AddComponent<T>(T val) where T : ISingleComponent
        {
            Type t = typeof(T);
            if (singleComponent.ContainsKey(t))
            {
                return false;
            }
            singleComponent.Add(t, val);
            return true;
        }

        public T GetComponent<T>() where T : class, ISingleComponent
        {
            ISingleComponent compt = null;
            singleComponent.TryGetValue(typeof(T), out compt);
            return compt as T;
        }

        public void RemoveComponent<T>() where T : ISingleComponent
        {
            singleComponent.Remove(typeof(T));
        }
        #endregion
    }
}
