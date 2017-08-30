using ECS;
using Logic.Base;
using System.Collections.Generic;

namespace Logic
{
    public class TriggerComponent : BaseComponent
    {
        private Trigger trigger = new Trigger();
        public Trigger TriggerEntity { get { return trigger; } }
    }
}
