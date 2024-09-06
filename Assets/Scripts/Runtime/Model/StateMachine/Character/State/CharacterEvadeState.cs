using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterEvadeState : CharacterActionState
    {
        private TimerHandle evadeColdDownHandle;
        public CharacterEvadeState(CharacterController controller, CharacterStateMachine stateMachine) 
            : base(controller, stateMachine, ECharacterState.Evade, EActionType.Evade)
        {
        }

        internal override void Enter()
        {
            base.Enter();
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

        internal override void Exit()
        {
            base.Exit();
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
        }
        protected override bool CheckDeriveTransition()
        {
            if (base.CheckDeriveTransition()) return true;
            if (phase == EActionPhase.Derive && controller.IsEvading)
            {
                if (FSM[ECharacterState.Evade] is CharacterActionState deriveState)
                {
                    var deriveData = new CharacterEvadeDeriveData(CharacterEvadeDeriveData.DeriveType.Reentrant);
                    deriveState.InjectDeriveData(deriveData);
                }
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (phase == EActionPhase.Active && controller.IsLightAttacking)
            {
                if (FSM[ECharacterState.LightAttack] is CharacterActionState deriveState)
                {
                    deriveState.InjectDeriveData(new CharacterLightAttackDeriveData(1, true));
                }
                FSM.ChangeState(ECharacterState.LightAttack);
                return true;
            }
            return false;
        }
        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
            if (phase == EActionPhase.Derive && controller.IsMoving)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            // recovery阶段就已经是逻辑上的Idle状态了，所以这里要切换
            if (phase == EActionPhase.Recovery)
            {
                FSM.ChangeState(ECharacterState.Idle);
                return true;
            }
            return false;
        }

        protected override void UnpackDeriveData()
        {
            base.UnpackDeriveData();
            if (deriveDataPack is CharacterEvadeDeriveData deriveData)
            {
                switch (deriveData.type)
                {
                    case CharacterEvadeDeriveData.DeriveType.Reentrant:
                        controller.canEvade = false;
                        evadeColdDownHandle?.Invalidate();
                        //TODO: config
                        evadeColdDownHandle = controller.timerManager.SetTimer(0.8f, () => { controller.canEvade = true; });
                        break;
                }
            }
            // destroy after unpack
            deriveDataPack = null;
        }
    }
}