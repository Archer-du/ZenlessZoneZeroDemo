using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.Config;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;
using ZZZDemo.Runtime.Model.Utils;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;
using TimerHandle = ZZZDemo.Runtime.Model.Character.Controller.TimerHandle;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterRunState : CharacterMoveState
    {
        private TimerHandle turnBackTimerHandle;
        internal CharacterRunState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine,ECharacterState.Run)
        {
        }
        internal override void Enter()
        {
            base.Enter();
            View.Animation.Running.Set(true);
            if (controller.IsSharpTurn)
            {
                // TODO: config
                View.Animation.TransitToState(EAnimationState.TurnBack, 0.2f);
            }
        }
        internal override void Exit()
        {
            base.Exit();
            View.Animation.Running.Set(false);
            controller.canTurnBack = true;
            turnBackTimerHandle?.Invalidate();
            turnBackTimerHandle = controller.timerManager.SetTimer(GlobalConstants.canTurnBackTimeWindow, 
                () => { controller.canTurnBack = false; });
        }
        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);

            if (controller.IsMoving)
                controller.lastRunDirection = Input.MoveJoyStick.Direction;
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
            if (!View.Animation.CheckTurnBack() && controller.IsLightAttacking)
            {
                controller.rushAttack = true;
                FSM.ChangeState(ECharacterState.LightAttack);
                return true;
            }
            if (!View.Animation.CheckTurnBack() && !controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            if (!View.Animation.CheckTurnBack() && controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (!View.Animation.CheckTurnBack() && controller.IsHeavyAttacking)
            {
                FSM.ChangeState(ECharacterState.HeavyAttack);
                return true;
            }
            return false;
        }
    }
}