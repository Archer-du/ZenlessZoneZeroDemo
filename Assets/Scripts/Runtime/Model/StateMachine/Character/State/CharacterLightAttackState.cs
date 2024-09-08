using ZZZDemo.Runtime.Model.Character.Controller;
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
                View.Animation.RushAttack.Set();
            }
            View.Animation.LightAttack.Set();
            View.Animation.LightAttackDeriveLayer.Set(deriveData.deriveLayer);
        }


        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            // TODO: config
            // TODO: generic
            if (phase == EActionPhase.Derive && controller.IsLightAttacking && deriveData.deriveLayer < 4)
            {
                FSM.DeriveState(ECharacterState.LightAttack, 
                    new CharacterLightAttackDeriveData(deriveData.deriveLayer + 1));
                return true;
            }
            // if (phase == EActionPhase.DelayDerive && controller.IsLightAttacking && deriveData.deriveLayer == 3)
            // {
            //     if (FSM[ECharacterState.LightAttack] is CharacterActionState<CharacterLightAttackDeriveData> deriveState)
            //     {
            //         deriveState.InjectDeriveData(
            //             new CharacterLightAttackDeriveData(deriveData.deriveLayer + 1, false, true));
            //     }
            //     FSM.ChangeState(ECharacterState.LightAttack);
            //     return true;
            // }

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
            
            // if (phase >= EActionPhase.Derive && controller.IsEvading)
            // {
            //     FSM.ChangeState(ECharacterState.Evade);
            //     return true;
            // }
            return false;
        }
    }
}