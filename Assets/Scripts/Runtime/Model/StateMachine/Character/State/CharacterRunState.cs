using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
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
                View.Animation.TurnBack.Set();
            }
        }
        internal override void Exit()
        {
            base.Exit();
            View.Animation.Running.Set(false);
            controller.canTurnBack = true;
            turnBackTimerHandle?.Invalidate();
            //TODO: config
            turnBackTimerHandle = controller.timerManager.SetTimer(0.2f, () => { controller.canTurnBack = false; });
        }
        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);

            if (controller.IsMoving)
                controller.lastRunDirection = Input.MoveJoyStick.Direction;
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            if (!View.Animation.CheckTurnBack() && controller.IsLightAttacking)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new CharacterLightAttackDeriveData(1, true));
                return true;
            }
            return false;
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
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
            return false;
        }

        protected override bool UseSmoothRotate() => !View.Animation.CheckAnimatedRootRotation();
    }
}