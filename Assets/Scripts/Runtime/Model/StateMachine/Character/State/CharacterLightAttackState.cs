using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterLightAttackState : CharacterAttackState
    {
        private EActionPhase phase = EActionPhase.Terminate;

        public CharacterLightAttackState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.LightAttack)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            Input.LightAttackButton.Consume();
            phase = EActionPhase.Terminate;

            View.Animation.LightAttack.Set();
        }

        internal override void Exit()
        {
            base.Exit();
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            phase = View.Animation.GetActionPhase(EActionType.Attack);
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
            // if (phase == EActionPhase.Derive && controller.IsLightAttacking)
            // {
            //     FSM.ChangeState(ECharacterState.Evade);
            //     return true;
            // }
            if (phase == EActionPhase.Recovery && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            
            return false;
        }
    }
}