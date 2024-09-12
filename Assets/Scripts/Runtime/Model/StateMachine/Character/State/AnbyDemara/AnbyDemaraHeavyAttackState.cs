using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State.AnbyDemara
{
    internal class AnbyDemaraHeavyAttackState : CharacterDerivableState<AnbyDemaraHeavyAttackDeriveData>
    {
        internal AnbyDemaraHeavyAttackState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.HeavyAttack, EActionType.Attack)
        {
        }
        internal override void Enter()
        {
            base.Enter();
            Input.HeavyAttackButton.Consume();
            
            if (deriveData.perfectDerive)
            {
                // TODO: config
                // TODO: generic
                View.Animation.TransitToState(EAnimationState.Anby_DeriveHeavyAttack, 0.05f);
            }
            else
            {
                // TODO: config
                View.Animation.TransitToState(EAnimationState.HeavyAttack, 0.05f);
            }
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            // TODO: config
            if (phase == EActionPhase.Cancel 
                && deriveData.perfectDerive && !deriveData.derivedFromDelayAttack && controller.IsLightAttacking)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new AnbyDemaraLightAttackDeriveData()
                    {
                        layer = 4,
                        derivedFromHeavyAttack = true,
                        delayDerive = true,
                    });
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