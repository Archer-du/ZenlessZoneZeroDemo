using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.Config;
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
                    evadeColdDownHandle = controller.timerManager.SetTimer(GlobalConstants.continuousEvadeCooldownTime, 
                        () => { controller.canEvade = true; });
                    break;
            }
            
            Input.EvadeButton.Consume();

            if (controller.IsMoving)
            {
                //TODO: config
                View.Animation.TransitToState(EAnimationState.EvadeFront, 0.1f);
            }
            else
            {
                //TODO: config
                View.Animation.TransitToState(EAnimationState.EvadeBack, 0.1f);
            }
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            if (phase == EActionPhase.Cancel && controller.IsEvading)
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
            
            if (phase == EActionPhase.Cancel && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            return false;
        }
    }
}