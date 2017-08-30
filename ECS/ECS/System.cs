using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public interface ISystem
    {
        bool IsActive { get; set; }

        void Update(EntityManager entityMgr, TimeDelta timeDelta);

        void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta);
    }

    public abstract class BaseSystem : ISystem
    {
        private bool isActive;
        public bool IsActive {
            get { return isActive; }
            set { isActive = value; }
        }

        public abstract void FixedUpdate(EntityManager entityMgr, TimeDelta timeDelta);
        public abstract void Update(EntityManager entityMgr, TimeDelta timeDelta);
    }
}
