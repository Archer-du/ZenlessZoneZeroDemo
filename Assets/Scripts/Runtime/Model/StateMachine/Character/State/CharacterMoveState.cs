﻿using UnityEngine;
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
            if (controller.IsMoving && UseSmoothRotate())
            {
                // TODO: config
                const float rotateResponseTime = 0.04f;
                controller.SmoothRotateTowardsTargetDirection(deltaTime, rotateResponseTime);
            }
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

            return false;
        }

        protected bool UseSmoothRotate() => !View.Animation.CheckAnimatedRootRotation();
    }
}