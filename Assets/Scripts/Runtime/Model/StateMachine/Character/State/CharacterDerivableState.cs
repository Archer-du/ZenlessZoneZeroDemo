using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    /// <summary>
    /// 可以作为派生目标的状态
    /// </summary>
    /// <typeparam name="TD">派生数据类</typeparam>
    internal class CharacterDerivableState<TD> : CharacterActionState where TD : CharacterDeriveData, new()
    {
        protected TD deriveData;
        internal CharacterDerivableState(CharacterController controller, CharacterStateMachine stateMachine, 
            ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type, actionType)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            deriveData ??= new TD();
        }

        internal override void Exit()
        {
            base.Exit();
            // clean up derive data
            deriveData = null;
        }

        internal override void InjectDeriveData(CharacterDeriveData data) => deriveData = data as TD;
    }
}