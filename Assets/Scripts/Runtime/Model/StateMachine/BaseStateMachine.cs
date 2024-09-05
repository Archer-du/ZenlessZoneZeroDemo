using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZZZDemo.Runtime.Model.StateMachine
{
    public abstract class BaseStateMachine<T, TE> where T : BaseState<TE> where TE : Enum
    {
        public T PreviousState { get; private set; }
        public T CurrentState { get; private set; }

        protected readonly Dictionary<TE, T> stateMap;
        
        public bool debugMode = true;

        public T this[TE eState]
        {
            get => stateMap[eState];
            set => stateMap.TryAdd(eState, value);
        }

        internal BaseStateMachine()
        {
            stateMap = new Dictionary<TE, T>();
        }

        internal void Initialize(TE startingState)
        {
            PreviousState = this[startingState];
            CurrentState = this[startingState];
            CurrentState.Enter();
        }

        // 现在的缺点：Enter下一帧才开始Update
        internal void ChangeState(TE newState)
        {
            CurrentState.Exit();
            PreviousState = CurrentState;
            CurrentState = this[newState];
            CurrentState.Enter();
            if (debugMode)
            {
                Debug.Log($"{PreviousState} ==> {CurrentState}");
            }
        }

        internal void Update(float deltaTime)
        {
            CurrentState?.Update(deltaTime);
        }
    }
}