using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;

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

        internal Vector2Int lastRunDirection;
        internal bool canTurnBack = false;

        #endregion
        
        public CharacterController(IInputHandler inputHandler, IViewHandler viewHandler)
        {
            this.input = inputHandler;
            this.view = viewHandler;

            characterFSM = new CharacterStateMachine();
            characterFSM[ECharacterState.Idle] = new CharacterIdleState(this, characterFSM);
            characterFSM[ECharacterState.Walk] = new CharacterWalkState(this, characterFSM);
            characterFSM[ECharacterState.Run] = new CharacterRunState(this, characterFSM);
            characterFSM.Initialize(ECharacterState.Idle);

            timerManager = new CharacterTimerManager(this);
        }
        
        public void Update(float deltaTime)
        {
            characterFSM.Update(deltaTime);
            timerManager.Update(deltaTime);
        }
    }
}