using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterWalkState : CharacterMoveState
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

            // TODO: config
            const float walkToRunSpeed = 0.75f;
            walkToRunFactor += walkToRunSpeed * deltaTime;
            View.Animation.WalkBlendParam.Set(walkToRunFactor);
        }
        
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

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