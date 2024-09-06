using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterEvadeState : CharacterBaseState
    {
        private EActionPhase phase = EActionPhase.Terminate;

        private bool reentrant = false;

        private TimerHandle evadeColdDownHandle;
        public CharacterEvadeState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Evade)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            Input.EvadeButton.Consume();
            phase = EActionPhase.Terminate;

            if (reentrant)
            {
                reentrant = false;
                controller.canEvade = false;
                evadeColdDownHandle?.Invalidate();
                //TODO: config
                evadeColdDownHandle = controller.timerManager.SetTimer(0.8f, () => { controller.canEvade = true; });
            }

            if (controller.IsMoving)
            {
                View.Animation.EvadeFront.Set();
            }
            else
            {
                View.Animation.EvadeBack.Set();
            }
        }

        internal override void Exit()
        {
            base.Exit();
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            phase = View.Animation.GetActionPhase(EActionType.Evade);
            
            if (phase == EActionPhase.Active && controller.IsEvading)
            {
                reentrant = true;
            }
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

            // recovery阶段就已经是逻辑上的Idle状态了，所以这里要切换
            if (phase == EActionPhase.Recovery)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            // derive
            if (phase == EActionPhase.Active && controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (phase == EActionPhase.Active && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }

            return false;
        }
    }
}