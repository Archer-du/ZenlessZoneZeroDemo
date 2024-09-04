﻿using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterWalkState : CharacterBaseState
    {
        private float walkToRunFactor = 0;
        internal CharacterWalkState(PlayerController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine,ECharacterState.Walk)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            walkToRunFactor = 0;
            View.Animation.WalkingParam.Set(true);
            View.Animation.WalkBlendParam.Set(0);
        }

        internal override void Exit()
        {
            base.Exit();
            View.Animation.WalkingParam.Set(false);
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            // smooth rotation
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

            // TODO: config
            const float walkToRunSpeed = 0.75f;
            walkToRunFactor += walkToRunSpeed * deltaTime;
            View.Animation.WalkBlendParam.Set(walkToRunFactor);
        }
        
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            if (controller.input.MoveJoyStick.Value == Vector2.zero)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            // TODO: config
            const float walkToRunThreshold = 0.75f;
            if (walkToRunFactor > walkToRunThreshold)
            {
                FSM.ChangeState(ECharacterState.Run);
            }
            return false;
        }
    }
}