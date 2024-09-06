using UnityEngine;
using ZZZDemo.Runtime.Model.Utils;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal abstract class CharacterMoveState : CharacterBaseState
    {
        protected CharacterMoveState(CharacterController controller, CharacterStateMachine stateMachine, ECharacterState type) : base(controller, stateMachine, type)
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

            return false;
        }

        protected virtual bool UseSmoothRotate() => true;

        private void SmoothRotate(float deltaTime)
        {
            if (!controller.IsMoving) return;
            
            var targetDir = MovementUtils.GetHorizontalProjectionVector(Input.LookAt.GetLookAtDirection());
            targetDir = MovementUtils.GetRotationByAxis(
                MovementUtils.GetRelativeInputAngle(Input.MoveJoyStick.Value), Vector3.up) * targetDir;
            float angle = MovementUtils.GetRelativeRotateAngle(View.Movement.GetCharacterForward(), targetDir);
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