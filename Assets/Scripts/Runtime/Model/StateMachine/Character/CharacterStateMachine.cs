
using UnityEngine;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;

namespace ZZZDemo.Runtime.Model.StateMachine.Character
{
    internal class CharacterStateMachine : BaseStateMachine<CharacterBaseState, ECharacterState>
    {
        internal void DeriveState(ECharacterState newState, CharacterDeriveData data)
        {
                CurrentState.Exit();
                PreviousState = CurrentState;
                CurrentState = this[newState];
                if (CurrentState is CharacterDerivableState derivableState)
                {
                    derivableState.InjectDeriveData(data);
                }
                else
                {
                    Debug.Log($"failed to derive to non-derivable state: {CurrentState}");
                }
                CurrentState.Enter();
                if (debugMode)
                {
                    Debug.Log($"{PreviousState} ==> |Derive|{CurrentState}");
                }
        }
    }
}