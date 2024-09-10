using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.Config;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class CharacterController
    {
        internal IInputHandler input;
        internal IViewHandler view;

        internal CharacterStateMachine characterFSM;
        internal CharacterTimerManager timerManager;

        #region Locomotion
        internal bool IsMoving => input.MoveJoyStick.Value != Vector2.zero;
        internal bool IsSharpTurn => canTurnBack && input.MoveJoyStick.Direction == -lastRunDirection;
        internal bool IsEvading => canEvade && input.EvadeButton.Requesting();
        internal bool IsLightAttacking => input.LightAttackButton.Requesting();
        internal bool IsHeavyAttacking => input.HeavyAttackButton.Requesting();

        // TODO: blackboard member
        internal Vector2Int lastRunDirection;
        internal bool canTurnBack = false;
        internal bool canEvade = true;

        #endregion
        
        public CharacterController(IInputHandler inputHandler, IViewHandler viewHandler)
        {
            this.input = inputHandler;
            this.view = viewHandler;

            characterFSM = new CharacterStateMachine();
            characterFSM[ECharacterState.Idle] = new CharacterIdleState(this, characterFSM);
            characterFSM[ECharacterState.Walk] = new CharacterWalkState(this, characterFSM);
            characterFSM[ECharacterState.Run] = new CharacterRunState(this, characterFSM);
            characterFSM[ECharacterState.Evade] = new CharacterEvadeState(this, characterFSM);
            characterFSM[ECharacterState.LightAttack] = new CharacterLightAttackState(this, characterFSM);
            characterFSM[ECharacterState.HeavyAttack] = new CharacterHeavyAttackState(this, characterFSM);
            characterFSM.Initialize(ECharacterState.Idle);

            timerManager = new CharacterTimerManager(this);
        }
        
        public void Update(float deltaTime)
        {
            characterFSM.Update(deltaTime);
            timerManager.Update(deltaTime);
        }
        
        internal void SmoothRotateTowardsTargetDirection(float deltaTime = 0, float responseTime = 0)
        {
            responseTime = Mathf.Clamp(responseTime, 0, 10);
            var targetDir = MovementUtils.GetHorizontalProjectionVector(input.LookAt.GetLookAtDirection());
            targetDir = MovementUtils.GetRotationByAxis(
                MovementUtils.GetRelativeInputAngle(input.MoveJoyStick.Value), Vector3.up) * targetDir;
            float angle = MovementUtils.GetRelativeRotateAngle(view.Movement.GetCharacterForward(), targetDir);
            if (Mathf.Abs(angle) > GlobalConstants.smoothRotateAngleTolerance)
            {
                if (responseTime == 0)
                {
                    view.Movement.RotateCharacterHorizontal(angle);
                }
                else
                {
                    view.Movement.RotateCharacterHorizontal(angle * (deltaTime / responseTime));
                }
            }
        }
    }
}