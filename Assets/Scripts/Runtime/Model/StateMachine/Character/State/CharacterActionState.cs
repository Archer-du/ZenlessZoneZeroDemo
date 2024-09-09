using UnityEngine;
using ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData;
using ZZZDemo.Runtime.Model.Utils;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterActionState : CharacterBaseState
    {
        protected EActionType actionType;
        protected EActionPhase phase = EActionPhase.Terminate;
        internal CharacterActionState(CharacterController controller, CharacterStateMachine stateMachine, 
            ECharacterState type, EActionType actionType) 
            : base(controller, stateMachine, type)
        {
            this.actionType = actionType;
        }
        internal override void Enter()
        {
            base.Enter();
            phase = EActionPhase.Terminate;
            if (controller.IsMoving)
            {
                // adjust rotation when enter action state
                controller.SmoothRotateTowardsTargetDirection();
            }
        }
        protected override void TickLogic(float deltaTime)
        {
            base.TickLogic(deltaTime);
            phase = View.Animation.GetActionPhase(actionType);
        }
        internal virtual void InjectDeriveData(CharacterDeriveData deriveData) {}
    }
}