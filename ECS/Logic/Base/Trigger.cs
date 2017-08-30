using ECS;
using System.Collections.Generic;

namespace Logic.Base
{
    public class Trigger
    {
        //第一次碰触的对象,每一帧开始清除
        public HashSet<Entity> EnterEntity = new HashSet<Entity>();
        //之前碰触，当前帧离开的对象，每一帧开始清除
        public HashSet<Entity> LeaveEntity = new HashSet<Entity>();
        //已经处理过第一次碰触等待离开的对象
        public HashSet<Entity> HitEntity = new HashSet<Entity>();
    }
}
