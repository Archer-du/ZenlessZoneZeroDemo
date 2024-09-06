using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterAttackState : CharacterBaseState
    {
        public CharacterAttackState(CharacterController controller, CharacterStateMachine stateMachine, ECharacterState type) : base(controller, stateMachine, type)
        {
        }
    }
}