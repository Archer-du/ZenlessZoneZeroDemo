using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterIdleState : CharacterBaseState
    {
        internal CharacterIdleState(PlayerController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Idle)
        {
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
            if (controller.input.MoveJoyStick.Value != Vector2.zero 
                && controller.turnbackWindowTimer > 0 
                && controller.input.MoveJoyStick.Direction == - controller.lastRunDirection)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            if (controller.input.MoveJoyStick.Value != Vector2.zero)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            return false;
        }
    }
}