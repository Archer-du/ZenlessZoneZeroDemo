using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    /// <summary>
    /// 可以作为派生目标的状态
    /// </summary>
    internal abstract class CharacterDerivableState : CharacterActionState
    {
        internal CharacterDerivableState(CharacterController controller, CharacterStateMachine stateMachine, 
            ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type, actionType)
        {
        }
        internal abstract void InjectDeriveData(CharacterDeriveData data);
    }

    internal abstract class CharacterDerivableState<T> : CharacterDerivableState 
        where T : CharacterDeriveData, new()
    {
        protected T deriveData;
        protected CharacterDerivableState(CharacterController controller, CharacterStateMachine stateMachine, 
            ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type, actionType)
        {
        }
        
        internal override void Enter()
        {
            base.Enter();
            deriveData ??= new T();
        }

        internal override void Exit()
        {
            base.Exit();
            // clean up derive data
            deriveData = null;
        }
        internal override void InjectDeriveData(CharacterDeriveData data) => deriveData = data as T;
    }
}