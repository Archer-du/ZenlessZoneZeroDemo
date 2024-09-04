using View;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.Input;

namespace ZZZDemo.Runtime.Model.StateMachine.Character
{
    internal abstract class CharacterBaseState : BaseState<ECharacterState>
    {
        internal readonly PlayerController controller;
        internal IInputHandler Input => controller.input;
        internal IViewHandler View => controller.view;
        
        internal readonly CharacterStateMachine FSM;
        protected CharacterBaseState(PlayerController controller, CharacterStateMachine stateMachine, ECharacterState type) : base(type)
        {
            this.controller = controller;
            FSM = stateMachine;
        }
    }
}