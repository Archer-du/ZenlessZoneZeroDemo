using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterActionState : CharacterBaseState
    {
        protected EActionType actionType;
        protected EActionPhase phase = EActionPhase.Terminate;

        public CharacterActionState(CharacterController controller, CharacterStateMachine stateMachine, ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type)
        {
            this.actionType = actionType;
        }

        internal override void Enter()
        {
            base.Enter();
            phase = EActionPhase.Terminate;
            UnpackDeriveData();
        }

        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            phase = View.Animation.GetActionPhase(actionType);
        }

        // TODO: derivable interface?

        protected CharacterDeriveData deriveDataPack;
        internal void InjectDeriveData(CharacterDeriveData deriveData) => this.deriveDataPack = deriveData; 
        protected virtual void UnpackDeriveData() {}
    }
}