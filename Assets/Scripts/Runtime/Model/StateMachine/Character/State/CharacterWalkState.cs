using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.Config;
using ZZZDemo.Runtime.Model.Utils;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterWalkState : CharacterMoveState
    {
        private float walkToRunFactor = 0;
        internal CharacterWalkState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine,ECharacterState.Walk)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            walkToRunFactor = 0;
            View.Animation.Walking.Set(true);
            View.Animation.WalkBlend.Set(0);
        }

        internal override void Exit()
        {
            base.Exit();
            View.Animation.Walking.Set(false);
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            walkToRunFactor += GlobalConstants.walkToRunAccelerateFactor * deltaTime;
            View.Animation.WalkBlend.Set(walkToRunFactor);
        }
        
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
            if (!controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            if (controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (walkToRunFactor > GlobalConstants.walkToRunSpeedThreshold)
            {
                FSM.ChangeState(ECharacterState.Run);
            }
            
            if (controller.IsLightAttacking)
            {
                FSM.ChangeState(ECharacterState.LightAttack);
                return true;
            }
            if (controller.IsHeavyAttacking)
            {
                FSM.ChangeState(ECharacterState.HeavyAttack);
                return true;
            }
            return false;
        }
    }
}