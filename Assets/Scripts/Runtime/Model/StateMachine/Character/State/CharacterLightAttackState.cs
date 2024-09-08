using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterLightAttackState : CharacterDerivableState<CharacterLightAttackDeriveData>
    {
        public CharacterLightAttackState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.LightAttack, EActionType.Attack)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            Input.LightAttackButton.Consume();
            
            if (deriveData.rushAttack)
            {
                // TODO: config
                View.Animation.TransitToState(EAnimationState.RushAttack, 0.05f);
            }
            if (deriveData.delayDerive)
            {
                // TODO: generic
                View.Animation.TransitToState(EAnimationState.Anby_DelayAttack, 0.05f);
            }
            View.Animation.LightAttackDeriveLayer.Set(deriveData.deriveLayer);
        }

        internal override void Exit()
        {
            base.Exit();
            View.Animation.LightAttackDeriveLayer.Set(-1);
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            // TODO: config
            // TODO: generic
            if (phase == EActionPhase.Cancel && controller.IsLightAttacking && deriveData.deriveLayer < 4)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new CharacterLightAttackDeriveData(deriveData.deriveLayer + 1));
                return true;
            }
            if (phase == EActionPhase.Delay && controller.IsLightAttacking && deriveData.deriveLayer == 3)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new CharacterLightAttackDeriveData(deriveData.deriveLayer + 1, false, true));
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
            
            if (phase >= EActionPhase.Cancel && controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            return false;
        }
    }
}