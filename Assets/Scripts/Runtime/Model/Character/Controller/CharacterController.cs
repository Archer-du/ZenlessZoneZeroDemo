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
        internal bool IsEvading => canEvade && input.EvadeButton.Requesting();

        internal Vector2Int lastRunDirection;
        internal bool canTurnBack = false;
        internal bool canEvade = true;
        // TODO: config
        internal int evadeTimesRemain = 2;

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
            characterFSM.Initialize(ECharacterState.Idle);

            timerManager = new CharacterTimerManager(this);

            // TODO: config
            timerManager.SetTimer(2f, () =>
            {
                if (evadeTimesRemain < 2) evadeTimesRemain++;
            }, true);
        }
        
        public void Update(float deltaTime)
        {
            characterFSM.Update(deltaTime);
            timerManager.Update(deltaTime);
        }
    }
}