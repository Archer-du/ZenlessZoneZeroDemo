using System;

namespace ZZZDemo.Runtime.Model.StateMachine
{
    public abstract class BaseState<TE> where TE : Enum
    {
        internal readonly TE type;

        protected BaseState(TE type)
        {
            this.type = type;
        }
        internal virtual void Enter() {}

        internal virtual void Exit() {}

        internal virtual void Update(float deltaTime) {}

        internal virtual bool CheckTransition() => false;

        public override string ToString()
        {
            return type.ToString();
        }
    }
}