using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.StateMachine.Character
{
    internal abstract class CharacterMoveState : CharacterBaseState
    {
        protected CharacterMoveState(PlayerController controller, CharacterStateMachine stateMachine, ECharacterState type) : base(controller, stateMachine, type)
        {
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            if (UseSmoothRotate())
            {
                SmoothRotate(deltaTime);
            }
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            if (controller.input.MoveJoyStick.Value == Vector2.zero)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }

            return false;
        }

        protected virtual bool UseSmoothRotate() => controller.input.MoveJoyStick.Value != Vector2.zero;

        private void SmoothRotate(float deltaTime)
        {
            var targetDir = MovementUtils.GetHorizontalProjectionVector(Input.LookAt.GetLookAtDirection());
            targetDir = MovementUtils.GetRotationByAxis(
                MovementUtils.GetRelativeInputAngle(Input.MoveJoyStick.Value), Vector3.up) * targetDir;
            float angle = MovementUtils.GetRelativeRotateAngle(View.Movement.GetTransformForward(), targetDir);
            // TODO: config
            const float angleTolerance = 2.5f;
            if (Mathf.Abs(angle) > angleTolerance)
            {
                // TODO: config
                const float rotateResponseTime = 0.04f;
                View.Movement.RotateCharacterHorizontal(angle * (deltaTime / rotateResponseTime));
            }
        }
    }
}