using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    public enum EEvadePhase
    {
        Start,
        Active,
        Recovery,
        Terminate
    }
    internal class CharacterEvadeState : CharacterBaseState
    {
        private EEvadePhase phase = EEvadePhase.Terminate;

        private bool reentrant = false;

        private TimerHandle evadeColdDownHandle;
        public CharacterEvadeState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Evade)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            // assert evadeTimesRemain > 0
            Input.EvadeButton.Consume();

            if (reentrant)
            {
                reentrant = false;
                controller.canEvade = false;
                evadeColdDownHandle?.Invalidate();
                //TODO: config
                evadeColdDownHandle = controller.timerManager.SetTimer(0.8f, () => { controller.canEvade = true; });
            }
            phase = EEvadePhase.Terminate;

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
            phase = View.Animation.GetEvadePhase();
            if (phase == EEvadePhase.Active && controller.IsEvading)
            {
                reentrant = true;
            }
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

            if (phase == EEvadePhase.Terminate)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            if (phase == EEvadePhase.Active && controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (phase == EEvadePhase.Active && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            if (phase == EEvadePhase.Recovery && controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (phase == EEvadePhase.Recovery && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            return false;
        }
    }
}