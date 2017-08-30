using ECS;
using Logic.Base;

namespace Logic
{
    public class MovementComonent : BaseComponent
    {
        private Movement move;

        public MovementComonent(Movement move)
        {
            this.move = move;
        }

        public Movement Move { get { return move; } }
    }
}
