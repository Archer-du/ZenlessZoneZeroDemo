using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State.AnbyDemara
{
    internal class AnbyDemaraLightAttackState : CharacterDerivableState<AnbyDemaraLightAttackDeriveData>
    {
        public AnbyDemaraLightAttackState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.LightAttack, EActionType.Attack)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            Input.LightAttackButton.Consume();
            
            if (controller.rushAttack)
            {
                controller.rushAttack = false;
                // TODO: config
                View.Animation.TransitToState(EAnimationState.RushAttack, 0.05f);
            }
            else if (deriveData.delayDerive)
            {
                // TODO: generic
                View.Animation.TransitToState(EAnimationState.Anby_DelayAttack, 0.05f);
            }
            else
            {
                //TODO: config
                if (deriveData.layer == 1)
                {
                    View.Animation.TransitToState(EAnimationState.LightAttack, 0.05f);
                }
            }
            View.Animation.LightAttackDeriveLayer.Set(deriveData.layer);
        }

        internal override void Exit()
        {
            base.Exit();
            View.Animation.LightAttackDeriveLayer.Set(-1);
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            if (phase == EActionPhase.Cancel 
                && deriveData.layer == 3 && controller.IsHeavyAttacking)
            {
                FSM.DeriveState(ECharacterState.HeavyAttack, 
                    new AnbyDemaraHeavyAttackDeriveData()
                    {
                        perfectDerive = true,
                    });
                return true;
            }
            // TODO: generic
            if (phase == EActionPhase.Cancel 
                && deriveData.layer < 4 && controller.IsLightAttacking)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new AnbyDemaraLightAttackDeriveData()
                    {
                        layer = deriveData.layer + 1,
                    });
                return true;
            }
            if (phase == EActionPhase.Delay 
                && deriveData.layer == 3 && controller.IsLightAttacking)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new AnbyDemaraLightAttackDeriveData()
                    {
                        layer = deriveData.layer + 1,
                        delayDerive = true,
                    });
                return true;
            }
            if (phase == EActionPhase.Cancel 
                && deriveData.layer == 4 && deriveData.delayDerive && !deriveData.derivedFromHeavyAttack 
                && controller.IsHeavyAttacking)
            {
                FSM.DeriveState(ECharacterState.HeavyAttack, 
                    new AnbyDemaraHeavyAttackDeriveData()
                    {
                        perfectDerive = true,
                        derivedFromDelayAttack = true,
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