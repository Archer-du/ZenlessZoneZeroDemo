using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    public enum EEvadePhase
    {
        NONE = 0,
        Start,
        Active,
        Recovery,
    }
    internal class CharacterEvadeState : CharacterBaseState
    {
        private EEvadePhase phase = EEvadePhase.NONE;
        
        public CharacterEvadeState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Evade)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            // assert evadeTimesRemain > 0
            controller.evadeTimesRemain--;
            phase = EEvadePhase.NONE;
            View.Animation.Evade.Set();
        }

        internal override void Exit()
        {
            base.Exit();
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            phase = View.Animation.GetEvadePhase();
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;

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
            
            if (phase == EEvadePhase.Recovery && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            return false;
        }
    }
}