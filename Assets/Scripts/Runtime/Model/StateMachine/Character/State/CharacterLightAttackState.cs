using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterLightAttackState : CharacterActionState
    {
        private int deriveLayer = 1;
        public CharacterLightAttackState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.LightAttack, EActionType.Attack)
        {
        }

        internal override void Enter()
        {
            base.Enter();
            Input.LightAttackButton.Consume();

            View.Animation.LightAttack.Set();
            View.Animation.LightAttackDeriveLayer.Set(deriveLayer);
        }

        internal override void Exit()
        {
            base.Exit();
            deriveLayer = 1;
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
        }

        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            // TODO: config
            if (phase == EActionPhase.Derive && controller.IsLightAttacking && deriveLayer < 4)
            {
                if (FSM[ECharacterState.LightAttack] is CharacterActionState deriveState)
                {
                    deriveState.InjectDeriveData(new CharacterLightAttackDeriveData(deriveLayer + 1));
                }
                FSM.ChangeState(ECharacterState.LightAttack);
                return true;
            }
            return false;
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
            if (phase == EActionPhase.Recovery && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            return false;
        }

        protected override void UnpackDeriveData()
        {
            base.UnpackDeriveData();
            if (deriveDataPack is CharacterLightAttackDeriveData deriveData)
            {
                deriveLayer = deriveData.deriveLayer;
                if (deriveData.rushAttack)
                {
                    View.Animation.RushAttack.Set();
                }
            }
            // destroy after unpack
            deriveDataPack = null;
        }
    }
}