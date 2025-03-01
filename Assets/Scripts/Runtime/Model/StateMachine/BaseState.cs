﻿using System;

namespace ZZZDemo.Runtime.Model.StateMachine
{
    public abstract class BaseState<TE> where TE : Enum
    {
        internal readonly TE type;
        protected BaseState(TE type)
        {
            this.type = type;
        }
        internal void Update(float deltaTime)
        {
            TickLogic(deltaTime);
            CheckTransition();
        }
        internal virtual void Enter() {}

        internal virtual void Exit() {}


        protected virtual bool CheckTransition() => false;
        protected virtual void TickLogic(float deltaTime) {}

        public override string ToString()
        {
            return type.ToString();
        }
    }
}