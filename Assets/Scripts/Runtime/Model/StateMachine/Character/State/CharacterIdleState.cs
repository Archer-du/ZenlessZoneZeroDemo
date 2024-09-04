using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterIdleState : CharacterBaseState
    {
        internal CharacterIdleState(PlayerController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Idle)
        {
        }

        internal override void Enter()
        {
            base.Enter();
        }
        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
        }

        internal override void Exit()
        {
            base.Exit();
        }
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            if (controller.input.MoveJoyStick.Value != Vector2.zero)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            return false;
        }
    }
}