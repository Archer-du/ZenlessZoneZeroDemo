using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterRunState : CharacterMoveState
    {
        internal CharacterRunState(PlayerController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine,ECharacterState.Run)
        {
        }
        internal override void Enter()
        {
            base.Enter();
            View.Animation.RunningParam.Set(true);
            if (controller.turnbackWindowTimer > 0 
                && controller.input.MoveJoyStick.Direction == - controller.lastRunDirection)
            {
                View.Animation.TurnBackParam.Set();
            }
        }
        internal override void Exit()
        {
            base.Exit();
            View.Animation.RunningParam.Set(false);
            // TODO: 
            controller.turnbackWindowTimer = 0.2f;
        }
        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);

            if (controller.input.MoveJoyStick.Value != Vector2.zero)
                controller.lastRunDirection = Input.MoveJoyStick.Direction;
        }
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

            return false;
        }

        protected override bool UseSmoothRotate()
        {
            return base.UseSmoothRotate() && !View.Animation.CheckAnimatedRootRotation();
        }
    }
}