using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterEvadeState : CharacterDerivableState<CharacterEvadeDeriveData>
    {
        private TimerHandle evadeColdDownHandle;
        public CharacterEvadeState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.Evade, EActionType.Evade)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            switch (deriveData.type)
            {
                case CharacterEvadeDeriveData.DeriveType.Reentrant:
                    controller.canEvade = false;
                    evadeColdDownHandle?.Invalidate();
                    //TODO: config
                    evadeColdDownHandle = controller.timerManager.SetTimer(0.8f, () => { controller.canEvade = true; });
                    break;
            }
            
            Input.EvadeButton.Consume();

            if (controller.IsMoving)
            {
                View.Animation.EvadeFront.Set();
            }
            else
            {
                View.Animation.EvadeBack.Set();
            }
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            if (phase == EActionPhase.Derive && controller.IsEvading)
            {
                FSM.DeriveState(ECharacterState.Evade, 
                    new CharacterEvadeDeriveData(CharacterEvadeDeriveData.DeriveType.Reentrant));
                return true;
            }
            if (phase == EActionPhase.Active && controller.IsLightAttacking)
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
            
            if (phase == EActionPhase.Recovery && !controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            if (phase == EActionPhase.Recovery && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            
            if (phase == EActionPhase.Derive && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            return false;
        }
    }
}