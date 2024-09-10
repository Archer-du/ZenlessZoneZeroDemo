using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    /// <summary>
    /// 可以作为派生目标的状态
    /// </summary>
    internal abstract class CharacterDerivableState : CharacterActionState
    {
        protected CharacterDeriveData deriveData;
        internal CharacterDerivableState(CharacterController controller, CharacterStateMachine stateMachine, 
            ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type, actionType)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            deriveData ??= GetDefaultData();
        }

        internal override void Exit()
        {
            base.Exit();
            // clean up derive data
            deriveData = null;
        }
        
        internal void InjectDeriveData(CharacterDeriveData data) => deriveData = data;

        protected abstract CharacterDeriveData GetDefaultData();
    }
}